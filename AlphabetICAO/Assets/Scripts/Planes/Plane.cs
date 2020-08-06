using Adminka;
using PathCreation.Follower;
using TMPro;
using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    public class Plane : MonoBehaviour, IPlaneCommunicator
    {
        public int WordsAmount;
        [SerializeField]
        protected TMP_Text lettersText;

        [SerializeField]
        private PathFollower pathFollower;

        public PathFollower PathFollower => pathFollower;

        public Vector3 Position => transform.position;

        [SerializeField]
        protected float speed;
        public float Speed => speed;

        public Vector3 LetterPosition => transform.localPosition + (Vector3.up * 1.5f);

        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            GameController.Instance.WrongEvent.AddListener(EndGame);
            GameController.Instance.CorrectEvent.AddListener(EndGame);
            speed = GameController.Instance.LvlProgress.Speed;
        }

        protected virtual void Init()
        {
            GameController.Instance.SetUpWords(WordsAmount);
            ChooseLettersForText();     
        }

        private void Update()
        {
            PathFollower.FollowPath(speed);
        }

        protected void ChooseLettersForText()
        {
            lettersText.text = null;
            foreach (string _letter in GameController.Instance.ChosenWords.Keys)
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
            GameController.Instance.WrongEvent.RemoveListener(() => EndGame());
            GameController.Instance.CorrectEvent.RemoveListener(() => EndGame());
        }

        void IPlaneCommunicator.Accelerate()
        {
            speed = 5f;
        }
    }

