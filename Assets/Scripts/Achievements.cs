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

    float _timeToAchievement = 60f;


    void Start()
    {
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

            if (YandexGame.savesData.achievements.achievementsCompleted[i] == 1)
            {
                newAchievement.GetComponent<Achievement>().SliderProgress.value = _targetAchievements[i];
                newAchievement.GetComponent<Achievement>().CurrentProgress.text =
                    newAchievement.GetComponent<Achievement>().SliderProgress.maxValue.ToString();
                newAchievement.GetComponent<Achievement>().Finalized = true;

                var newBackGround = newAchievement.GetComponent<Achievement>().BackGround;
                newBackGround.color = Color.red;
                newAchievement.GetComponent<Achievement>().BackGround = newBackGround;
            }

            _achievements.Add(newAchievement.GetComponent<Achievement>());
        }
    }


    private void Update()
    {
        if (YandexGame.savesData.achievements.playTime < int.Parse(_achievements[3].TargetProgress.text))
        {
            _timeToAchievement -= Time.deltaTime;

            if (_timeToAchievement <= 0)
            {
                _timeToAchievement = 60f;

                _achievements[3].SliderProgress.value += 1;
                _achievements[3].CurrentProgress.text += 1;

                YandexGame.savesData.achievements.playTime += 1;

                YandexGame.SaveProgress();
                // подсчет времени наигранного для достижения   
            }
        }


        for (int i = 0; i < _achievements.Count; i++)
        {
            if (i == 0 || i == 5 && !_achievements[i].Finalized)
            {
                if (CheckValueTarget(YandexGame.savesData.achievements.click, i))
                {
                    _achievements[i].SliderProgress.value = YandexGame.savesData.achievements.click;
                    _achievements[i].CurrentProgress.text = YandexGame.savesData.achievements.click.ToString();
                }
            }

            if (i == 1 || i == 6 && !_achievements[i].Finalized)
            {
                if (CheckValueTarget(YandexGame.savesData.achievements.buy, i))
                {
                    _achievements[i].SliderProgress.value = YandexGame.savesData.achievements.buy;
                    _achievements[i].CurrentProgress.text = YandexGame.savesData.achievements.buy.ToString();
                }
            }

            if (i == 2 || i == 4 && !_achievements[i].Finalized)
            {
                if (CheckValueTarget(YandexGame.savesData.achievements.spend, i))
                {
                    _achievements[i].SliderProgress.value = YandexGame.savesData.achievements.spend;
                    _achievements[i].CurrentProgress.text = YandexGame.savesData.achievements.spend.ToString();
                }
            }

            if (i == 7 || i == 8 && !_achievements[i].Finalized)
            {
                if (CheckValueTarget(YandexGame.savesData.energy, i) && !_achievements[i].Finalized)
                {
                    _achievements[i].SliderProgress.value = YandexGame.savesData.energy;
                    _achievements[i].CurrentProgress.text = YandexGame.savesData.energy.ToString();
                }
            }

            if (i == 3 && !_achievements[i].Finalized)
            {
                if (CheckValueTarget(YandexGame.savesData.achievements.playTime, i))
                {
                    _achievements[i].SliderProgress.value = YandexGame.savesData.achievements.playTime;
                    _achievements[i].CurrentProgress.text = YandexGame.savesData.achievements.playTime.ToString();
                }
            }

            if (i == _achievements.Count - 1 && !_achievements[i].Finalized)
            {
                if (CheckValueTarget(YandexGame.savesData.achievements.thingBuy, i))
                {
                    _achievements[i].SliderProgress.value = YandexGame.savesData.achievements.thingBuy;
                    _achievements[i].CurrentProgress.text = YandexGame.savesData.achievements.thingBuy.ToString();
                }
            }

            if (i == _achievements.Count - 1)
            {
                YandexGame.SaveProgress();
            }
        }

        bool CheckValueTarget(int value, int index)
        {
            if (int.Parse(_achievements[index].TargetProgress.text) <= value)
            {
                if (!_achievements[index].Finalized)
                {
                    _achievements[index].Finalized = true;
                    _achievements[index].CurrentProgress.text = _achievements[index].TargetProgress.text;
                    _achievements[index].SliderProgress.value = int.Parse(_achievements[index].TargetProgress.text);

                    var newBackGround = _achievements[index].BackGround;
                    newBackGround.color = Color.red;
                    _achievements[index].BackGround = newBackGround;

                    YandexGame.savesData.achievements.achievementsCompleted[index] = 1;

                    _notification.CreateNewNotification(_achievements[index].ImageAchievement,
                        _achievements[index].TextNameAchievement.text, _achievements[index].CurrentProgress.text);
                }

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}