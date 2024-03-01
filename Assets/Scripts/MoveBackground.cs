using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.localPosition += new Vector3(4, 0, 0);

        if (transform.localPosition.x > 550)
        {
            transform.localPosition = new Vector3(-2698, 0, 0);
        }
    }
}
