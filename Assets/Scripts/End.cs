using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private GameManger gameManger;

    private void Start()
    {
        gameManger = GameObject.Find("GameManager").GetComponent<GameManger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
            gameManger.CancelInvokeBombs();
        
        Destroy(other.gameObject);
    }
}
