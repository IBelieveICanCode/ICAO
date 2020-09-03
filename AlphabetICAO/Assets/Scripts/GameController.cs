using System;
using System.Collections;
using System.Collections.Generic;
using FactorySpace;
using UnityEngine;
using UnityEngine.Events;
using FieldSettings;
using Zenject;

namespace Adminka
{
    public delegate void LevelProgressHandler(LevelProgress progress);

    public class GameController : MonoBehaviour
    {
        public LevelProgress LvlProgress { get; private set; }
        private int roundCount;

        private ICAO alphabetIcao;
        public Dictionary<string, string> ChosenWords { get; private set; }
        [SerializeField] private PathPlane pathPlane;

        [SerializeField]
        private Field field;
        private Field Field => field;

        //private Factory planeFactory;
        //private IPlaneCommunicator plane;
        private Plane plane;
        [Inject]
        private Plane.PlaneFactory planeFactory;

        [Header("Events")]
        public UnityEvent CorrectEvent;
        public UnityEvent WrongEvent;
        public UnityEvent DefeatEvent;
        public event LevelProgressHandler ProgressEvent;

        private void Start()
        {
            LvlProgress = new LevelProgress {Rounds = 1};
            Field.CreateField();       
            //planeFactory = FactoryProducer.GetFactory(FactoryProductType.Planes);
            InitPlane();

            WrongEvent.AddListener(() => HUDControl.Instance.DiscardHealth());
            CorrectEvent.AddListener(() => HUDControl.Instance.AddHealth());
            CorrectEvent.AddListener(() =>
            {
                if (ProgressEvent != null) ProgressEvent(LvlProgress);
            });
            ProgressEvent += ProgressLevel;
            DefeatEvent.AddListener(() => HUDControl.Instance.Defeat());
            
        }

        private void InitPlane()
        {
            Vector3 _planeSpawn = Field.PickRandomSpotToSpawn();
            CreatePathForPlane(_planeSpawn, Field.Center, Field.StartingDestination);
            //plane = planeFactory.GetProduct((int)LvlProgress.PlaneTypes, _planeSpawn, pathPlane.Path);
            plane = planeFactory.Create(LvlProgress, _planeSpawn, pathPlane.Path);
            HUDControl.Instance.AskPlayer();
        }

        private void CreatePathForPlane(Vector3 startingPoint, Vector3 secondPoint, Vector3 endPoint)
        {
            Vector3[] _plane = new Vector3[3]
            {
                startingPoint,
                secondPoint,
                endPoint
            };
            pathPlane.CreatePath(_plane);
        }

        public void SetUpWords(int amount)
        {
            //HUDControl.Instance.ChangeWordsPanel(new Vector2(0, -80), 0f);
            ChosenWords = ICAO.ReturnRandomWordsFromAlphabet(amount);
            HUDControl.Instance.ShowWordsPanel();
        }

        private void ProgressLevel(LevelProgress lvlProgress)
        {
            lvlProgress.Rounds++;
        }
        public void AnswerCorrectUpdatePath()
        {
            Vector3 _finishPos = Field.FinishPointPos();
            CreatePathForPlane(plane.Position, _finishPos, _finishPos);
            plane.Accelerate();
            pathPlane.Path.TriggerPathUpdate();
        }

        public void AnswerNotCorrect()
        {
            plane.Accelerate();
        }
        void SpawnConfetti()
        {
            GameObject _confetti = Resources.Load("Prefabs/Confetti") as GameObject;
            foreach (Vector3 _pos in Field.GetCornersOfField(2))
            {
                ParticleSystem _conf = Instantiate(_confetti).GetComponent<ParticleSystem>(); ;
                _conf.transform.position = _pos;
                Vector3 _dir = (Field.Center - _pos).normalized;
                Quaternion _targetRot = Quaternion.LookRotation(_dir);
                _conf.transform.rotation = Quaternion.Slerp(_conf.transform.rotation, _targetRot, 10);
            }
        }
    }
}