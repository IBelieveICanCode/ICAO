using System.Collections;
using System.Collections.Generic;
using Adminka;
using UnityEngine;

public class ThreeLetterPlane : Plane
{
    protected override void Init()
    {
        GameController.Instance.SetUpWords(3);
        ChooseLettersForText();        
    }
}
