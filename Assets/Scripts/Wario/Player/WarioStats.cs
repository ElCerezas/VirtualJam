using System;
using UnityEngine;

public class WarioStats : MonoBehaviour
{
    public static WarioStats Instance;
    private WarioStateManager stateManager;

    public static Action<int> OnUpdateHearts;
    public static Action<int> OnUpdateCoins;

    public static Action<int> OnHeartsChanged;
    public static Action<int> OnCoinsChanged;

    public static Action OnPlayerKilled;

    public static Action<Vector3> OnChangePosition;

    [SerializeField] public int InitialHearts = 3;
    public int Hearts;
    public int Coins;

    public Transform transform;
    public Transform SpawnPoint;

    private void OnEnable()
    {
        OnUpdateHearts += UpdateHearts;
        OnUpdateCoins += UpdateCoins;
    }

    private void OnDisable()
    {
        OnUpdateHearts -= UpdateHearts;
        OnUpdateCoins -= UpdateCoins;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Hearts = InitialHearts;
        Coins = 0;
        OnHeartsChanged?.Invoke(Hearts);
        OnCoinsChanged?.Invoke(Coins);
        transform = GetComponent<Transform>();
        stateManager = WarioStateManager.Instance;
    }

    private void UpdateHearts(int value)
    {
        if (value < 0)
        {
            OnPlayerKilled?.Invoke();
            AudioManager.Instance.PlaySFX("Damage");
            transform.position = SpawnPoint.position;
            Hearts = Mathf.Max(0, Hearts + value);
            
            if (Hearts == 0)
            {
                GameManager.GameLose();
            }
        }
        else
        {
            Hearts += value;
        }
        OnHeartsChanged?.Invoke(Hearts);
    }

    private void UpdateCoins(int value)
    {
        Coins = Mathf.Max(0, Coins + value);
        OnCoinsChanged?.Invoke(Coins);
    }
}
