using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    private bool _isMove = false;

    [SerializeField]
    private TextMeshProUGUI _txtProgress = null;


    [SerializeField]
    private TextMeshProUGUI _txtNameNotification = null;


    [SerializeField]
    private Image _imageNotification = null;


    public TextMeshProUGUI TextProgress
    {
        get { return _txtProgress; }
        set { _txtProgress = value; }
    }

    public TextMeshProUGUI TextNameNotification
    {
        get { return _txtNameNotification; }
        set { _txtNameNotification = value; }
    }

    public Image ImageNotification
    {
        get { return _imageNotification; }
        set { _imageNotification = value; }
    }

    public bool Move
    {
        set { _isMove = value; }
    }

    private void FixedUpdate()
    {
        if (_isMove)
        {
            transform.localPosition += Vector3.up;
        }
    }
}
