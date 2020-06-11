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
    GameObject finish;

    Bounds bounds;
    Vector3 leftUpPoint;
    Vector3 rightUpPoint;
    Vector3 leftDownPoint;
    Vector3 rightDownPoint;

    List<Vector3> cornersOfField;
    Vector3[] sidesToSpawnPlane;

    Vector3 offset = Vector3.down * 2;
    public Vector3 StartingDestination => Utility.RandomizePoint(finishPos + offset);

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
        GameObject _field = Instantiate(fieldPrefab, Vector3.forward, Quaternion.identity);
        _field.transform.localScale = new Vector3(width, height, 0.1f);
        _field.transform.parent = this.transform;

        center = GetComponentInChildren<MeshRenderer>().bounds.center;
        SetSidesOfMesh();

        GameObject _finishPrefab = Resources.Load("Prefabs/Finish") as GameObject;
        finishPos = new Vector3(0f, -height / 2, 0.2f);
        finish = Instantiate(_finishPrefab, finishPos, Quaternion.identity);
        finish.transform.parent = this.transform;

        BoxCollider2D _col = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        _col.offset = finishPos + offset;
        _col.size = new Vector3((rightDownPoint - leftDownPoint).magnitude, 1) ;
        _col.isTrigger = true;
    }

    private void SetSidesOfMesh()
    {
        bounds = GetComponentInChildren<MeshRenderer>().bounds;
        leftDownPoint = new Vector3(-bounds.extents.x - 1f, -bounds.extents.y - 1f, -.1f);
        rightDownPoint = new Vector3(bounds.extents.x + 1f, -bounds.extents.y - 1f, -.1f);
        leftUpPoint = new Vector3(-bounds.extents.x - 1f, bounds.extents.y + 1f, -.1f);
        rightUpPoint = new Vector3(bounds.extents.x + 1f, bounds.extents.y + 1f, -.1f);
        cornersOfField = new List<Vector3>() { leftDownPoint, rightDownPoint, leftUpPoint, rightUpPoint };
    }

    public Vector3 PickRandomSpotToSpawn()
    {
        Vector3 _leftSideVec = leftUpPoint - leftDownPoint;
        Vector3 _rightSideVec = rightUpPoint - rightDownPoint;
        Vector3 _upSideVec = rightUpPoint - leftUpPoint;

        Vector3 _leftSpawn = leftDownPoint + Random.value * _leftSideVec;
        Vector3 _rightSpawn = rightDownPoint + Random.value * _rightSideVec;
        Vector3 _upSpawn = leftUpPoint + Random.value * _upSideVec;
        sidesToSpawnPlane = new Vector3[3] {
            _leftSpawn,
            _rightSpawn,
            _upSpawn
        };
        return Utility.PickRandomPos(sidesToSpawnPlane);
    }

    public Vector3 FinishPoint()
    {
        Vector3 _center = new Vector3((leftDownPoint.x + rightDownPoint.x) / 2, (leftDownPoint.y + rightDownPoint.y) / 2);
        return _center;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IPlaneCommunicator>() != null)
            GameController.Instance.LoseEvent?.Invoke();

    }

    public Vector3[] ReturnCornersOfField(int amount)
    {
        List<Vector3> _corners = new List<Vector3>();
        if (amount > cornersOfField.Count)
            amount = cornersOfField.Count;
        for (int i = 0; i < amount; i++)
        {
            _corners.Add(cornersOfField[i]);
        }
        return _corners.ToArray();
    }
}
