using System;
using UnityEngine;

public class PowerJump : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }
}
