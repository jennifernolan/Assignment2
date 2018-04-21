using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");//find the gameobject with the tag GameController
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent <GameController>();//access the GameController script
        }
        if (gameControllerObject == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        if(other.tag == "Bolt") //if the bullet from the gun hits the spider
        {
            Instantiate(explosion, transform.position, transform.rotation);// have the spider explode
            gameController.AddScore(scoreValue);//increase the score value in the GameController script in the AddScore method
            Destroy(gameObject);//destroy the spider gameobject
            Destroy(other.gameObject);//destroy the bullet
        }
    }
}
