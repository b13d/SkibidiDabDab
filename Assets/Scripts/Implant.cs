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
    private bool isDamageAdd = false;

    [SerializeField]
    private int bonusAdd = 0;

    [SerializeField]
    private int percentAdd = 1;

    [SerializeField]
    private TextMeshProUGUI _txtPriceImplant = null;

    [SerializeField]
    private bool _onlyOne = false;



    void Start()
    {
        _savesYG = YandexGame.savesData;
        
        if (price > 1000000)
        {
            _txtPriceImplant.text = (float)price / 1000000 + "ì" + " <sprite=\"Money\" name=\"Money\">";
        }
        else
        {
            _txtPriceImplant.text = price.ToString() + " <sprite=\"Money\" name=\"Money\">";
        }
    }


    public void Buy()
    {
        if (_onlyOne)
        {
            GetComponent<Button>().interactable = false;
        }

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

        price += Mathf.FloorToInt((float)price / 100 * percentAdd);

        Debug.Log(price / 100 * percentAdd);
        Debug.Log("price: " + price);

        percentAdd += 5;

        _txtPriceImplant.text = price.ToString() + " <sprite=\"Money\" name=\"Money\">";
        GameManager.instance.UpdateUI();
    }
}
