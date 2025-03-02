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

    [SerializeField] public int InitialHearts = 3;
    public int Hearts;
    public int Coins;

    public Transform transform;
    public Vector3 lastPos;

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
        if (Instance != null) Instance = this;
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

    private void Update()
    {
        if(stateManager.IsGrounded)
        {
            lastPos = transform.position;
        }
    }

    private void UpdateHearts(int value)
    {
        if (value < 0)
        {
            transform.position = lastPos;
            Hearts = Mathf.Max(0, Hearts + value);
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
