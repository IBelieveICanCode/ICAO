using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "CreateGameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField]
    private GameObject planePrefab, finishPefab;
    public GameObject PlanePrefab => planePrefab;
    public GameObject FinishPrefab => finishPefab;
}