using Adminka;
using PathCreation;
using PathCreation.Follower;
using TMPro;
using UnityEngine;
using Zenject;

    [RequireComponent(typeof(Rigidbody2D))]
    public class Plane : MonoBehaviour, IPlaneCommunicator
    {
        private int wordsAmount;
        public int WordsAmount => wordsAmount;
        [SerializeField]
        protected TMP_Text lettersText;

        [SerializeField]
        private PathFollower pathFollower;
        public PathFollower PathFollower => pathFollower;

        public Vector3 Position => transform.position;

        [SerializeField]
        protected float speed;
        public float Speed => speed;

        [Inject]
        protected GameController gameController;

        public Vector3 LetterPosition => transform.localPosition + (Vector3.up * 1.5f);

        [Inject]
        public void Construct(LevelProgress level, Vector3 spawnPos, PathCreator path)
        {
            speed = level.Speed;
            transform.position = spawnPos;
            pathFollower.pathCreator = path;
            wordsAmount = (int)level.PlaneTypes;
        }
        private void Start()
        {
            gameController.WrongEvent.AddListener(EndGame);
            gameController.CorrectEvent.AddListener(EndGame);
            Init();
        }

        protected virtual void Init()
        {
            gameController.SetUpWords(WordsAmount);
            ChooseLettersForText();     
        }

        private void Update()
        {
            PathFollower.FollowPath(speed);
        }

        protected void ChooseLettersForText()
        {
            lettersText.text = null;
            foreach (string _letter in gameController.ChosenWords.Keys)
            {
                lettersText.text += _letter + "  ";
            }
        }

        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
        }
        
        protected void EndGame()
        {   
            Destroy(gameObject);
            gameController.WrongEvent.RemoveListener(() => EndGame());
            gameController.CorrectEvent.RemoveListener(() => EndGame());
        }

        public void Accelerate()
        {
            speed = 5f;
        }

    public class PlaneFactory : PlaceholderFactory<LevelProgress, Vector3, PathCreator, Plane>
    {
    }

}

