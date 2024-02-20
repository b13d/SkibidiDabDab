using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public float _time = 1f;
    public SavesYG savesYG;

    [SerializeField]
    private TextMeshProUGUI _txtScore;

    [SerializeField]
    private TextMeshProUGUI _txtInSecond;

    [SerializeField]
    private TextMeshProUGUI _txtInClick;

    private bool _doubleBonus;


    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _txtScore.text = savesYG.energy + " <sprite=\"Money\" name=\"Money\">";
            _txtInSecond.text = savesYG.energyInSecond + "  <sprite=\"Money\" name=\"Money\">В секунду";
            _txtInClick.text = savesYG.energyInClick + "  <sprite=\"Money\" name=\"Money\"> за клик";

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        savesYG = YandexGame.savesData;
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
                savesYG.energy += savesYG.energyInSecond * 2;
            }
            else
            {
                savesYG.energy += savesYG.energyInSecond;
            }

            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        if (_doubleBonus)
        {
            _txtScore.text = savesYG.energy + " <sprite=\"Money\" name=\"Money\">";
            _txtInSecond.text = savesYG.energyInSecond * 2 + " <sprite=\"Money\" name=\"Money\">В секунду x2";
            _txtInClick.text = savesYG.energyInClick * 2 + " <sprite=\"Money\" name=\"Money\"> за клик x2";
        }
        else
        {
            _txtScore.text = savesYG.energy + " <sprite=\"Money\" name=\"Money\">";
            _txtInSecond.text = savesYG.energyInSecond + " <sprite=\"Money\" name=\"Money\">В секунду";
            _txtInClick.text = savesYG.energyInClick + " <sprite=\"Money\" name=\"Money\"> за клик";
        }


    }
}
