using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public GameObject playerExplosion;
    public GameObject playerHurt;
    bool isDead;
    bool damaged;
    private GameController gameController;

    public void Start ()
    {
        currentHealth = startingHealth + 10;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");//find the game object with the tag gamecontroller
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();//get access to the game controller script
        }
    }

    void Update ()
    {
         if (damaged && currentHealth < 100)//if the player is damaged flash the red colour also stops from flashing colour when the game starts
         {
            damageImage.color = flashColour;
         }
         else
         {
             damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);//slowly have the colour fade out to transparent again
         }
         damaged = false;
    }


    public void OnTriggerEnter (Collider other)
    {
        damaged = true;

        currentHealth = currentHealth - 10;//decrease the players current health

        healthSlider.value = currentHealth;//have the health slider show the new value of the player

        if(currentHealth == 0)//if the player is dead
        {
            other.tag = "Player";//access the gameobject with the tag player
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);//have the playerexplosion play
            Destroy(gameObject);//destroy the player gameobject
            gameController.GameOver();//go to the gamecontroller script and go to the game over method
        }
        if(currentHealth != 0 && currentHealth < 100)//if the player isn't dead and also stops the playerhurt sound from playing when game starts
        {
            other.tag = "Player";//access the gameobject with the player tag
            Instantiate(playerHurt, other.transform.position, other.transform.rotation);//have the playerhurt game object play(player hurt audio)
        }
    }
}
