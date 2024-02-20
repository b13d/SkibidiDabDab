using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 _posTarget;

    public Vector3 SetPosTarget
    {
        set { _posTarget = value; }
    }

    void FixedUpdate()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _posTarget, .1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Money")
        {
            Destroy(gameObject);
        }
    }
}
