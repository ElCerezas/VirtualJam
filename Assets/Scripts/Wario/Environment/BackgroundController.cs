using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundController : MonoBehaviour
{
    public Transform FirstPlane;
    public Transform SecondPlane;

    public Transform Player;
    public Vector3 SecondInitialPos;

    public float VelocityFirstPlane = 1f;
    public float VelocitySecondPlane;

    [SerializeField]
    private WarioMovement playerMovement;

    private void OnEnable()
    {
        WarioStats.OnPlayerKilled += ResetPosition;
    }

    private void OnDisable()
    {
        WarioStats.OnPlayerKilled -= ResetPosition;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SecondInitialPos = SecondPlane.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x >= 55)
        {
            MoveTilemap(SecondPlane, VelocitySecondPlane);
        }
        //MoveTilemap(FirstPlane, VelocityFirstPlane);
    }

    private void MoveTilemap(Transform tilemap, float velocity)
    {
        Vector3 move = new Vector3(-velocity * Time.deltaTime * playerMovement.xVelocity, 0, 0);
        tilemap.transform.Translate(move);
    }

    private void ResetPosition ()
    {
        SecondPlane.position = SecondInitialPos;
    }
}
