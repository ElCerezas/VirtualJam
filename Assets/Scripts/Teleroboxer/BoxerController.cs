using UnityEngine;
using UnityEngine.InputSystem;

public class BoxerController : MonoBehaviour
{
    BoxerStats b;
    PlayerHandController left, right;

    void Start()
    {
        b = GetComponent<BoxerStats>();
        left = b.leftHand.GetComponent<PlayerHandController>();
        right = b.rightHand.GetComponent<PlayerHandController>();
    }

    void OnRightBlock(InputValue v)
    {
        right.OnBlock(v.Get<float>() == 1);
    }

    void OnLeftBlock(InputValue v)
    {
        left.OnBlock(v.Get<float>() == 1);
    }

    void OnRightHit()
    {
        right.OnHit();
    }

    void OnLeftHit()
    {
        left.OnHit();
    }
}
