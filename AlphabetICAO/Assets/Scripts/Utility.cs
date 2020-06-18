using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static Vector3 CircleCenter(Vector3 aP0, Vector3 aP1, Vector3 aP2, out Vector3 normal)
    {
        var v1 = aP1 - aP0;
        var v2 = aP2 - aP0;

        normal = Vector3.Cross(v1, v2);
        if (normal.sqrMagnitude < 0.00001f)
            return Vector3.one * float.NaN;
        normal.Normalize();

        // perpendicular of both chords
        var p1 = Vector3.Cross(v1, normal).normalized;
        var p2 = Vector3.Cross(v2, normal).normalized;
        // distance between the chord midpoints
        var r = (v1 - v2) * 0.5f;
        // center angle between the two perpendiculars
        var c = Vector3.Angle(p1, p2);
        // angle between first perpendicular and chord midpoint vector
        var a = Vector3.Angle(r, p1);
        // law of sine to calculate length of p2
        var d = r.magnitude * Mathf.Sin(a * Mathf.Deg2Rad) / Mathf.Sin(c * Mathf.Deg2Rad);
        if (Vector3.Dot(v1, aP2 - aP1) > 0)
            return aP0 + v2 * 0.5f - p2 * d;
        return aP0 + v2 * 0.5f + p2 * d;
    }

    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        System.Random prng = new System.Random(seed);

        for (int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = prng.Next(i, array.Length);
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }
        return array;
    }

    public static string ReplaceStringWithUnderlines(string word)
    {
        string _replacedWord = word[0].ToString();
        //char _firstLetter = word[0];
        //_replacedWord += _firstLetter;
        for (int i = 0; i < word.Length - 1; i++)
        {
            _replacedWord += "_";
        }
        return _replacedWord;
            //changedWords.Add(_replacedWord);
    }

    public static Vector3 RandomizePoint(Vector3 pos)
    {
        Vector3 _rightSide = pos + (Vector3.right * Random.Range(4, 6));
        Vector3 _leftside = pos + (Vector3.left * Random.Range(4, 6));
        return PickRandomPos(new Vector3[2] { _rightSide, _leftside });

    }

    public static Vector3 PickRandomPos(Vector3[] arr)
    {
        int _rand = Random.Range(0, arr.Length);
        Vector3 _randomSide = arr[_rand];
        _randomSide.z = -0.1f;
        return _randomSide;
    }
}
