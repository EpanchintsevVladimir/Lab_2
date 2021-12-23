using System;
using System.Numerics;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Globalization;

namespace lab1
{
    class V3DataArray : V3Data
    {
        public int x_n { get; private set; }
        public int y_n { get; private set; }
        public double x_step { get; private set; }
        public double y_step { get; private set; }
        public Vector2[,] grid { get; private set; }
        public V3DataArray(string name, DateTime time) : base(name, time)
        {
            x_n = 0;
            y_n = 0;
            x_step = 0.0;
            y_step = 0.0;
            grid = new Vector2[0, 0];
        }
        public V3DataArray(string name, DateTime time, int x_n, int y_n,
                           double x_step, double y_step, FdblVector2 F) : base(name, time)
        {
            this.x_n = x_n;
            this.y_n = y_n;
            this.x_step = x_step;
            this.y_step = y_step;
            grid = new Vector2[x_n, y_n];
            for (int i = 0; i < x_n; ++i)
            {
                for (int j = 0; j < y_n; ++j)
                {
                    grid[i, j] = F(i * x_step, j * y_step);
                }
            }
        }
        public override int Count { get => grid.Length; }
        public override double MaxDistance { get =>  Math.Sqrt(Math.Pow((x_n - 1) * x_step, 2) + Math.Pow((y_n - 1) * y_step, 2)); }
        public override string ToString()
        {
            return $"V3DataArray: {base.ToString()}\nx_nodes = {x_n} y_nodes = {y_n} x_step = {x_step} y_step = {y_step}\n";
        }
        public override string ToLongString(string format = "")
        {
            string str = ToString();
            for (int i = 0; i < x_n; ++i)
            {
                for (int j = 0; j < y_n; ++j)
                {
                    str += $"--: x = {(x_step * i).ToString(format)} y = {(y_step * j).ToString(format)} field = {grid[i, j].ToString(format)}\n";
                }
            }
            return str;
        }
        public static explicit operator V3DataList(V3DataArray array)
        {
            V3DataList list = new V3DataList(array.name, array.time);
            for (int i = 0; i < array.x_n; i++)
            {
                for (int j = 0; j < array.y_n; j++)
                {
                    list.Add(new DataItem(array.x_step * i, array.y_step * j, array.grid[i, j]));
                }
            }
            return list;
        }

        public override IEnumerator<DataItem> GetEnumerator()
        {
            for (int i = 0; i < x_n; i++)
            {
                for (int j = 0; j < y_n; j++)
                {
                    DataItem dataItem = new DataItem(i * x_step, j * y_step, grid[i, j]);
                    yield return dataItem;
                }
            }
        }

        public bool SaveAsText(string filename)
        {
            FileStream fs = null;
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(name);
                sw.WriteLine(time.ToString());
                sw.WriteLine(x_n.ToString());
                sw.WriteLine(y_n.ToString());
                sw.WriteLine(x_step.ToString());
                sw.WriteLine(y_step.ToString());
                for (int i = 0; i < x_n; i++)
                {
                    for (int j = 0; j < y_n; j++)
                    {
                        sw.WriteLine(grid[i, j].X.ToString());
                        sw.WriteLine(grid[i, j].Y.ToString());
                    }
                }
                sw.Close();
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
        static public bool LoadAsText(string filename, ref V3DataArray v3)
        {
            FileStream fs = null;
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string name = sr.ReadLine();
                DateTime time = Convert.ToDateTime(sr.ReadLine());
                if (v3 == null)
                {
                    v3 = new V3DataArray(name, time);
                }
                else
                {
                    v3.name = name;
                    v3.time = time;
                }
                v3.x_n = Convert.ToInt32(sr.ReadLine());
                v3.y_n = Convert.ToInt32(sr.ReadLine());
                v3.x_step = Convert.ToDouble(sr.ReadLine());
                v3.y_step = Convert.ToDouble(sr.ReadLine());
                v3.grid = new Vector2[v3.x_n, v3.y_n];
                for (int i = 0; i < v3.x_n; i++)
                {
                    for (int j = 0; j < v3.y_n; j++)
                    {
                        float x = Convert.ToSingle(sr.ReadLine());
                        float y = Convert.ToSingle(sr.ReadLine());
                        v3.grid[i, j] = new Vector2(x, y);
                    }
                }
                sr.Close();
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
