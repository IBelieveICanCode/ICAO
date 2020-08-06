using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FactorySpace;

[System.Serializable]
public class LevelProgress 
{
    #region Fields
    private int rounds;

    #endregion
    #region Properties
    public int Scores { get; private set; }

    public PlaneTypes PlaneTypes { get; private set; }

    public int Speed { get; private set; }

    public int Rounds
    {
        get => rounds;
        set
        {
            rounds = value;
            DifficultyUp(rounds);
        }
    }
    #endregion
    public void DifficultyUp(int currentRound)
    {
        int _difficultIncrStep = currentRound / 3;
        Scores = _difficultIncrStep * 5;

        if (currentRound % 3 == 0 || _difficultIncrStep >= 3)
            Speed = 3;
        else
            Speed = currentRound % 3;
        if (_difficultIncrStep < 1)
            PlaneTypes = PlaneTypes.OneLetterPlane;
        else if (_difficultIncrStep >= 1 && _difficultIncrStep < 3)
            PlaneTypes = PlaneTypes.TwoLettersPlane;
        else
            PlaneTypes = PlaneTypes.ThreeLettersPlane;

    }


}
