using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FaderAsync;

public class MinimapFollow : MonoBehaviour
{
    public float lerpFactor;
    [SerializeField] private Transform player;

    [SerializeField] private Vector3 offset;
    private Vector3 startPosition;

    public Transform Player
    {
        get
        {
            return player;
        }

        set
        {
            player = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player != null)
        {
            transform.position = Vector3.Lerp(transform.position, Player.position + offset, lerpFactor);
        }
        else
        {
            if(PlayerManager.playerInstance!=null)
            {
                Player = PlayerManager.playerInstance.transform;
            }
            transform.position = Vector3.Lerp(transform.position, startPosition, lerpFactor);
        }
    }
}
