using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsEvents : MonoBehaviour
{
    [SerializeField]
    private bool _isOpenShop = false;

    [SerializeField]
    private bool _isShopMoving = false;

    [SerializeField]
    private bool _isOpenAchievements = false;

    [SerializeField]
    private bool _isAchievementsMoving = false;

    private float _yShopOpen = -225f;
    private float _yShopClose = -1190f;

    private float _xAchievementsOpen = 266f;
    private float _xAchievementsClose = 762f;

    [SerializeField]
    GameObject _shop = null;

    [SerializeField]
    GameObject _achievements = null;

    [SerializeField]
    GameObject _shopBG = null;

    [SerializeField]
    GameObject _contentShop = null;

    [SerializeField]
    GameObject _contentAchievements = null;

    public void Achievements()
    {
        Vector3 posContent = _contentAchievements.transform.localPosition;
        _contentAchievements.transform.localPosition = new Vector3(posContent.x, 0, posContent.z);

        _isAchievementsMoving = true;

        _isOpenAchievements = !_isOpenAchievements;

        //if (_isOpenAchievements)
        //{
        //    _shopBG.SetActive(true);
        //}
        //else
        //{
        //    _shopBG.SetActive(false);
        //}
    }

    public void Shop()
    {
        Vector3 posContent = _contentShop.transform.localPosition;
        _contentShop.transform.localPosition = new Vector3(posContent.x, 0, posContent.z);

        _isShopMoving = true;

        _isOpenShop = !_isOpenShop;

        if (_isOpenShop)
        {
            _shopBG.SetActive(true);
        }
        else
        {
            _shopBG.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (_isShopMoving)
        {
            if (_isOpenShop)
            {
                Vector3 targetPos = new Vector3(_shop.transform.localPosition.x, _yShopOpen, _shop.transform.localPosition.z);

                _shop.transform.localPosition = Vector3.Lerp(_shop.transform.localPosition, targetPos, .1f);
            }
            else
            {
                Vector3 targetPos = new Vector3(_shop.transform.localPosition.x, _yShopClose, _shop.transform.localPosition.z);

                _shop.transform.localPosition = Vector3.Lerp(_shop.transform.localPosition, targetPos, .1f);
            }
        }

        if (_isAchievementsMoving)
        {
            if (_isOpenAchievements)
            {
                Vector3 targetPos = new Vector3(_xAchievementsOpen, _achievements.transform.localPosition.y, _achievements.transform.localPosition.z);

                _achievements.transform.localPosition = Vector3.Lerp(_achievements.transform.localPosition, targetPos, .1f);
            }
            else
            {
                Vector3 targetPos = new Vector3( _xAchievementsClose, _achievements.transform.localPosition.y , _achievements.transform.localPosition.z);

                _achievements.transform.localPosition = Vector3.Lerp(_achievements.transform.localPosition, targetPos, .1f);
            }
        }
    }
}
