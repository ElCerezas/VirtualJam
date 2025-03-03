using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _initialPositionX;
    [SerializeField]
    private float _initialPositionY;

    [SerializeField]
    private float _speed;

    Rigidbody2D _rigidbody;
    
    private float _horizontalDir;
    private bool _right;

    private Vector2 _velocity;

    private SpriteRenderer _sprite;

    private bool _canHit;

    private Animator _animator;

    public delegate void HitTennisBall(int direction);
    public static event HitTennisBall OnHitTennisBall;

    void Start()
    {
        _canHit = false;
        _right = true;
        _sprite = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        this.transform.position = new Vector2(_initialPositionX, _initialPositionY);
    }

    private void OnEnable()
    {
        GameCounter.OnRestartSignal += OnRestart;
    }

    private void OnDisable()
    {
        GameCounter.OnRestartSignal -= OnRestart;
    }

    void FixedUpdate()
    {
        _rigidbody.linearVelocity = _velocity;

        _animator.SetBool("Hit", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TennisBall")
        {
            _canHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TennisBall")
        {
            _canHit = false;
        }
    }

    private void OnMove(InputValue value)
    {
        var inputVal = value.Get<float>();
        Debug.Log(value.GetType());

        if (inputVal < 0)
        {
            _animator.SetBool("Mov", true);
            if (_right)
            {
                _right = false;
                _sprite.transform.Rotate(0, 180, 0);
            }
            _horizontalDir = -1;
        }
        else if (inputVal > 0)
        {
            _animator.SetBool("Mov", true);
            if (!_right)
            {
                _right = true;
                _sprite.transform.Rotate(0, 180, 0);
            }
            _horizontalDir = 1;
        }
        else
        {
            _animator.SetBool("Mov", false);
            _horizontalDir = 0;
        }

        _velocity.x = _horizontalDir * _speed;
        _velocity.y = 0;
    }


    private void OnHit()
    {
        if (_canHit)
        {
            if (_velocity.x == 0)
            {
                OnHitTennisBall?.Invoke(0);
            }
            else if (_velocity.x < 0)
            {
                OnHitTennisBall?.Invoke(-1);
            }
            else if (_velocity.x > 0)
            {
                OnHitTennisBall?.Invoke(1);
            }

            _canHit = false;

            _animator.SetBool("Hit", true);

            //TODO SONIDO DE RAQUETAZO
        }
    }

    private void OnRestart()
    {
        this.transform.position = new Vector2(_initialPositionX, _initialPositionY);
    }
}
