using System;
using JetBrains.Annotations;
using PathCreation;
using UnityEngine;

    [RequireComponent(typeof(PathCreator))]
    public class PathPlane : MonoBehaviour
    {
        [HideInInspector]
        public PathCreator Path { get; private set; }
        private void Awake()
        {
            Path = GetComponent<PathCreator>();
        }

        public void CreatePath([NotNull] Vector3[] points)
        {
            if (points == null) throw new ArgumentNullException(nameof(points));
            BezierPath _bezierPath = new BezierPath(points, false, PathSpace.xy);
            Path.bezierPath = _bezierPath;
        }
    }

