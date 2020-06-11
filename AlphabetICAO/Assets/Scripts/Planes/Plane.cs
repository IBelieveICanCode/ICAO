using System.Collections;
using UnityEngine;
using PathCreation.Follower;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Plane : MonoBehaviour, IPlaneCommunicator
{
    [SerializeField]
    protected TMP_Text LettersText;
    Rigidbody2D rb;
    protected PathFollower pathFollower;

    public Vector3 Position => transform.position;

    [SerializeField]
    protected float speed;
    public float Speed => speed;

    public Vector3 LetterPosition { get => transform.localPosition + (Vector3.up * 1.5f); /** transform.localScale.y / 2*/ }

    private void Awake()
    {
    }
    private void Start()
    {
        LettersText = GetComponentInChildren<TMP_Text>();
        pathFollower = GetComponent<PathFollower>();
        Init();
        GameController.Instance.LoseEvent.AddListener(() => EndGame());
        GameController.Instance.WinEvent.AddListener(() => EndGame());
    }

    public abstract void Init();

    private void Update()
    {
        pathFollower.FollowPath(speed);
    }

    public void ChooseLettersForText()
    {
        LettersText.text = null;
        foreach (string _letter in GameController.Instance.ChosenWords.Keys)
        {
            LettersText.text += _letter + "  ";
        }
    }

    protected void EndGame()
    {
        Destroy(gameObject);
        GameController.Instance.LoseEvent.RemoveListener(() => EndGame());
        GameController.Instance.WinEvent.RemoveListener(() => EndGame());
    }
    
    //public IEnumerator Move()
    //{
    //    while (t <= 1)
    //    {
    //        t += Time.deltaTime * 0.05f;
    //        planePos = Bezier.QuardaticBezierPoint(t, spawnPos, center, finishPoint);
    //        rb.MovePosition(planePos);

    //        float origt = t - (Time.deltaTime * 0.01f);
    //        origPos = Bezier.QuardaticBezierPoint(origt, spawnPos, center, finishPoint);
    //        float deltaX = origPos.x - rb.transform.position.x;
    //        float deltaY = origPos.y - rb.transform.position.y;
    //        float degree = Mathf.Atan2(deltaY, deltaX);
    //        float angle = (degree * 180 / 3.14f) - 90;
    //        if (angle < 0)
    //        {
    //            angle = 360 + angle;
    //        }
    //        rb.MoveRotation(angle);
    //        yield return new WaitForEndOfFrame();            
    //    }
    //}
}
