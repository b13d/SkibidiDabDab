using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private float _time = 1f;
    
    [SerializeField]
    private TextMeshProUGUI _txtScore;

    [SerializeField]
    private TextMeshProUGUI _txtInSecond;

    [SerializeField]
    private TextMeshProUGUI _txtInClick;

    private bool _doubleBonus;

    private int localMoney = 0;
    private int localMoneyInSecond = 1;
    private int localMoneyInClick = 1;
    private int localMaxRecord = 0;

    private int countClick = 0;
    private int countSpend = 0;
    private int countBuy = 0;
    private int countHaveMoney = 0;
    private int countPlayTime = 0;
    private int countThingBuy = 0;
    private int[] arrCountAchievementsCompleted = new int[10];
    
    private int[] localPriceThings = new int[15];
    private int[] localPercentAddThings = new int[15];
    private int[] localFirstBuyThing = new int[15];

    public bool _isPause = false;

    public int[] LocalPriceThings
    {
        get { return localPriceThings; }
        set { localPriceThings = value; }
    }
    
    public int[] LocalPercentAddThings
    {
        get { return localPercentAddThings; }
        set { localPercentAddThings = value; }
    }
    
    public int[] LocalFirstBuyThing
    {
        get { return localFirstBuyThing; }
        set { localFirstBuyThing = value; }
    }
    
    public int[] ArrCountAchievementsCompleted
    {
        get { return arrCountAchievementsCompleted; }
        set { arrCountAchievementsCompleted = value; }
    }
    
    public int CountSpend
    {
        get { return countSpend; }
        set { countSpend = value; }
    }
    
    public int CountBuy
    {
        get { return countBuy; }
        set { countBuy = value; }
    }
    
    public int CountHaveMoney
    {
        get { return countHaveMoney; }
        set { countHaveMoney = value; }
    }
    
    public int CountPlayTime
    {
        get { return countPlayTime; }
        set { countPlayTime = value; }
    }
    
    public int CountThingBuy
    {
        get { return countThingBuy; }
        set { countThingBuy = value; }
    }
    
    public int CountClick
    {
        get { return countClick; }
        set { countClick = value; }
    }
    
    public int GetMoney
    {
        get { return localMoney; }
        set { localMoney = value; }
    }
    
    public int GetMoneyInSecond
    {
        get { return localMoneyInSecond; }
        set { localMoneyInSecond = value; }
    }
    
    public int GetMoneyInClick
    {
        get { return localMoneyInClick; }
        set { localMoneyInClick = value; }
    }
    
    public int GetMaxRecord
    {
        get { return localMaxRecord; }
    }
    
    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // YandexGame.ResetSaveProgress();
            // YandexGame.SaveProgress();
            
            // if (YandexGame.savesData.energyInSecond == 0 || YandexGame.savesData.energyInClick == 0)
            // {
            //     YandexGame.savesData.energyInSecond = 1;
            //     YandexGame.savesData.energyInClick = 1;
            // }
            
            
            _txtScore.text = YandexGame.savesData.energy + " <sprite=\"Money\" name=\"Money\">";
            _txtInSecond.text = YandexGame.savesData.energyInSecond + "  <sprite=\"Money\" name=\"Money\">В секунду";
            _txtInClick.text = YandexGame.savesData.energyInClick + "  <sprite=\"Money\" name=\"Money\"> за клик";

            instance = this;

            localMoney = YandexGame.savesData.energy;
            localMoneyInSecond = YandexGame.savesData.energyInSecond;
            localMoneyInClick = YandexGame.savesData.energyInClick;
            localMaxRecord = YandexGame.savesData.maxRecord;
            
            countSpend = YandexGame.savesData.achievements.spend;
            countBuy = YandexGame.savesData.achievements.buy;
            countClick = YandexGame.savesData.achievements.click;
            countHaveMoney = YandexGame.savesData.achievements.haveMoney;
            countPlayTime = YandexGame.savesData.achievements.playTime;
            countThingBuy = YandexGame.savesData.achievements.thingBuy;

            localPriceThings = YandexGame.savesData.priceThings;
            localFirstBuyThing = YandexGame.savesData.firstBuyThing;
            localPercentAddThings = YandexGame.savesData.percentAddThings;
            
            DontDestroyOnLoad(gameObject);
            
            StartCoroutine(SaveRecord());
        }
    }

    IEnumerator SaveRecord()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);
        
            YandexGame.NewLeaderboardScores("Records", localMaxRecord);  
        
            Debug.Log("Сохранение рекорда");
        }
    }
    
    public bool DoubleBonus
    {
        set { _doubleBonus = value; }
        get { return _doubleBonus; }
    }

    void Update()
    {
        _time -= Time.deltaTime;

        if (_time < 0)
        {
            
            _time = 1f;

            if (_doubleBonus)
            {
                localMoney += localMoneyInSecond * 2;
                // YandexGame.savesData.energy += YandexGame.savesData.energyInSecond * 2;
            }
            else
            {
                localMoney += localMoneyInSecond;
                // YandexGame.savesData.energy += YandexGame.savesData.energyInSecond;
            }

            if (localMaxRecord < localMoney)
            // if (YandexGame.savesData.maxRecord < YandexGame.savesData.energy)
            {
                localMaxRecord = localMoney;
            }
            // YandexGame.SaveProgress();
        }
        
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (_doubleBonus)
        {
            _txtScore.text = localMoney + " <sprite=\"Money\" name=\"Money\">";
            _txtInSecond.text = localMoneyInSecond * 2 + " <sprite=\"Money\" name=\"Money\">В секунду x2";
            _txtInClick.text = localMoneyInClick * 2 + " <sprite=\"Money\" name=\"Money\"> за клик x2";
            
            // _txtScore.text = YandexGame.savesData.energy + " <sprite=\"Money\" name=\"Money\">";
            // _txtInSecond.text = YandexGame.savesData.energyInSecond * 2 + " <sprite=\"Money\" name=\"Money\">В секунду x2";
            // _txtInClick.text = YandexGame.savesData.energyInClick * 2 + " <sprite=\"Money\" name=\"Money\"> за клик x2";
        }
        else
        {
            _txtScore.text = localMoney + " <sprite=\"Money\" name=\"Money\">";
            _txtInSecond.text = localMoneyInSecond+ " <sprite=\"Money\" name=\"Money\">В секунду";
            _txtInClick.text = localMoneyInClick + " <sprite=\"Money\" name=\"Money\"> за клик";
            
            // _txtScore.text = YandexGame.savesData.energy + " <sprite=\"Money\" name=\"Money\">";
            // _txtInSecond.text = YandexGame.savesData.energyInSecond + " <sprite=\"Money\" name=\"Money\">В секунду";
            // _txtInClick.text = YandexGame.savesData.energyInClick + " <sprite=\"Money\" name=\"Money\"> за клик";
        }


    }
}
