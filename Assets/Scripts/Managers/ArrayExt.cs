using System;
using System.Collections.Generic;

public static class ArrayExt
{
    public static T RandomAt<T>(this T[] array) => array[UnityEngine.Random.Range(0, array.Length)];
    public static T RandomAt<T>(this List<T> list) => list[UnityEngine.Random.Range(0, list.Count)];    
    public static T Find<T>(this T[] array, Predicate<T> match) => Array.Find(array, match);

}
