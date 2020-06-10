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
            GameController.Instance.PathUpdate();       
    }

    //answerInput.text.ToLower().Equals(ICAO.alphabet[letter].ToLower())
    public void AskPlayer()
    {
        inputPanel.CreateAndSetInputFields();
    }

    //IEnumerator ShowVerdict(GameObject text)
    //{
    //    text.SetActive(true);
    //    yield return new WaitForSeconds(0.5f);
    //    text.SetActive(false);
    //}
}
