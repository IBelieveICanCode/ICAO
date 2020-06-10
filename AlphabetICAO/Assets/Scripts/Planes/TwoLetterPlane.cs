using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoLetterPlane : Plane
{
    public override void Init()
    {
        speed = 2f;
        ChooseLettersForText();
    }
}
