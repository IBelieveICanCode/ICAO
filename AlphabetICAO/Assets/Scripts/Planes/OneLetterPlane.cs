using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneLetterPlane : Plane
{
    public override void Init()
    {
        GameController.Instance.SetUpWords(1);
        speed = 1f;
        ChooseLettersForText();        
    }
}
