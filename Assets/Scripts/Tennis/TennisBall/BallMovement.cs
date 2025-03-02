using Unity.VisualScripting;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    private float _initialPositionX;
    [SerializeField]
    private float _initialPositionY;

    [SerializeField] 
    private float _speed;
    
    private const int WAITING = 0;
    private const int MOVE_TO_ENEMY = 1;
    private const int MOVE_TO_PLAYER = 2;

    private int _state;

    private const float MAX_SCALE = 0.90f;
    private const float MIN_SCALE = 0.7f;

    private float _scale = 1.0f;

    private Vector2 _velocity;

    private Rigidbody2D _rb;

    public delegate void UpdateCounter();
    public static event UpdateCounter OnUpdateCounter;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _state = WAITING;

        _velocity = Vector2.zero;

        this.transform.position = new Vector2(_initialPositionX, _initialPositionY);
    }

    private void OnEnable()
    {
        Movement.OnHitTennisBall += OnPlayerHit;
        EnemyMovement.OnEnemyHitBall += OnEnemeyHit;
        GameCounter.OnIncreaseV += OnIncreaseBallSpeed;
        GameCounter.OnRestartSignal += OnRestart;
    }

    private void OnDisable()
    {
        Movement.OnHitTennisBall -= OnPlayerHit;
        EnemyMovement.OnEnemyHitBall -= OnEnemeyHit;
        GameCounter.OnIncreaseV -= OnIncreaseBallSpeed;
        GameCounter.OnRestartSignal -= OnRestart;
    }

    void Update()
    {
        switch (_state)
        {
            case WAITING:
                break;
            case MOVE_TO_ENEMY:

                transform.localScale = new Vector3(_scale, _scale, _scale);
                _scale -= 0.001f;

                _rb.linearVelocity = _velocity * _speed;

                break;
            case MOVE_TO_PLAYER:

                transform.localScale = new Vector3(_scale, _scale, _scale);
                _scale += 0.001f;

                _rb.linearVelocity = _velocity * _speed;

                break;
        }
    }

    private void OnPlayerHit(int direction)
    {
        _state = MOVE_TO_ENEMY;
        _scale = MAX_SCALE;

        OnUpdateCounter?.Invoke();

        if (direction == 0)
        {
            _velocity.x = 0.0f;
            _velocity.y = 3.0f;
        }
        else if (direction == -1)
        {
            _velocity.x = -1.0f;
            _velocity.y = 3.0f;
        }
        else if (direction == 1)
        {
            _velocity.x = 1.0f;
            _velocity.y = 3.0f;
        }
    }

    private void OnEnemeyHit(int direction)
    {
        _state = MOVE_TO_PLAYER;
        _scale = MIN_SCALE;

        if (direction == 0)
        {
            _velocity.x = 0.0f;
            _velocity.y = -3.0f;
        }
        else if (direction == -1)
        {
            _velocity.x = -1.5f;
            _velocity.y = -3.0f;
        }
        else if (direction == 1)
        {
            _velocity.x = 1.5f;
            _velocity.y = -3.0f;
        }
    }

    private void OnIncreaseBallSpeed()
    {
        _speed += 0.75f;
    }

    private void OnRestart()
    {
        this.transform.position = new Vector2(_initialPositionX, _initialPositionY);
        
        _scale = MAX_SCALE;
        transform.localScale = new Vector3(_scale, _scale, _scale);

        _velocity = Vector2.zero;
        _rb.linearVelocity = Vector2.zero;

        _state = WAITING;
    }
}
