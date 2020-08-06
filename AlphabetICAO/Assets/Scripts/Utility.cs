using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UtilityScripts
{
    public static class Utility
    {
        public static Vector3 CircleCenter(Vector3 aP0, Vector3 aP1, Vector3 aP2, out Vector3 normal)
        {
            var _v1 = aP1 - aP0;
            var _v2 = aP2 - aP0;

            normal = Vector3.Cross(_v1, _v2);
            if (normal.sqrMagnitude < 0.00001f)
                return Vector3.one * float.NaN;
            normal.Normalize();

            // perpendicular of both chords
            var _p1 = Vector3.Cross(_v1, normal).normalized;
            var _p2 = Vector3.Cross(_v2, normal).normalized;
            // distance between the chord midpoints
            var _r = (_v1 - _v2) * 0.5f;
            // center angle between the two perpendiculars
            var _c = Vector3.Angle(_p1, _p2);
            // angle between first perpendicular and chord midpoint vector
            var _a = Vector3.Angle(_r, _p1);
            // law of sine to calculate length of p2
            var _d = _r.magnitude * Mathf.Sin(_a * Mathf.Deg2Rad) / Mathf.Sin(_c * Mathf.Deg2Rad);
            if (Vector3.Dot(_v1, aP2 - aP1) > 0)
                return aP0 + _v2 * 0.5f - _p2 * _d;
            return aP0 + _v2 * 0.5f + _p2 * _d;
        }

        public static T[] ShuffleArray<T>(T[] array, int seed)
        {
            System.Random _prng = new System.Random(seed);

            for (int i = 0; i < array.Length - 1; i++)
            {
                int randomIndex = _prng.Next(i, array.Length);
                T _tempItem = array[randomIndex];
                array[randomIndex] = array[i];
                array[i] = _tempItem;
            }
            return array;
        }

        public static string ReplaceStringWithUnderlines(string word)
        {
            var _replacedWord = word[0].ToString();
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
        
        public static IEnumerator Fade(float time, Graphic fadePlane)
        {
            var _from = fadePlane.color;
            var _to = new Color(0, 0, 0);

            var _speed = 1 / time;
            float _percent = 0;
            while (_percent < 1)
            {
                _percent += Time.deltaTime * _speed;
                fadePlane.color = Color.Lerp(_from, _to, _percent);
                yield return null;
            }
        }
    }
}
