using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

enum SoundsName
{
    ChestOpen = 0,
    ChestClose,
    Achievements
}

public class ButtonsEvents : MonoBehaviour
{
    [SerializeField] private bool _isOpenShop = false;

    [SerializeField] private bool _isShopMoving = false;

    [SerializeField] private bool _isOpenAchievements = false;

    [SerializeField] private bool _isAchievementsMoving = false;

    private float _yShopOpen = 0;
    private float _yShopClose = -1000f;

    private float _xAchievementsOpen = 500f;
    private float _xAchievementsClose = 0f;

    [SerializeField] private List<AudioClip> _audioClips = new List<AudioClip>();

    private AudioSource _audio;

    [SerializeField] GameObject _shop = null;

    [SerializeField] GameObject _achievements = null;

    [SerializeField] GameObject _shopBG = null;

    [SerializeField] GameObject _contentShop = null;

    [SerializeField] GameObject _contentAchievements = null;

    private float _second = 1f;

    [SerializeField] private TextMeshProUGUI _txtRewardButton;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private GameObject _panel;

    public void Achievements()
    {
        Vector3 posContent = _contentAchievements.transform.localPosition;
        _contentAchievements.transform.localPosition = new Vector3(posContent.x, 0, posContent.z);

        _isAchievementsMoving = true;

        _isOpenAchievements = !_isOpenAchievements;
    }

    private void Start()
    {
        _audio = GetComponent<AudioSource>();

        if (YandexGame.savesData.wasShowReward)
        {
            _rewardButton.interactable = false;
            _panel.SetActive(true);

            int timer = YandexGame.savesData.timerToUnblockReward;

            int seconds = timer - Mathf.RoundToInt(timer / 60) * 60;

            _txtRewardButton.text = $"{timer / 60} мин {seconds} сек";
        }
    }


    public void Shop()
    {
        Vector3 posContent = _contentShop.transform.localPosition;
        _contentShop.transform.localPosition = new Vector3(posContent.x, 0, posContent.z);

        _isShopMoving = true;

        _isOpenShop = !_isOpenShop;

        if (_isOpenShop)
        {
            _shopBG.SetActive(true);
            _audio.PlayOneShot(_audioClips[(int)SoundsName.ChestOpen]);
        }
        else
        {
            _shopBG.SetActive(false);
            _audio.PlayOneShot(_audioClips[(int)SoundsName.ChestClose]);
        }
    }

    private void FixedUpdate()
    {
        if (_isShopMoving)
        {
            if (_isOpenShop)
            {
                var posAnchored = _shop.GetComponent<RectTransform>().anchoredPosition;

                Vector2 targetPos = new Vector2(posAnchored.x, _yShopOpen);

                _shop.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(posAnchored, targetPos, .1f);
            }
            else
            {
                var posAnchored = _shop.GetComponent<RectTransform>().anchoredPosition;

                Vector2 targetPos = new Vector2(posAnchored.x, _yShopClose);

                _shop.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(posAnchored, targetPos, .1f);
            }
        }

        if (_isAchievementsMoving)
        {
            if (_isOpenAchievements)
            {
                var posAnchored = _achievements.GetComponent<RectTransform>().anchoredPosition;

                Vector2 targetPos = new Vector2(-500f, posAnchored.y);

                _achievements.GetComponent<RectTransform>().anchoredPosition =
                    Vector2.Lerp(posAnchored, targetPos, .1f);

                if (posAnchored == targetPos)
                {
                    _isAchievementsMoving = false;
                }
            }
            else
            {
                var posAnchored = _achievements.GetComponent<RectTransform>().anchoredPosition;

                Vector2 targetPos = new Vector2(0, posAnchored.y);

                _achievements.GetComponent<RectTransform>().anchoredPosition =
                    Vector2.Lerp(posAnchored, targetPos, .1f);

                if (posAnchored == targetPos)
                {
                    _isAchievementsMoving = false;
                }
            }
        }
    }

    public void DisabledButtonReward()
    {
        _panel.SetActive(true);
        _rewardButton.interactable = false;
        YandexGame.savesData.wasShowReward = true;
    }

    private void Update()
    {
        CheckTime();
    }

    void CheckTime()
    {
        if (YandexGame.savesData.wasShowReward)
        {
            _second -= Time.deltaTime;

            if (_second < 0)
            {
                _second = 1f;

                YandexGame.savesData.timerToUnblockReward -= 1;

                int timer = YandexGame.savesData.timerToUnblockReward;

                int seconds = timer - Mathf.RoundToInt(timer / 60) * 60;

                _txtRewardButton.text = $"{timer / 60} мин {seconds} сек";

                // Debug.Log("Текст изменился");

                YandexGame.SaveProgress();

                if (timer <= 0)
                {
                    YandexGame.savesData.wasShowReward = false;

                    YandexGame.savesData.timerToUnblockReward = 300;

                    _panel.SetActive(false);
                    _rewardButton.interactable = true;

                    YandexGame.SaveProgress();
                }
            }
        }
    }
}