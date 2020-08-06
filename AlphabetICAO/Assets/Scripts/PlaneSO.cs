using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreatePlane/Create New Plane", fileName = "New Plane")]
public class PlaneSO : ScriptableObject
{
    [SerializeField] private Plane plane;
    public Plane Plane => plane;
    
}
