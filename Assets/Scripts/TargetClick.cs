using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class TargetClick : MonoBehaviour
{
    public Image _image;
    public bool _wasClick = false;
    private SavesYG _savesYG = null;

    [SerializeField]
    private Slider _health = null;

    [SerializeField]
    private TextMeshProUGUI _txtHealth = null;

    void Start()
    {
        _image = transform.GetChild(0).GetComponent<Image>();
        _savesYG = YandexGame.savesData;
    }

    private void FixedUpdate()
    {
        ChangeScale();
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

    public void Click()
    {
        _health.value -= _savesYG.damage;

        _wasClick = true;
        _savesYG.energy += _savesYG.energyInClick;
        GameManager.instance.UpdateUI();

        _txtHealth.text = _health.value.ToString();
    }

}