using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBezierPath
{
    public class Bezier
    {
        public static Vector3 QuardaticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            Vector3 _pos =
                Mathf.Pow(1 - t, 2) * p0 +
                2 * (1 - t) * t * p1 +
                Mathf.Pow(t, 2) * p2;
            return _pos;
        }

        public static Vector3 QuadraticVelocity(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            Vector3 _vel =
                2 * (1 - t) * p0 +
                (2 - 4 * t) * p1 +
                2 * t * p2;
            return _vel;
        }

        public static Vector3 CubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Vector3 _pos =
                Mathf.Pow(1 - t, 3) * p0 +
                3 * Mathf.Pow(1 - t, 2) * t * p1 +
                3 * Mathf.Pow(1 - t, 2) * t * p2 +
                 Mathf.Pow(t, 3) * p3;
            return _pos;
        }

        public static Vector3 CubicVelocity(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Vector3 _vel =
                3 * Mathf.Pow(1 - t, 2) * p0 +
                6 * (1 - t) * p1 +
                6 * t * p2 +
                3 * Mathf.Pow(t, 2) * p3;
            return _vel;
        }

    }
}
