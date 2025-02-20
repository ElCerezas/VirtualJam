using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMove : MonoBehaviour
{
    private PlayerController _controller;

    private float _xVelocity;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
        _xVelocity = 0;
    }
    private void FixedUpdate()
    {
        _controller.RigidBody.linearVelocityX = _xVelocity * _controller.MovementSpeed;
    }
    private void OnMove(InputValue value)
    {
        var inputVal = value.Get<Vector2>();
        _xVelocity = inputVal.x;
        _controller.Animator.SetFloat("xVelocity", Mathf.Abs(_xVelocity));
        FlipGameObject();
    }

    private void FlipGameObject()
    {
        if (_xVelocity < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (_xVelocity > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
