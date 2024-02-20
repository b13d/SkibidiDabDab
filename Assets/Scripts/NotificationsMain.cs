using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationsMain : MonoBehaviour
{
    [SerializeField]
    private GameObject _newNotification = null;

    [SerializeField]
    private List<Transform> _notificationsTransform = new List<Transform>();

    private int _currentNumber = 1;



    public void CreateNewNotification()
    {
        var newNotification = Instantiate(_newNotification, transform.position, Quaternion.identity, transform);

        newNotification.GetComponent<Notification>().TextProgress.text = _currentNumber.ToString() + " / " + "10";

        _notificationsTransform.Add(newNotification.transform);

        _currentNumber++;


        Invoke("AnimationDestroy", 3.9f);
        Invoke("DeleteLastNotification", 5f);
    }

    private void DeleteLastNotification()
    {
        Destroy(transform.GetChild(0).gameObject);
    }

    private void AnimationDestroy()
    {
        Debug.Log("Я тута");

        _notificationsTransform[0].GetComponent<Notification>().Move = true;

        _notificationsTransform[0].gameObject.GetComponent<Animator>().SetTrigger("Destroy");

        _notificationsTransform.RemoveAt(0);
    }
}
