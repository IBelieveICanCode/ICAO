﻿using Adminka;
using UnityEngine;
using UnityEngine.UI;
using UtilityScripts;
using TMPro;
using DG.Tweening;
using Zenject;

public class HUDControl : Singleton<HUDControl> {

    [SerializeField] private Image fadeImage;
    private InputFieldWords inputPanel;
    [SerializeField] private TMP_Text roundCount;
    [SerializeField] private RectTransform setUpWordsPanel;

    [Inject]
    private GameController gameController;
    
    void Awake()
    {
        inputPanel = gameObject.GetComponentInChildren<InputFieldWords>();
    }
    private void Start()
    {
        gameController.ProgressEvent += UpdateRounds;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SubmitButton()
    {
        if (inputPanel.CheckIfAnswersCorrect())
            gameController.AnswerCorrectUpdatePath();
        else
            gameController.AnswerNotCorrect();
    }

    public void Defeat()
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(Utility.Fade(3, fadeImage));
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

    public void ShowWordsPanel()
    {
        setUpWordsPanel.DOAnchorPos(Vector2.zero, 0.25f);
    }

    public void HideWordsPanel()
    {
        setUpWordsPanel.DOAnchorPos(new Vector2(0, -setUpWordsPanel.rect.height), 0.25f);
    }
    private void UpdateRounds(LevelProgress lvlProgress)
    {
        roundCount.text = lvlProgress.Rounds.ToString().ToUpper();
    }

}
