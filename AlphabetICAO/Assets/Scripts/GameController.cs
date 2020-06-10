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
    private void Start()
    {        
        Field.CreateField();       
        planeFactory = FactoryProducer.GetFactory(FactoryProductType.Planes);
        InitPlane();
    }

    public void InitPlane()
    {
        Vector3 _planeSpawn = Field.PickRandomSpotToSpawn();
        CreatePathForPlane(_planeSpawn, Field.center, Field.Destination);        
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

    private void Update()
    {
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


}
