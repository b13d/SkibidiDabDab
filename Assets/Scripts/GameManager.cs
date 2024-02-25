using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public float _time = 1f;

    [SerializeField]
    private TextMeshProUGUI _txtScore;

    [SerializeField]
    private TextMeshProUGUI _txtInSecond;

    [SerializeField]
    private TextMeshProUGUI _txtInClick;

    private bool _doubleBonus;


    private void Start()
    {
        Debug.Log(YandexGame.savesData.priceThings);
        
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // YandexGame.ResetSaveProgress();

            _txtScore.text = YandexGame.savesData.energy + " <sprite=\"Money\" name=\"Money\">";
            _txtInSecond.text = YandexGame.savesData.energyInSecond + "  <sprite=\"Money\" name=\"Money\">� �������";
            _txtInClick.text = YandexGame.savesData.energyInClick + "  <sprite=\"Money\" name=\"Money\"> �� ����";

            instance = this;
            DontDestroyOnLoad(gameObject);
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
                YandexGame.savesData.energy += YandexGame.savesData.energyInSecond * 2;
            }
            else
            {
                YandexGame.savesData.energy += YandexGame.savesData.energyInSecond;
            }
        }
        
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (_doubleBonus)
        {
            _txtScore.text = YandexGame.savesData.energy + " <sprite=\"Money\" name=\"Money\">";
            _txtInSecond.text = YandexGame.savesData.energyInSecond * 2 + " <sprite=\"Money\" name=\"Money\">� ������� x2";
            _txtInClick.text = YandexGame.savesData.energyInClick * 2 + " <sprite=\"Money\" name=\"Money\"> �� ���� x2";
        }
        else
        {
            _txtScore.text = YandexGame.savesData.energy + " <sprite=\"Money\" name=\"Money\">";
            _txtInSecond.text = YandexGame.savesData.energyInSecond + " <sprite=\"Money\" name=\"Money\">� �������";
            _txtInClick.text = YandexGame.savesData.energyInClick + " <sprite=\"Money\" name=\"Money\"> �� ����";
        }


    }
}
