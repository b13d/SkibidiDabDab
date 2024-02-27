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
    [SerializeField]
    private bool _isOpenShop = false;

    [SerializeField]
    private bool _isShopMoving = false;

    [SerializeField]
    private bool _isOpenAchievements = false;

    [SerializeField]
    private bool _isAchievementsMoving = false;

    private float _yShopOpen = -225f;
    private float _yShopClose = -1190f;

    private float _xAchievementsOpen = 266f;
    private float _xAchievementsClose = 762f;

    [SerializeField] private List<AudioClip> _audioClips = new List<AudioClip>();
    
    private AudioSource _audio;

    [SerializeField]
    GameObject _shop = null;

    [SerializeField]
    GameObject _achievements = null;

    [SerializeField]
    GameObject _shopBG = null;

    [SerializeField]
    GameObject _contentShop = null;

    [SerializeField]
    GameObject _contentAchievements = null;
    
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
                Vector3 targetPos = new Vector3(_shop.transform.localPosition.x, _yShopOpen, _shop.transform.localPosition.z);

                _shop.transform.localPosition = Vector3.Lerp(_shop.transform.localPosition, targetPos, .1f);
            }
            else
            {
                Vector3 targetPos = new Vector3(_shop.transform.localPosition.x, _yShopClose, _shop.transform.localPosition.z);

                _shop.transform.localPosition = Vector3.Lerp(_shop.transform.localPosition, targetPos, .1f);
            }
        }

        if (_isAchievementsMoving)
        {

            if (_isOpenAchievements)
            {
                Vector3 targetPos = new Vector3(_xAchievementsOpen, _achievements.transform.localPosition.y,
                    _achievements.transform.localPosition.z);

                _achievements.transform.localPosition =
                    Vector3.Lerp(_achievements.transform.localPosition, targetPos, .1f);
            }
            else
            {
                Vector3 targetPos = new Vector3(_xAchievementsClose, _achievements.transform.localPosition.y,
                    _achievements.transform.localPosition.z);

                _achievements.transform.localPosition =
                    Vector3.Lerp(_achievements.transform.localPosition, targetPos, .1f);
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
