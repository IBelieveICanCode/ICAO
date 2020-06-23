using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDControl : Singleton<HUDControl> {
    
    InputFieldWords inputPanel;
    [SerializeField]
    TMP_Text roundCount;

    void Awake()
    {
        inputPanel = gameObject.GetComponentInChildren<InputFieldWords>();
    }
    private void Start()
    {
        GameController.Instance.ProgressEvent += UpdateRounds;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SubmitButton()
    {
        if (inputPanel.CheckCorrectAnswers())
            GameController.Instance.PathCorrectUpdate();       
    }

    //answerInput.text.ToLower().Equals(ICAO.alphabet[letter].ToLower())
    public void AskPlayer()
    {
        inputPanel.CreateAndSetInputFields();
    }

    public void DiscardHealth()
    {
        HeartsHealthVisual.heartsHealthSystemStatic.Damage(1);
    }
    public void AddHealth()
    {
        HeartsHealthVisual.heartsHealthSystemStatic.Heal(1);
    }

    private void UpdateRounds(LevelProgress lvlProgress)
    {
        roundCount.text = lvlProgress.Rounds.ToString().ToUpper();
    }
    //IEnumerator ShowVerdict(GameObject text)
    //{
    //    text.SetActive(true);
    //    yield return new WaitForSeconds(0.5f);
    //    text.SetActive(false);
    //}
}
