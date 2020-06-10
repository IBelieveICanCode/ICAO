using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneLetterPlane : Plane
{
    public override void Init()
    {
        speed = 1f;
        ChooseLettersForText();
    }
}
