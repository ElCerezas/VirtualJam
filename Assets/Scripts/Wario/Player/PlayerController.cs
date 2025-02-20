using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField]
    public float MovementSpeed;

    public Rigidbody2D RigidBody {  get; private set; }
    public Animator Animator { get; private set; }

    [Header("Jump Settings")]
    [SerializeField]
    public float JumpHeight;
    [SerializeField]
    public float DistanceToMaxHeight;
    [SerializeField]
    public float SpeedHorizontal;
    [SerializeField]
    public float PressTimeToMaxJump;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
