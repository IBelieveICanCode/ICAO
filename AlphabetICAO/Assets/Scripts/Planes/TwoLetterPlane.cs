using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoLetterPlane : Plane
{
    public override void Init()
    {
        GameController.Instance.SetUpWords(2);
        speed = 2f;
        ChooseLettersForText();
        
    }
}
