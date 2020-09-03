using System.Collections;
using System.Collections.Generic;
using Adminka;
using UnityEngine;

public class OneLetterPlane : Plane
{
    protected override void Init()
    {
        gameController.SetUpWords(1);
        ChooseLettersForText();        
    }
}
