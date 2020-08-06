using System.Collections;
using System.Collections.Generic;
using Adminka;
using UnityEngine;

public class TwoLetterPlane : Plane
{
    protected override void Init()
    {
        GameController.Instance.SetUpWords(2);
        ChooseLettersForText();
    }
}
