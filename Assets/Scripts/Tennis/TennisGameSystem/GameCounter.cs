using UnityEngine;

public class GameCounter : MonoBehaviour
{
    [SerializeField]
    private int FIRST_COUNTER;
    [SerializeField]
    private int SECOND_COUNTER;
    [SerializeField]
    private int LAST_COUNTER;

    private int _counter;

    [SerializeField]
    private TextMesh _text;

    private const int FIRST_STATE = 0;
    private const int SECOND_STATE = 1;
    private const int LAST_STATE = 2;

    private int _state;

    private Transform _ballPosition;

    public delegate void IncreaseV();
    public static event IncreaseV OnIncreaseV;

    public delegate void RestartPart();
    public static event RestartPart OnRestartSignal;

    private void Start()
    {
        _state = FIRST_STATE;
        _counter = FIRST_COUNTER;
        _ballPosition = GameObject.FindGameObjectWithTag("TennisBall").transform;
        _text.text = _counter + " Hits";
    }

    public void OnEnable()
    {
        BallMovement.OnUpdateCounter += UpdateCounter;
    }

    public void OnDisable()
    {
        BallMovement.OnUpdateCounter -= UpdateCounter;
    }

    private void FixedUpdate()
    {
        if ((_ballPosition.position.y < -7.25) || (_ballPosition.position.y > 7.25))
        {
            OnRestartSignal?.Invoke();
            
            switch (_state)
            {
                case FIRST_STATE:
                    _counter = FIRST_COUNTER;
                    break;
                case SECOND_STATE:
                    _counter = SECOND_COUNTER;
                    break;
                case LAST_STATE:
                    _counter = LAST_COUNTER;
                    break;
            }

            UpdateText();

            //TODO SONIDO DE CUANDO PERDEMOS LA PELOTA PQ SE VA FUERA DEL ESCENARIO
        }
    }

    private void UpdateCounter()
    {
        _counter--;

        switch (_state)
        {
            case FIRST_STATE:
                if (_counter == 0)
                {
                    _state = SECOND_STATE;
                    _counter = SECOND_COUNTER;

                    OnIncreaseV?.Invoke();
                    OnRestartSignal.Invoke();

                    //TODO SONIDO DE QUE HA CONSEGUIDO UNA PARTE
                }
                break;
            case SECOND_STATE:
                if (_counter == 0)
                {
                    _state = LAST_STATE;
                    _counter = LAST_COUNTER;

                    OnIncreaseV?.Invoke();
                    OnRestartSignal.Invoke();

                    //TODO SONIDO DE QUE HA CONSEGUIDO UNA PARTE
                }
                break;
            case LAST_STATE:
                if (_counter == 0)
                {
                    //TODO CAMBIO DE ESCENA
                    GameManager.GameWin();
                }
                break;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = _counter + " Hits";
    }
}
