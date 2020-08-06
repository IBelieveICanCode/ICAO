using System.Collections.Generic;
using Adminka;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UtilityScripts;

namespace FieldSettings
{
    public class Field : MonoBehaviour
    {
        private float width;
        private float height;

        public Vector3 Center { get; private set; }

        private GameObject finish;

        private Bounds bounds;

        private Vector3 leftUpPoint,
            rightUpPoint,
            leftDownPoint,
            rightDownPoint;
        private Vector3 finishPos;

        private List<Vector3> cornersOfField;
        private Vector3[] sidesToSpawnPlane;

        private readonly Vector3 offset = Vector3.down * 2;
        public Vector3 StartingDestination => Utility.RandomizePoint(finishPos + offset);

        private void Awake()
        {
        }
        public void CreateField()
        {
            var _halfHeight = Camera.main.orthographicSize;
            var _halfWidth = Camera.main.aspect * _halfHeight;
            width = _halfWidth * 2;
            height = _halfHeight * 2;

            var _fieldPrefab = Resources.Load("Prefabs/Field") as GameObject;
            var _field = Instantiate(_fieldPrefab, Vector3.forward, Quaternion.identity);
            _field.transform.localScale = new Vector3(width, height, 0.1f);
            _field.transform.parent = this.transform;

            Center = GetComponentInChildren<MeshRenderer>().bounds.center;
            SetSidesOfMesh();

            var _finishPrefab = Resources.Load("Prefabs/Finish") as GameObject;
            finishPos = new Vector3(0f, -height / 2, 0.2f);
            finish = Instantiate(_finishPrefab, finishPos, Quaternion.identity);
            finish.transform.parent = this.transform;

            var _col = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
            if (_col != null)
            {
                _col.offset = finishPos + offset;
                _col.size = new Vector3((rightDownPoint - leftDownPoint).magnitude, 1);
                _col.isTrigger = true;
            }

            this.OnTriggerEnter2DAsObservable()
                .Where(collision => collision.gameObject.GetComponent<IPlaneCommunicator>() != null)
                .Subscribe(collision => GameController.Instance.WrongEvent?.Invoke());
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
            var _leftSideVec = leftUpPoint - leftDownPoint;
            var _rightSideVec = rightUpPoint - rightDownPoint;
            var _upSideVec = rightUpPoint - leftUpPoint;

            var _leftSpawn = leftDownPoint + Random.value * _leftSideVec;
            var _rightSpawn = rightDownPoint + Random.value * _rightSideVec;
            var _upSpawn = leftUpPoint + Random.value * _upSideVec;
            sidesToSpawnPlane = new Vector3[3] {
                _leftSpawn,
                _rightSpawn,
                _upSpawn
            };
            return Utility.PickRandomPos(sidesToSpawnPlane);
        }

        public Vector3 FinishPointPos()
        {
            var _center = new Vector3((leftDownPoint.x + rightDownPoint.x) / 2, (leftDownPoint.y + rightDownPoint.y) / 2);
            return _center;
        }

        public IEnumerable<Vector3> ReturnCornersOfField(int amount)
        {
            var _corners = new List<Vector3>();
            if (amount > cornersOfField.Count)
                amount = cornersOfField.Count;
            for (var i = 0; i < amount; i++)
            {
                _corners.Add(cornersOfField[i]);
            }
            return _corners.ToArray();
        }
    }
}
