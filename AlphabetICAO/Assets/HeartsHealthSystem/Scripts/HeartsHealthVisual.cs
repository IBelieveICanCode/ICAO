/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */
using System;
using System.Collections;
using System.Collections.Generic;
using Adminka;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class HeartsHealthVisual : MonoBehaviour {

    public static HeartsHealthSystem heartsHealthSystemStatic;

    [SerializeField] private Sprite heart0Sprite;
    [SerializeField] private Sprite heart1Sprite;
    [SerializeField] private AnimationClip heartFullAnimationClip;

    private List<HeartImage> heartImageList;
    private HeartsHealthSystem heartsHealthSystem;
    private bool isHealing;

    private IDisposable _update;

    private void Awake() {
        heartImageList = new List<HeartImage>();
    }

    private void Start() {
        _update = Observable.Interval(TimeSpan.FromMilliseconds(50)).Subscribe(x => HealingAnimatedPeriodic());
        HeartsHealthSystem _heartsHealthSystem = new HeartsHealthSystem(3);
        SetHeartsHealthSystem(_heartsHealthSystem);       
    }

    public void SetHeartsHealthSystem(HeartsHealthSystem heartsHealthSystem) {
        this.heartsHealthSystem = heartsHealthSystem;
        heartsHealthSystemStatic = heartsHealthSystem;

        List<HeartsHealthSystem.Heart> _heartList = heartsHealthSystem.GetHeartList();
        int _row = 0;
        int _col = 0;
        const int colMax = 10;
        const float rowColSize = 30f;

        foreach (var heart in _heartList)
        {
            Vector2 _heartAnchoredPosition = new Vector2(_col * rowColSize, -_row * rowColSize);
            CreateHeartImage(_heartAnchoredPosition).SetHeartFraments(heart.GetFragmentAmount());

            _col++;
            if (_col >= colMax) {
                _row++;
                _col = 0;
            }
        }

        heartsHealthSystem.OnDamaged += HeartsHealthSystem_OnDamaged;
        heartsHealthSystem.OnHealed += HeartsHealthSystem_OnHealed;
        heartsHealthSystem.OnDead += HeartsHealthSystem_OnDead;
    }

    private void HeartsHealthSystem_OnDead(object sender, System.EventArgs e) {
        GameController.Instance.DefeatEvent?.Invoke();
    }

    private void HeartsHealthSystem_OnHealed(object sender, System.EventArgs e) {
        // Hearts health system was healed
        //RefreshAllHearts();
        isHealing = true;
    }

    private void HeartsHealthSystem_OnDamaged(object sender, System.EventArgs e) {
        // Hearts health system was damaged
        RefreshAllHearts();
    }

    private void RefreshAllHearts() {
        List<HeartsHealthSystem.Heart> _heartList = heartsHealthSystem.GetHeartList();
        for (int i = 0; i < heartImageList.Count; i++) {
            HeartImage _heartImage = heartImageList[i];
            HeartsHealthSystem.Heart _heart = _heartList[i];
            _heartImage.SetHeartFraments(_heart.GetFragmentAmount());
        }
    }

    private void HealingAnimatedPeriodic()
    {
        if (!isHealing) return;
        bool _fullyHealed = true;
        List<HeartsHealthSystem.Heart> _heartList = heartsHealthSystem.GetHeartList();
        for (int i = 0; i < _heartList.Count; i++) {
            HeartImage _heartImage = heartImageList[i];
            HeartsHealthSystem.Heart _heart = _heartList[i];
            if (_heartImage.GetFragmentAmount() != _heart.GetFragmentAmount()) {
                // Visual is different from logic
                _heartImage.AddHeartVisualFragment();
                if (_heartImage.GetFragmentAmount() == HeartsHealthSystem.MAX_FRAGMENT_AMOUNT) {
                    // This heart was fully healed
                    _heartImage.PlayHeartFullAnimation();
                }
                _fullyHealed = false;
                break;
            }
        }
        if (_fullyHealed) {
            isHealing = false;
        }
    }

    private HeartImage CreateHeartImage(Vector2 anchoredPosition) {
        // Create Game Object
        GameObject _heartGameObject = new GameObject("Heart", typeof(Image), typeof(Animation));

        // Set as child of this transform
        _heartGameObject.transform.parent = transform;
        _heartGameObject.transform.localPosition = Vector3.zero;
        _heartGameObject.transform.localScale = Vector3.one;

        // Locate and Size heart
        _heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        _heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 35);

        _heartGameObject.GetComponent<Animation>().AddClip(heartFullAnimationClip, "HeartFull");

        // Set heart sprite
        Image _heartImageUi = _heartGameObject.GetComponent<Image>();
        _heartImageUi.sprite = heart1Sprite;

        HeartImage _heartImage = new HeartImage(this, _heartImageUi, _heartGameObject.GetComponent<Animation>());
        heartImageList.Add(_heartImage);

        return _heartImage;
    }


    // Represents a single Heart
    private class HeartImage {

        private int fragments;
        private Image heartImage;
        private HeartsHealthVisual heartsHealthVisual;
        private Animation animation;

        public HeartImage(HeartsHealthVisual heartsHealthVisual, Image heartImage, Animation animation) {
            this.heartsHealthVisual = heartsHealthVisual;
            this.heartImage = heartImage;
            this.animation = animation;
        }

        public void SetHeartFraments(int fragments) {
            this.fragments = fragments;
            switch (fragments) {
            case 0: heartImage.sprite = heartsHealthVisual.heart0Sprite; break;
            case 1: heartImage.sprite = heartsHealthVisual.heart1Sprite; break;
            }
        }

        public int GetFragmentAmount() {
            return fragments;
        }

        public void AddHeartVisualFragment() {
            SetHeartFraments(fragments + 1);
        }

        public void PlayHeartFullAnimation() {
            animation.Play("HeartFull", PlayMode.StopAll);
        }
    }


}
