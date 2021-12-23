using System;
using System.Numerics;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Globalization;

namespace lab1
{
    public static class functions
    {
        public static Vector2 F(double x, double y)
        {
            return new Vector2(Convert.ToSingle(x * x), Convert.ToSingle(y * 5));
        }
    }

    class Program
    {
        public static void First()
        {
            V3DataArray arr_before = new V3DataArray("Array Before", DateTime.Now, 2, 2, 1, 1, functions.F);
            Console.WriteLine(arr_before.ToLongString());
            arr_before.SaveAsText("array.txt");
            V3DataArray arr_after = new V3DataArray("Array After", DateTime.Now);
            V3DataArray.LoadAsText("array.txt", ref arr_after);
            Console.WriteLine(arr_after.ToLongString());

            V3DataList list_before = new V3DataList("List Before", DateTime.Now);
            list_before.AddDefaults(3, functions.F);
            Console.WriteLine(list_before.ToLongString("F3"));
            list_before.SaveBinary("list");
            V3DataList list_after = new V3DataList("List After", DateTime.Now);
            V3DataList.LoadBinary("list", ref list_after);
            Console.WriteLine(list_after.ToLongString("F3"));
        }

        public static void Second()
        {
            DateTime MinTime = new DateTime(2008, 3, 1, 7, 0, 0);
            V3DataArray arr0 = new V3DataArray("Arr_0", DateTime.Now);
            V3DataArray arr1 = new V3DataArray("Arr_1", DateTime.Now, 2, 3, 1, 0.5, functions.F);
            V3DataArray arr2 = new V3DataArray("Arr_2", MinTime, 3, 2, 0.5, 1, functions.F);

            V3DataList list0 = new V3DataList("List_0", DateTime.Now);

            V3DataList list1 = new V3DataList("List_1", DateTime.Now);
            DataItem data1 = new DataItem(0, 1, functions.F(5, 3));
            DataItem data2 = new DataItem(1, 0, functions.F(3, 5));
            DataItem data3 = new DataItem(2, 1, functions.F(4, 2));
            list1.Add(data1);
            list1.Add(data2);
            list1.Add(data3);

            V3DataList list2 = new V3DataList("List_2", MinTime);
            list2.AddDefaults(3, functions.F);

            V3MainCollection collection = new V3MainCollection();
            collection.Add(arr0);
            collection.Add(arr1);
            collection.Add(arr2);
            collection.Add(list0);
            collection.Add(list1);
            collection.Add(list2);
            Console.WriteLine(collection.ToLongString("F3"));

            Console.WriteLine("\nMaxDistanceItem test:\n");
            if (collection.MaxDistanceItem == null)
            {
                Console.WriteLine("No data in collection.\n");
            }
            else
            {
                Console.WriteLine($"Max distance element: {collection.MaxDistanceItem}");
            }
            
            Console.WriteLine("\nRepeatableX test:\n");
            if (collection.RepeatableX == null)
            {
                Console.WriteLine("There is no duplicate coordinates in X axis in collection.\n");
            }
            else
            {
                foreach (double item in collection.RepeatableX)
                {
                    Console.WriteLine(item);
                }
            }

            Console.WriteLine("\nMinTimeData test:\n");
            if (collection.MinTimeData == null)
            {
                Console.WriteLine("No data in collection.\n");
            }
            else
            {
                foreach (V3Data item in collection.MinTimeData)
                {
                    Console.WriteLine(item);
                }
            }

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Отладка чтения/записи\n");
            Program.First();
            Console.WriteLine("Отладка методов LINQ\n");
            Program.Second();
        }
    }
}

