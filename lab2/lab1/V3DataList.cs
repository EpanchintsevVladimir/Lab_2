using System;
using System.Numerics;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Globalization;

namespace lab1
{
    class V3DataList : V3Data
    {
        public List<DataItem> data { get; }
        public V3DataList(string name, DateTime time) : base(name, time)
        {
            data = new List<DataItem>();
        }
        public bool Add(DataItem newItem)
        {
            foreach (DataItem item in data)
            {
                if ((item.x == newItem.x) && (item.y == newItem.y))
                {
                    return false;
                }
            }
            data.Add(newItem);
            return true;
        }
        public int AddDefaults(int nItems, FdblVector2 F)
        {
            Random rand = new Random(nItems);
            int count = 0;
            for (int i = 0; i < nItems; ++i)
            {
                double x = rand.NextDouble() * 100;
                double y = rand.NextDouble() * 100;
                if (Add(new DataItem(x, y, F(x, y))))
                {
                    count++;
                }
            }
            return count;
        }
        public override int Count { get => data.Count; }
        public override double MaxDistance
        {
            get
            {
                double maxDist = 0.0;
                for (int i = 0; i < data.Count; ++i)
                {
                    for (int j = i + 1; j < data.Count; ++j)
                    {
                        double dist = Math.Sqrt(Math.Pow(data[i].x - data[j].x, 2) + Math.Pow(data[i].y - data[j].y, 2));
                        if (dist > maxDist)
                        {
                            maxDist = dist;
                        }
                    }
                }
                return maxDist;
            }
        }
        public override string ToString()
        {
            return $"V3DataList: {base.ToString()}\n";
        }
        public override string ToLongString(string format = "")
        {
            string str = ToString();
            foreach (DataItem item in data)
            {
                str += $"--: {item.ToLongString(format)}\n";
            }
            return str;
        }

        public override IEnumerator<DataItem> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        public bool SaveBinary(string filename)
        {
            FileStream fs = null;
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate);
                BinaryWriter writer = new BinaryWriter(fs);
                writer.Write(name);
                writer.Write(time.ToString());
                writer.Write(Count);
                foreach (DataItem item in data)
                {
                    writer.Write(item.x);
                    writer.Write(item.y);
                    writer.Write(item.field.X);
                    writer.Write(item.field.Y);
                }
                writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
        static public bool LoadBinary(string filename, ref V3DataList v3)
        {
            FileStream fs = null;
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                BinaryReader reader = new BinaryReader(fs);
                string name = reader.ReadString();
                DateTime time = Convert.ToDateTime(reader.ReadString());
                int count = reader.ReadInt32();
                if (v3 == null)
                {
                    v3 = new V3DataList(name, time);
                }
                else
                {
                    v3.name = name;
                    v3.time = time;
                }
                for (int i = 0; i < count; i++)
                {
                    double x = reader.ReadDouble();
                    double y = reader.ReadDouble();
                    float vector_x = reader.ReadSingle();
                    float vector_y = reader.ReadSingle();
                    Vector2 vector = new Vector2(vector_x, vector_y);
                    DataItem item = new DataItem(x, y, vector);
                    v3.data.Add(item);
                }
                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
    }
}
