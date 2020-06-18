using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDControl : Singleton<HUDControl> {
    InputFieldWords inputPanel;

    void Awake()
    {
        inputPanel = gameObject.GetComponentInChildren<InputFieldWords>();
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
    //IEnumerator ShowVerdict(GameObject text)
    //{
    //    text.SetActive(true);
    //    yield return new WaitForSeconds(0.5f);
    //    text.SetActive(false);
    //}
}
