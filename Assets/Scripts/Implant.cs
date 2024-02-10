using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class Implant : MonoBehaviour
{
    private SavesYG _savesYG = null;

    [SerializeField]
    private int price = 50;

    [SerializeField]
    private bool isSpeedAdd = false;

    [SerializeField]
    private bool isDamageAdd = false;

    [SerializeField]
    private int bonusAdd = 0;

    [SerializeField]
    private int percentAdd = 1;

    [SerializeField]
    private TextMeshProUGUI _txtPriceImplant = null;



    void Start()
    {
        _savesYG = YandexGame.savesData;
    }


    public void Buy()
    {
        if (_savesYG.energy < price)
        {
            return;
        }

        _savesYG.energy -= price;

        if (isSpeedAdd)
        {
            _savesYG.energyInSecond += bonusAdd;
        }
        else if (isDamageAdd)
        {
            _savesYG.damage += bonusAdd;
        }
        else
        {
            _savesYG.energyInClick += bonusAdd;
        }

        price += price / percentAdd;

        percentAdd += 5;

        _txtPriceImplant.text = price.ToString();
        GameManager.instance.UpdateUI();
    }
}
