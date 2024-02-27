using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Click : MonoBehaviour
{
    private float _second = 1f;

    [SerializeField] private TextMeshProUGUI _txtRewardButton;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private GameObject _panel;

    private void Start()
    {
        if (YandexGame.savesData.wasShowReward)
        {
            _rewardButton.interactable = false;
            _panel.SetActive(true);
            
            int timer = YandexGame.savesData.timerToUnblockReward;
            
            int seconds = timer - Mathf.RoundToInt(timer / 60) * 60;
            
            _txtRewardButton.text = $"{timer / 60} мин {seconds} сек";
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Dekstop();
        }

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

    void Dekstop()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up);

        //Debug.Log(hit.collider.name);
    }

    public void DisabledButtonReward()
    {
        _panel.SetActive(true);
        _rewardButton.interactable = false;
        YandexGame.savesData.wasShowReward = true;
    }
}