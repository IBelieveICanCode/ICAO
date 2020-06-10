using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{

    float width;
    float height;
    [HideInInspector]
    public Vector3 
        center,
        finishPos;
    GameObject field;
    GameObject finish;

    Bounds bounds;
    Vector3 leftUpPoint;
    Vector3 rightUpPoint;
    Vector3 leftDownPoint;
    Vector3 rightDownPoint;

    Vector3[] sidesToSpawn;

    public Vector3 Destination => Utility.RandomizePoint(finishPos);

    private void Awake()
    {
        //bezier = GetComponent<BezierSpline>();
    }
    public void CreateField()
    {
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = Camera.main.aspect * halfHeight;
        width = halfWidth * 2;
        height = halfHeight * 2;

        GameObject fieldPrefab = Resources.Load("Prefabs/Field") as GameObject;
        field = Instantiate(fieldPrefab, Vector3.forward, Quaternion.identity);
        field.transform.localScale = new Vector3(width, height, 0.1f);
        field.transform.parent = this.transform;

        center = GetComponentInChildren<MeshRenderer>().bounds.center;
        SetSidesOfMesh();

        GameObject _finishPrefab = Resources.Load("Prefabs/Finish") as GameObject;
        finishPos = new Vector3(0f, -height / 2, 0.2f);
        finish = Instantiate(_finishPrefab, finishPos, Quaternion.identity);
        finish.transform.parent = this.transform;
    }

    private void SetSidesOfMesh()
    {
        bounds = GetComponentInChildren<MeshRenderer>().bounds;
        leftUpPoint = new Vector3(-bounds.extents.x - 1f, bounds.extents.y + 1f, -.1f);
        rightUpPoint = new Vector3(bounds.extents.x + 1f, bounds.extents.y + 1f, -.1f);
        leftDownPoint = new Vector3(-bounds.extents.x - 1f, -bounds.extents.y - 1f, -.1f);
        rightDownPoint = new Vector3(bounds.extents.x + 1f, -bounds.extents.y - 1f, -.1f);

    }

    public Vector3 PickRandomSpotToSpawn()
    {
        Vector3 _leftSideVec = leftUpPoint - leftDownPoint;
        Vector3 _rightSideVec = rightUpPoint - rightDownPoint;
        Vector3 _upSideVec = rightUpPoint - leftUpPoint;

        Vector3 _leftSpawn = leftDownPoint + Random.value * _leftSideVec;
        Vector3 _rightSpawn = rightDownPoint + Random.value * _rightSideVec;
        Vector3 _upSpawn = leftUpPoint + Random.value * _upSideVec;
        sidesToSpawn = new Vector3[3] {
            _leftSpawn,
            _rightSpawn,
            _upSpawn
        };
        return Utility.PickRandomPos(sidesToSpawn);
    }

    public Vector3 FinishPoint()
    {
        Vector3 _center = new Vector3((leftDownPoint.x + rightDownPoint.x) / 2, (leftDownPoint.y + rightDownPoint.y) / 2);
        return _center;
    }
}
