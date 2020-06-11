using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FactorySpace;
using System;

public class GameController : Singleton<GameController>
{
    Dictionary<string,string> chosenWords;
    public Dictionary<string, string> ChosenWords => chosenWords;
    private ICAO ICAO;
    [SerializeField]
    PathPlane pathPlane;
    [SerializeField]
    private Field field;
    public Field Field => field;


    private Factory planeFactory;
    private IPlaneCommunicator plane;

    [Header("Events")]
    public UnityEvent WinEvent;
    public UnityEvent LoseEvent;
    private void Start()
    {        
        Field.CreateField();       
        planeFactory = FactoryProducer.GetFactory(FactoryProductType.Planes);
        InitPlane();

        WinEvent.AddListener(() => SpawnConfetti());
    }

    public void InitPlane()
    {
        Vector3 _planeSpawn = Field.PickRandomSpotToSpawn();
        CreatePathForPlane(_planeSpawn, Field.center, Field.StartingDestination);        
        plane = planeFactory.GetProduct((int)PlaneTypes.TwoLettersPlane, _planeSpawn, pathPlane.path);

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
        chosenWords = ICAO.ReturnRandomWordsFromAlphabet(amount);
    }

    public void PathUpdate()
    {
        Vector3 _side = Field.FinishPoint();
        CreatePathForPlane(plane.Position, _side, _side);
        pathPlane.path.TriggerPathUpdate();
    }

    void SpawnConfetti()
    {
        GameObject _confetti = Resources.Load("Prefabs/Confetti") as GameObject;
        foreach (Vector3 _pos in Field.ReturnCornersOfField(2))
        {
            ParticleSystem _conf = Instantiate(_confetti).GetComponent<ParticleSystem>(); ;
            _conf.transform.position = _pos;

            Vector3 _dir = (Field.center - _pos).normalized;
            Quaternion _targetRot = Quaternion.LookRotation(_dir);
            _conf.transform.rotation = Quaternion.Slerp(_conf.transform.rotation, _targetRot, 10);
        }
    }
}
