using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowndSensor : MonoBehaviour
{
    public bool isGrounded;

    void OnTriggerEnter2D(Collider2D collider)
    {
        isGrounded = true;

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        isGrounded = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        isGrounded = false;
    }

}
