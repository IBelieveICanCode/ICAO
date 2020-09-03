using System;
using System.Collections;
using System.Collections.Generic;
using Adminka;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class FinishSpot : MonoBehaviour
{
    [Inject]
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        this.OnTriggerEnter2DAsObservable()
            .Where(collision  => collision.gameObject.GetComponent<IPlaneCommunicator>() != null)
            .Subscribe(collision => gameController.CorrectEvent?.Invoke());
    }

    public class FinishSpotFactory : PlaceholderFactory<FinishSpot>
    {
    }
}


