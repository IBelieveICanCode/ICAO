using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class FinishSpot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.OnTriggerEnter2DAsObservable()
            .Where(collision  => collision.gameObject.GetComponent<IPlaneCommunicator>() != null)
            .Subscribe(collision => GameController.Instance.CorrectEvent?.Invoke());
    }
}
