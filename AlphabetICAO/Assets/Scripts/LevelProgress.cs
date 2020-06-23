using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelProgress 
{
    int rounds;
    int scores;
    FactorySpace.PlaneTypes planeTypes;
    int speed;

    public int Scores => scores;
    public FactorySpace.PlaneTypes PlaneTypes => planeTypes;
    public int Speed => speed;

    public int Rounds
    {
        get
        {
            return rounds;
        }
        set
        {
            rounds = value;
            DifficultyUp(rounds);
        }
    }
    public void DifficultyUp(int crntRound)
    {
        //Debug.Log(crntRound);
        int _difficultIncrStep = crntRound / 3;
        scores = _difficultIncrStep * 5;

        if (crntRound % 3 == 0 || _difficultIncrStep >= 3)
            speed = 3;
        else
            speed = crntRound % 3;

        Debug.Log(_difficultIncrStep);
        if (_difficultIncrStep < 2)
            planeTypes = FactorySpace.PlaneTypes.OneLetterPlane;
        else if (_difficultIncrStep >= 2 && _difficultIncrStep < 4)
            planeTypes = FactorySpace.PlaneTypes.TwoLettersPlane;

    }


}
