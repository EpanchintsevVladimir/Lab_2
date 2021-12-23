using System;
using System.Numerics;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Globalization;

namespace lab1
{
    abstract class V3Data : IEnumerable<DataItem>
    {
        public string name { get; protected set; }
        public DateTime time { get; protected set; }
        public abstract int Count { get; }
        public abstract double MaxDistance { get; }
        public V3Data(string name, DateTime time)
        {
            this.name = name;
            this.time = time;
        }
        public abstract string ToLongString(string format);
        public override string ToString()
        {
            return $"name: {name}\ntime: {time}\n";
        }
        public abstract IEnumerator<DataItem> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}