using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Implant : MonoBehaviour
{
    private SavesYG _savesYG = null;

    [SerializeField]
    private int price = 50;

    [SerializeField]
    private bool isSpeedAdd = false;

    [SerializeField]
    private bool isDoubling = false;

    [SerializeField]
    private int bonusAdd = 0;

    [SerializeField]
    private int percentAdd = 20;

    [SerializeField]
    private TextMeshProUGUI _txtPriceImplant = null;

    [SerializeField]
    private bool _onlyOne = false;

    [SerializeField]
    private int _indexThing = 0;

    [SerializeField] private bool _clickAndInSecond = false;


    void Start()
    {
        _savesYG = YandexGame.savesData;

        if (price >= 1000000)
        {
            _txtPriceImplant.text = (float)price / 1000000 + "ì" + " <sprite=\"Money\" name=\"Money\">";
        }
        else
        {
            _txtPriceImplant.text = price.ToString() + " <sprite=\"Money\" name=\"Money\">";

            var priceInSave = YandexGame.savesData.priceThings[_indexThing];
            var newPercentAdd = YandexGame.savesData.percentAddThings[_indexThing];
            
            Debug.Log("priceInSave: " + priceInSave);
            
            if (priceInSave != 0)
            {
                percentAdd = newPercentAdd;
                price = priceInSave;
                _txtPriceImplant.text = priceInSave + " <sprite=\"Money\" name=\"Money\">";
            }
        }
    }


    public void Buy()
    {
        if (_savesYG.energy < price)
        {
            return;
        }
        

        YandexGame.savesData.achievements.buy += 1;
        YandexGame.savesData.achievements.spend += price;
        
        if (YandexGame.savesData.firstBuyThing[_indexThing] != 1)
        {
            YandexGame.savesData.achievements.thingBuy += 1;
            YandexGame.savesData.firstBuyThing[_indexThing] = 1;
        
            YandexGame.SaveProgress();
        }

        if (_onlyOne)
        {
            GetComponent<Button>().interactable = false;
        }

        _savesYG.energy -= price;

        if (isDoubling)
        {
            _savesYG.energyInSecond *= 2;
            _savesYG.energyInClick *= 2;

            price *= 4;
        }
        else
        {
            if (isSpeedAdd)
            {
                _savesYG.energyInSecond += bonusAdd;
            }
            else if (_clickAndInSecond)
            {
                _savesYG.energyInSecond += bonusAdd;
                _savesYG.energyInClick += bonusAdd;
            }
            else
            {
                _savesYG.energyInClick += bonusAdd;
            }

            price += Mathf.FloorToInt((float)price / 100 * percentAdd);

            percentAdd += 20;
        }


        YandexGame.savesData.priceThings[_indexThing] = price; 
        YandexGame.savesData.percentAddThings[_indexThing] = percentAdd; 
        
        YandexGame.SaveProgress();
        
        _txtPriceImplant.text = price.ToString() + " <sprite=\"Money\" name=\"Money\">";
        GameManager.instance.UpdateUI();
        
    }
}
