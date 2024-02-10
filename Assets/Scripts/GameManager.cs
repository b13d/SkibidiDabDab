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

    [SerializeField]
    private TextMeshProUGUI _txtDamage;


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
            _txtDamage.text = savesYG.damage + "  <sprite=\"Damage\" name=\"Damage\"> за клик";


            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        savesYG = YandexGame.savesData;
    }

    void Update()
    {
        _time -= Time.deltaTime;

        if (_time < 0)
        {
            _time = 1f;
            savesYG.energy += savesYG.energyInSecond;

            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        _txtScore.text = savesYG.energy + " <sprite=\"Money\" name=\"Money\">";
        _txtInSecond.text = savesYG.energyInSecond + " <sprite=\"Money\" name=\"Money\">В секунду";
        _txtInClick.text = savesYG.energyInClick + " <sprite=\"Money\" name=\"Money\"> за клик";
        _txtDamage.text = savesYG.damage + "  <sprite=\"Damage\" name=\"Damage\"> за клик";
    }
}
