using System.Collections;
using System.Collections.Generic;
using Adminka;
using UnityEngine;

public class ThreeLetterPlane : Plane
{
    protected override void Init()
    {
        gameController.SetUpWords(3);
        ChooseLettersForText();        
    }
}
