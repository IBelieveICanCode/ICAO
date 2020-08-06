using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Test : MonoBehaviour
{
    private void Start()
    {
        string str = "The quick brown fox jumps over the lazy dog.";
        print(Sort(str));
        //int[] arr = { 0, 0, 0, 1, 1, 1, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5 };
        //print (Number(arr));
    }

    int Number(int[] arr)
    {
        Dictionary<int, int> m = new Dictionary<int, int>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (m.ContainsKey(arr[i]))
            {
                var val = m[arr[i]];
                m.Remove(arr[i]);
                m.Add(arr[i], val + 1);
            }
            else
            {
                m.Add(arr[i], 1);
            }
        }
        for (int i = 0; i < arr.Length; i++)
            if (m[arr[i]] == 1)
                return arr[i];
        return -1;
    }

    string Sort(string str)
    {
        return String.Concat(str.ToLower().OrderBy(c => c));
    }
    
    public class A
    {
        public static int operator +(A a, A b)
        {
            return 1;
        }
    
        public static int operator *(A a, string str)
        {
            return 0;
        }
    }
}
