using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private GameManger _gameManger;
    private void Start()
    {
        _gameManger = GameObject.Find("GameManager").GetComponent<GameManger>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            string name = String.Empty;
            if (other.gameObject.name.Contains("Verde"))
            {
                name = "Verde";
            }
            else if (other.gameObject.name.Contains("Azul"))
            {
                name = "Azul";
            }
            else if (other.gameObject.name.Contains("Rojo"))
            {
                name = "Rojo";
            }
            
            _gameManger.RestPoint(name);
            Instantiate(_particleSystem, transform.position, _particleSystem.transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
