using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>();

    [SerializeField]
    private Image _mainImage = null;

    [SerializeField]
    private Transform _transformParent;

    [SerializeField]
    private GameObject _buttonClose = null;

    bool _isPointer = false;
    bool _wasClick = false;
    bool _movePanel = false;

    float xPos = 0;
    Vector3 _targetPos = Vector3.zero;


    private void Start()
    {
        _buttonClose = transform.GetChild(1).gameObject;
        _mainImage = transform.GetChild(0).GetComponent<Image>();
        _transformParent = transform.parent;
    }

    private void FixedUpdate()
    {
        if (_movePanel)
        {
            if (_transformParent.position == _targetPos)
            {
                _movePanel = false;
            }

            _transformParent.localPosition = Vector3.Lerp(_transformParent.localPosition, _targetPos, .1f);
        }

        var even = Mathf.Ceil(_transformParent.localPosition.x) == Mathf.Ceil(_targetPos.x);

        if (even)
        {
            _movePanel = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isPointer)
        {
            if (_wasClick)
            {
                xPos = 787;
            }
            else
            {
                xPos = 285;
            }


            _targetPos = _transformParent.localPosition;
            _targetPos.x = xPos;
            _movePanel = true;

            _wasClick = !_wasClick;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");

        if (_wasClick)
        {
            _buttonClose.SetActive(true);
            _mainImage.gameObject.SetActive(false);
            _mainImage.sprite = null;
        }
        else
        {
            _mainImage.gameObject.SetActive(true);
            _buttonClose.SetActive(false);
            _mainImage.sprite = sprites[1];
        }

        _isPointer = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");

        if (_wasClick)
        {
            _mainImage.gameObject.SetActive(false);
            _buttonClose.SetActive(true);
            _mainImage.sprite = null;
        }
        else
        {
            _mainImage.gameObject.SetActive(true);
            _buttonClose.SetActive(false);
            _mainImage.sprite = sprites[0];
        }

        _isPointer = false;
        
    }

}
