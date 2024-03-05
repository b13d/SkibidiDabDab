using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SaveOut : MonoBehaviour
{
    [SerializeField] private ButtonsEvents _buttonsEvents = null;
    
    public void Save()
    {
        YandexGame.NewLeaderboardScores("Records", YandexGame.savesData.maxRecord);  
        YandexGame.savesData.energy = GameManager.instance.GetMoney;
        YandexGame.savesData.energyInClick = GameManager.instance.GetMoneyInClick;
        YandexGame.savesData.energyInSecond = GameManager.instance.GetMoneyInSecond;
        YandexGame.savesData.maxRecord = GameManager.instance.GetMaxRecord;
        YandexGame.savesData.timerToUnblockReward = _buttonsEvents.GetTimerToUnblockReward;

        YandexGame.savesData.achievements.buy = GameManager.instance.CountBuy;
        YandexGame.savesData.achievements.click = GameManager.instance.CountClick;
        YandexGame.savesData.achievements.spend = GameManager.instance.CountSpend;
        YandexGame.savesData.achievements.achievementsCompleted = GameManager.instance.ArrCountAchievementsCompleted;
        YandexGame.savesData.achievements.haveMoney = GameManager.instance.CountHaveMoney;
        YandexGame.savesData.achievements.playTime = GameManager.instance.CountPlayTime;
        YandexGame.savesData.achievements.thingBuy = GameManager.instance.CountThingBuy;

        YandexGame.savesData.firstBuyThing = GameManager.instance.LocalFirstBuyThing;
        YandexGame.savesData.percentAddThings = GameManager.instance.LocalPercentAddThings;
        YandexGame.savesData.priceThings = GameManager.instance.LocalPriceThings;
        
        YandexGame.SaveProgress();
        
        Debug.Log("Сохранение рекорда в SaveOut");
    }
}
