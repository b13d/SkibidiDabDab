using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class TargetClick : MonoBehaviour
{
    public Image _image;
    public bool _wasClick = false;
    private SavesYG _savesYG = null;

    [SerializeField]
    private Slider _energy = null;

    [SerializeField]
    private GameObject _prefabMoney;

    [SerializeField]
    private Transform _posTarget;

    [SerializeField]
    private float _lastTimeClick = 0f;

    [SerializeField]
    private GameObject _txtDoubleBonus = null;

    [SerializeField]
    private GameObject _firstPlay = null;

    void Start()
    {
        Debug.Log(YandexGame.savesData.achievements["Buy"]);

        _image = transform.GetChild(0).GetComponent<Image>();
        _savesYG = YandexGame.savesData;

        // проверка на то первый раз ли игрок зашел в игру
        // если первый то firstPlay не трогать
        // иначе firstPlay SetActive(false)

        if (!YandexGame.savesData.firstTry)
        {
            _firstPlay.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Dekstop();
        }

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                MobileClick(i);
            }
        }
    }

    void Dekstop()
    {
        

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up);

        Debug.Log(hit.collider.tag);

        if (hit.collider != null)
        {
            if (!hit.collider.CompareTag("Implant") && !hit.collider.CompareTag("Shop") && !hit.collider.CompareTag("Achievement"))
            {
                Click(0);
            }
        }
    }

    void MobileClick(int touchId)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(touchId).position), Vector2.up);

        if (Input.GetTouch(touchId).phase == TouchPhase.Began)
        {
            if (hit.collider != null)
            {
                if (!hit.collider.CompareTag("Implant") && !hit.collider.CompareTag("Shop") && !hit.collider.CompareTag("Achievement"))
                {
                    Click(touchId);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        _lastTimeClick += 1 * Time.deltaTime;

        ChangeScale();

        if (_lastTimeClick > .5f)
        {
            if (_energy.value > 0)
            {
                _energy.value -= 1;
            }
        }

        if (_energy.value > 80)
        {
            _txtDoubleBonus.SetActive(true);

            GameManager.instance.DoubleBonus = true;
        }
        else
        {
            _txtDoubleBonus.SetActive(false);

            GameManager.instance.DoubleBonus = false;
        }
    }


    void ChangeScale()
    {
        if (_wasClick)
        {
            _image.transform.localScale -= Vector3.one / 5f;
        }

        if (!_wasClick && _image.transform.localScale.x < 1)
        {
            _image.transform.localScale += Vector3.one / 5f;
        }

        if (_image.transform.localScale.x < 0.5f)
        {
            _wasClick = false;
        }
    }

    public void Click(int idTouch)
    {
        if (_firstPlay.activeSelf == true)
        {
            _firstPlay.SetActive(false);
            YandexGame.savesData.firstTry = false;
            YandexGame.SaveProgress();
        }

        _lastTimeClick = 0f;
        // тут надо что-то с энергией ( шкалой ) и ее бонусами делать

        if (_energy.value < 100)
        {
            _energy.value += 8;
        }

        _wasClick = true;

        if (GameManager.instance.DoubleBonus)
        {
            _savesYG.energy += _savesYG.energyInClick * 2;
        }
        else
        {
            _savesYG.energy += _savesYG.energyInClick;
        }

        
        GameManager.instance.UpdateUI();

        var newMoney = _prefabMoney;
        newMoney.GetComponent<MoneyMove>().SetPosTarget = _posTarget.localPosition;

        if (YandexGame.EnvironmentData.isMobile)
        {
            Instantiate(newMoney, Camera.main.ScreenToWorldPoint(Input.GetTouch(idTouch).position), Quaternion.identity);
        }
        else
        {
            Instantiate(newMoney, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        }

    }

}
