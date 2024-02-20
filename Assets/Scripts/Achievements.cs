using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    [SerializeField]
    private List<string> _nameAchievements = new List<string>();

    [SerializeField]
    private List<int> _targetAchievements = new List<int>();

    [SerializeField]
    private List<Sprite> _spriteAchievements = new List<Sprite>();

    [SerializeField]
    private GameObject _achievementPrefab = null;

    [SerializeField]
    private GameObject _placeAchievements = null;


    void Start()
    {
        CreateAchievements();
    }


    void CreateAchievements()
    {
        for (int i = 0; i <  _nameAchievements.Count; i++)
        {
            var newAchievement = Instantiate(_achievementPrefab, transform.position, Quaternion.identity, _placeAchievements.transform);
            newAchievement.GetComponent<Achievement>().TextNameAchievement.text = _nameAchievements[i];
            newAchievement.GetComponent<Achievement>().ImageAchievement.sprite = _spriteAchievements[i];
        }
    }

}
