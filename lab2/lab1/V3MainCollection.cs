using System;
using System.Numerics;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Globalization;

namespace lab1
{
    class V3MainCollection
    {
        private List<V3Data> collection;
        public V3MainCollection()
        {
            collection = new List<V3Data>();
        }
        public int Count { get => collection.Count; }
        public V3Data this[int i] { get => collection[i]; }
        public bool Contains(string ID)
        {
            foreach (V3Data data in collection)
            {
                if (data.name == ID)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Add(V3Data v3Data)
        {
            if (Contains(v3Data.name))
            {
                return false;
            }
            collection.Add(v3Data);
            return true;
        }
        public string ToLongString(string format = "")
        {
            string str = "\n-----collection-----\n";
            foreach (V3Data data in collection)
            {
                str += $"{data.ToLongString(format)}\n";
            }
            str += "--------------------\n";
            return str;
        }
        public override string ToString()
        {
            string str = "\n-----collection-----\n";
            foreach (V3Data data in collection)
            {
                str += $"{data}\n";
            }
            str += "--------------------\n";
            return str;
        }

        public DataItem? MaxDistanceItem
        {
            get
            {
                if (Count == 0)
                {
                    return null;
                }
                var data = from item in collection
                           from t in item
                           orderby t.field.Length()
                           select t;
                return data.Last();
            }
        }

        public IEnumerable<double> RepeatableX
        {
            get
            {
                if (Count == 0)
                {
                    return null;
                }
                var data = from item in collection
                           from t in item
                           select t.x;
                var ret = data.GroupBy(x => x)
                          .Where(g => g.Count() > 1)
                          .Select(y => y.Key)
                          .ToList();
                if (ret.Count == 0)
                {
                    return null;
                }
                return ret;
            }
        }

        public IEnumerable<V3Data> MinTimeData
        {
            get
            {
                if (Count == 0)
                {
                    return null;
                }
                var time_min = (from item in collection select item.time).Min();
                var data = from item in collection
                           where item.time == time_min
                           select item;
                return data;
            }
        }
    }
}
