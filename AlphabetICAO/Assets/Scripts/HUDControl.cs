using Adminka;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UtilityScripts;
using Zenject;

public class HUDControl : Singleton<HUDControl> {

    [SerializeField] private Image fadeImage;
    private InputFieldWords inputPanel;
    [SerializeField] private TMP_Text roundCount;
    [SerializeField] private RectTransform wordsPanel;

    [Inject]
    private readonly GameController gameController;
    
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
        wordsPanel.DOAnchorPosY(-wordsPanel.rect.y, 0.25f);
    }

    public void HideWordsPanel()
    {
        wordsPanel.DOAnchorPosY(wordsPanel.rect.y, 0.25f);
    }
    private void UpdateRounds(LevelProgress lvlProgress)
    {
        roundCount.text = lvlProgress.Rounds.ToString().ToUpper();
    }

}
