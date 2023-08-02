using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOBRespawn : MonoBehaviour {

    [SerializeField] Vector3 spawnPosition;

    private Collider2D _collider;
    private bool _playerOnPlatform;

    // Use this for initialization
    void Start ()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<CharacterController2D>();
        if (player != null)
        {
            other.transform.position = spawnPosition;
        }
    }
}
