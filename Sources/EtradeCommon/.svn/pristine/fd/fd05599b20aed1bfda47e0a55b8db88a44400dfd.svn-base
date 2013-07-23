using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;

namespace OTS.WebLib.array
{
    public class ArrayEx {
    /// <summary>
    /// Appends a list of elements to the end of an array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="items"></param>
    public static void Append<T>(ref T[] array, params T[] items)
    {
        int oldLength = array.Length;
        //make room for new items
        Array.Resize<T>(ref array, oldLength + items.Length);
        
        for(int i=0;i<items.Length;i++)
            array[oldLength + i] = items[i];
    }

    /// <summary>
    /// Remove an Array at a specific Location
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index">index to remove at</param>
    /// <param name="list">The Array Object</param>
    public static void RemoveAt<T>(int index, ref T[] list)
    {
        //pre:
        if (index < 0 || list == null | list.Length == 0) return;

        //move everything from the index on to the left one then remove last empty
        if (list.Length > index + 1)
            for (int i = index + 1; i < list.Length; i++)
                list[i - 1] = list[i]; 

        Array.Resize<T>(ref list, list.Length - 1);
    }

    /// 
    ///<summary> Thanks to homer.
    /// Remove all elements in an array satisfying a predicate
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">The Array Object</param>
    /// <param name="condition">A Predicate when the element shall get removed under.
    /// </param>
    /// <returns>Number of elements removed</returns>
    public static int RemoveAll<T>(ref T[] list, Predicate<T> condition)
    {
        if (null == condition || null == list || 0 == list.Length) return 0;
        
        int length = list.Length;
        int destinationIndex = 0;
        T[] destinationArray = new T[length];
        
        for (int i = 0; i < list.Length; i++)
        {
            if (!condition(list[i]))
            {
                destinationArray[destinationIndex] = list[i];
                destinationIndex++;
            }
        }
        
        if (destinationIndex != length)
        {
            Array.Resize<T>(ref destinationArray, destinationIndex);
            list = destinationArray;
        }
        
        return length - destinationIndex;
    }
}
}