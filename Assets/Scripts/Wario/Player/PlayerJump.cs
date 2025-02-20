using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    
    public ContactFilter2D filter;

    //private CollisionDetection _collisionDetection;
    private float _lastVelocityY;
    private float _jumpStartedTime;

    private PlayerController _controller;

    private float _yVelocity;
    private float JumpHeight;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
        JumpHeight = _controller.JumpHeight;
    }
    private void FixedUpdate()
    {
        if (IsPeakReached()) TweakGravity();
    }
    public void OnJumpStarted()
    {
        SetGravity();
        var vel = new Vector2(_controller.RigidBody.linearVelocity.x, GetJumpForce());
        _controller.RigidBody.linearVelocity = vel;
        _jumpStartedTime = Time.time;
    }
    public void OnJumpFinished()
    {
        float fractionOfTimePressed = 1 / Mathf.Clamp01((Time.time - _jumpStartedTime) / _controller.PressTimeToMaxJump);
        _controller.RigidBody.gravityScale *= fractionOfTimePressed;
    }
    void OnJump(InputValue value)
    {
        _yVelocity = value.Get<float>();
        _controller.Animator.SetFloat("yVelocity", _yVelocity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        float h = -GetDistanceToGround() + JumpHeight;
        Vector3 start = transform.position + new Vector3(-1, h, 0);
        Vector3 end = transform.position + new Vector3(1, h, 0);
        Gizmos.DrawLine(start, end);
        Gizmos.color = Color.white;
    }

    private bool IsPeakReached()
    {
        bool reached = ((_lastVelocityY * _controller.RigidBody.linearVelocity.y) < 0);
        _lastVelocityY = _controller.RigidBody.linearVelocity.y;

        return reached;
    }
    private void SetGravity()
    {
        var grav = 1.1f * JumpHeight * (Mathf.Sqrt(_controller.SpeedHorizontal)) / Mathf.Sqrt(_controller.DistanceToMaxHeight);
        _controller.RigidBody.gravityScale = grav / 9.81f;
    }

    private void TweakGravity()
    {
        _controller.RigidBody.gravityScale *= 1.005f;
    }

    private float GetJumpForce()
    {
        return 2 * JumpHeight * _controller.SpeedHorizontal / _controller.DistanceToMaxHeight;
    }

    private float GetDistanceToGround()
    {
        RaycastHit2D[] hit = new RaycastHit2D[3];

        Physics2D.Raycast(transform.position, Vector2.down, filter, hit, 10);

        return hit[0].distance;
    }
}
