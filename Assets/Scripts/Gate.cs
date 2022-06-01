using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private enum ColorGate { Rojo, Verde, Azul }

    [SerializeField] private ColorGate colorGate;
    private GameManger gameManger;
    private AudioSource _audioSource;
    private void Start()
    {
        gameManger = GameObject.Find("GameManager").GetComponent<GameManger>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains(colorGate.ToString()))
        {
            _audioSource.Play();
            gameManger.AddPoint(colorGate.ToString());
            Destroy(other.gameObject);
        }
    }
}
