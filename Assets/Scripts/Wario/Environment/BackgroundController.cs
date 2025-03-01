using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundController : MonoBehaviour
{
    public Transform FirstPlane;
    public Transform SecondPlane;

    public List<Tilemap> FirstPlanes;
    public List<Tilemap> SecondPlanes;

    public float VelocityFirstPlane = 1f;
    public float VelocitySecondPlane;

    [SerializeField]
    private WarioMovement playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        VelocitySecondPlane = VelocityFirstPlane * 2;
        FirstPlanes = new List<Tilemap>(FirstPlane.GetComponentsInChildren<Tilemap>());
        SecondPlanes = new List<Tilemap>(SecondPlane.GetComponentsInChildren<Tilemap>());
    }

    // Update is called once per frame
    void Update()
    {
        //MoveTilemap(FirstPlanes, VelocityFirstPlane);
        MoveTilemap(SecondPlanes, VelocitySecondPlane);
    }

    private void MoveTilemap(List<Tilemap> tilemaps, float velocity)
    {
        foreach (var tilemap in tilemaps)
        {
            Vector3 move = new Vector3(-velocity * Time.deltaTime * playerMovement.xVelocity, 0, 0);
            tilemap.transform.Translate(move);
        }
    }
}
