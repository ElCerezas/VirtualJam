using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class BoxerController : MonoBehaviour
{
    BoxerStats b;
    HandState left, right;
    void Start()
    {
        b = GetComponent<BoxerStats>();
        //left = GetComponent<BoxerStats>().leftHand.GetComponent<HandState>();
        right = GetComponent<BoxerStats>().rightHand.GetComponent<HandState>();
    }


    void OnRightBlock(InputValue v)
    {
        Debug.Log("Right");
        if (v.Get<float>() == 1)
        {
            right.OnBlock(true);
        } 
        else
        {
            right.OnBlock(false);
        }
    }
    /*void OnLeftBlock(InputValue v)
    {
        Debug.Log("Left");
        if (v.Get<float>() == 1)
        {
            left.OnBlock(true);
        }
        else
        {
            left.OnBlock(false);
        }
    }*/
    void OnRightHit()
    {   
        right.OnHit();
    }
}
