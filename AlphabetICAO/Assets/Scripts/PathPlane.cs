using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(PathCreator))]
public class PathPlane : MonoBehaviour
{
    [HideInInspector]
    public PathCreator path;
    private void Awake()
    {
        path = GetComponent<PathCreator>();
    }

    public void CreatePath(Vector3[] points)
    {
        BezierPath bezierPath = new BezierPath(points, false, PathSpace.xy);
        path.bezierPath = bezierPath;
    }
}
