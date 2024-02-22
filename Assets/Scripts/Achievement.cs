using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _txtNameAchievement = null;

    [SerializeField]
    private Image _imageAchievement = null;

    [SerializeField]
    private Slider _sliderProgress = null;

    [SerializeField]
    private TextMeshProUGUI _txtCurrentProgress = null;

    [SerializeField]
    private TextMeshProUGUI _txtTargetProgress = null;

    [SerializeField] private RawImage _backGroundImage = null;
    
    [SerializeField] private bool finalized = false;

    public bool Finalized
    {
        get { return finalized; }
        set { finalized = value; }
    }
    
    public TextMeshProUGUI TextNameAchievement
    {
        get { return _txtNameAchievement; }
        set { _txtNameAchievement = value; }
    }
    
    public RawImage BackGround
    {
        get { return _backGroundImage; }
        set { _backGroundImage = value; }
    }

    public Image ImageAchievement
    {
        get { return _imageAchievement; }
        set { _imageAchievement = value; }
    }

    public TextMeshProUGUI CurrentProgress
    {
        get { return _txtCurrentProgress; }
        set { _txtCurrentProgress = value; }
    }

    public TextMeshProUGUI TargetProgress
    {
        get { return _txtTargetProgress; }
        set { _txtTargetProgress = value; }
    }

    // возможно тут ошибка, со слайдером

    public Slider SliderProgress
    {
        get { return _sliderProgress; }
        set { _sliderProgress = value; }
    }
}

