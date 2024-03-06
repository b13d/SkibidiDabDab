using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Achievements : MonoBehaviour
{
    [SerializeField] private List<string> _nameAchievements = new List<string>();

    [SerializeField] private List<int> _targetAchievements = new List<int>();

    [SerializeField] private List<Sprite> _spriteAchievements = new List<Sprite>();

    [SerializeField] private GameObject _achievementPrefab = null;

    [SerializeField] private GameObject _placeAchievements = null;

    [SerializeField] private List<Achievement> _achievements = new List<Achievement>();

    [SerializeField] private NotificationsMain _notification = null;

    private AudioSource _audio;

    float _timeToAchievement = 60f;

    private float _second = 2f;


    void Start()
    {
        _audio = GetComponent<AudioSource>();

        CreateAchievements();
    }


    void CreateAchievements()
    {
        for (int i = 0; i < _nameAchievements.Count; i++)
        {
            var newAchievement = Instantiate(_achievementPrefab, transform.position, Quaternion.identity,
                _placeAchievements.transform);
            newAchievement.GetComponent<Achievement>().TextNameAchievement.text = _nameAchievements[i];
            newAchievement.GetComponent<Achievement>().ImageAchievement.sprite = _spriteAchievements[i];
            newAchievement.GetComponent<Achievement>().TargetProgress.text = _targetAchievements[i].ToString();
            newAchievement.GetComponent<Achievement>().SliderProgress.maxValue = _targetAchievements[i];
            newAchievement.GetComponent<Achievement>().CurrentProgress.text = "0";
            newAchievement.GetComponent<Animator>().SetTrigger("isRed");


            if (YandexGame.savesData.achievements.achievementsCompleted[i] == 1)
            {
                newAchievement.GetComponent<Achievement>().SliderProgress.value = _targetAchievements[i];
                newAchievement.GetComponent<Achievement>().CurrentProgress.text =
                    newAchievement.GetComponent<Achievement>().SliderProgress.maxValue.ToString();
                newAchievement.GetComponent<Achievement>().Finalized = true;

                // var newBackGround = newAchievement.GetComponent<Achievement>().BackGround;
                // newBackGround.color = Color.grey;
                // newAchievement.GetComponent<Achievement>().BackGround = newBackGround;
                newAchievement.GetComponent<Animator>().SetTrigger("isGreen");
            }

            _achievements.Add(newAchievement.GetComponent<Achievement>());
        }
    }


    private void Update()
    {
        CheckAchievements();

        if (GameManager.instance.CountPlayTime < int.Parse(_achievements[3].TargetProgress.text))
            // if (YandexGame.savesData.achievements.playTime < int.Parse(_achievements[3].TargetProgress.text))
        {
            _timeToAchievement -= Time.deltaTime;

            if (_timeToAchievement <= 0)
            {
                _timeToAchievement = 60f;

                _achievements[3].SliderProgress.value += 1;
                _achievements[3].CurrentProgress.text += 1;

                GameManager.instance.CountPlayTime += 1;
                // YandexGame.savesData.achievements.playTime += 1;

                // подсчет времени наигранного для достижения   
            }
        }
    }


    void CheckAchievements()
    {
        for (int i = 0; i < _achievements.Count; i++)
        {
            if (i == 0 || i == 5 && !_achievements[i].Finalized)
            {
                if (CheckValueTarget(GameManager.instance.CountClick, i))
                    // if (CheckValueTarget(YandexGame.savesData.achievements.click, i))
                {
                    _achievements[i].SliderProgress.value = GameManager.instance.CountClick;
                    _achievements[i].CurrentProgress.text = GameManager.instance.CountClick.ToString();

                    // _achievements[i].SliderProgress.value = YandexGame.savesData.achievements.click;
                    // _achievements[i].CurrentProgress.text = YandexGame.savesData.achievements.click.ToString();
                }
            }

            if (i == 1 || i == 6 && !_achievements[i].Finalized)
            {
                if (CheckValueTarget(GameManager.instance.CountBuy, i))
                    // if (CheckValueTarget(YandexGame.savesData.achievements.buy, i))
                {
                    _achievements[i].SliderProgress.value = GameManager.instance.CountBuy;
                    _achievements[i].CurrentProgress.text = GameManager.instance.CountBuy.ToString();

                    // _achievements[i].SliderProgress.value = YandexGame.savesData.achievements.buy;
                    // _achievements[i].CurrentProgress.text = YandexGame.savesData.achievements.buy.ToString();
                }
            }

            if (i == 2 || i == 4 && !_achievements[i].Finalized)
            {
                // if (CheckValueTarget(YandexGame.savesData.achievements.spend, i))

                if (CheckValueTarget(GameManager.instance.CountSpend, i))
                {
                    _achievements[i].SliderProgress.value = GameManager.instance.CountSpend;
                    _achievements[i].CurrentProgress.text = GameManager.instance.CountSpend.ToString();

                    // _achievements[i].SliderProgress.value = YandexGame.savesData.achievements.spend;
                    // _achievements[i].CurrentProgress.text = YandexGame.savesData.achievements.spend.ToString();
                }
            }

            if (i == 7 || i == 8 && !_achievements[i].Finalized)
            {
                // if (CheckValueTarget(YandexGame.savesData.energy, i) && !_achievements[i].Finalized)


                if (CheckValueTarget(GameManager.instance.GetMoney, i) && !_achievements[i].Finalized)
                {
                    _achievements[i].SliderProgress.value = GameManager.instance.GetMoney;
                    _achievements[i].CurrentProgress.text = GameManager.instance.GetMoney.ToString();

                    // _achievements[i].SliderProgress.value = YandexGame.savesData.energy;
                    // _achievements[i].CurrentProgress.text = YandexGame.savesData.energy.ToString();
                }
            }

            if (i == 3 && !_achievements[i].Finalized)
            {
                // if (CheckValueTarget(YandexGame.savesData.achievements.playTime, i))


                if (CheckValueTarget(GameManager.instance.CountPlayTime, i))
                {
                    _achievements[i].SliderProgress.value = GameManager.instance.CountPlayTime;
                    _achievements[i].CurrentProgress.text = GameManager.instance.CountPlayTime.ToString();

                    // _achievements[i].SliderProgress.value = YandexGame.savesData.achievements.playTime;
                    // _achievements[i].CurrentProgress.text = YandexGame.savesData.achievements.playTime.ToString();
                }
            }

            if (i == _achievements.Count - 1 && !_achievements[i].Finalized)
            {
                // if (CheckValueTarget(YandexGame.savesData.achievements.thingBuy, i))


                if (CheckValueTarget(GameManager.instance.CountThingBuy, i))
                {
                    _achievements[i].SliderProgress.value = GameManager.instance.CountThingBuy;
                    _achievements[i].CurrentProgress.text = GameManager.instance.CountThingBuy.ToString();

                    // _achievements[i].SliderProgress.value = YandexGame.savesData.achievements.thingBuy;
                    // _achievements[i].CurrentProgress.text = YandexGame.savesData.achievements.thingBuy.ToString();
                }
            }
        }
    }

    bool CheckValueTarget(int value, int index)
    {
        if (int.Parse(_achievements[index].TargetProgress.text) <= value)
        {
            
            if (!_achievements[index].Finalized)
            {
                _audio.Play();

                _achievements[index].Finalized = true;
                _achievements[index].CurrentProgress.text = _achievements[index].TargetProgress.text;
                _achievements[index].SliderProgress.value = int.Parse(_achievements[index].TargetProgress.text);

                // var newBackGround = _achievements[index].BackGround;
                // newBackGround.color = Color.grey;
                // _achievements[index].BackGround = newBackGround;

                GameManager.instance.ArrCountAchievementsCompleted[index] = 1;

                // YandexGame.savesData.achievements.achievementsCompleted[index] = 1;

                _notification.CreateNewNotification(_achievements[index].ImageAchievement,
                    _achievements[index].TextNameAchievement.text, _achievements[index].CurrentProgress.text);
            }
            else
            {
                _achievements[index].CurrentProgress.text = _achievements[index].TargetProgress.text;
                _achievements[index].SliderProgress.value = int.Parse(_achievements[index].TargetProgress.text);
                
                _achievements[index].GetComponent<Animator>().SetTrigger("isGreen");
            }

            return false;
        }
        else
        {
            return true;
        }
    }
}