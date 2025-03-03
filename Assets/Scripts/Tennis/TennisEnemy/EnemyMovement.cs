using System.Runtime.CompilerServices;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _initialPositionX;
    [SerializeField]
    private float _initialPositionY;

    [SerializeField]
    private float _speed;

    private Vector2 _velocity = Vector2.zero;

    private Rigidbody2D _rb;

    private Transform _target;

    private bool _facingRight;

    private SpriteRenderer _sprite;

    private Animator _animator;

    public delegate void EnemyHitBall(int direction);
    public static event EnemyHitBall OnEnemyHitBall;

    public delegate void HitSound();
    public static event HitSound OnHitSound;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("TennisBall").GetComponent<Transform>();
        
        _facingRight = true;
        _sprite = GetComponent<SpriteRenderer>();

        _animator = GetComponent<Animator>();

        this.transform.position = new Vector2(_initialPositionX, _initialPositionY);
    }

    private void OnEnable()
    {
        GameCounter.OnIncreaseV += OnIncreaseVelocity;
        GameCounter.OnRestartSignal += OnRestart;
    }

    private void OnDisable()
    {
        GameCounter.OnIncreaseV -= OnIncreaseVelocity;
        GameCounter.OnRestartSignal -= OnRestart;
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - _target.position.x) > 0.05)
        {
            _animator.SetBool("Mov", true);

            _rb.linearVelocity = _velocity * _speed;

            if (_rb.position.x < _target.position.x)
            {
                _velocity.x = 1;
                if (!_facingRight)
                {
                    _sprite.transform.Rotate(0, 180, 0);
                    _facingRight = true;
                }
            }
            else if (_rb.position.x > _target.position.x)
            {
                _velocity.x = -1;
                if (_facingRight)
                {
                    _sprite.transform.Rotate(0, 180, 0);
                    _facingRight = false;
                }
            }
        }
        else{
            _velocity.x = 0;
            _rb.linearVelocity = _velocity;
            _animator.SetBool("Mov", false);
        }
        _animator.SetBool("Hit", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TennisBall")
        {
            _animator.SetBool("Hit", true);

            if (_target.position.x > 1) OnEnemyHitBall?.Invoke(-1);
            else if (_target.position.x < 1) OnEnemyHitBall?.Invoke(1);
            else OnEnemyHitBall?.Invoke(0);

            OnHitSound?.Invoke();
        }
    }

    private void OnIncreaseVelocity()
    {
        _speed += 1;
    }
    private void OnRestart()
    {
        this.transform.position = new Vector2(_initialPositionX, _initialPositionY);
    }
}
