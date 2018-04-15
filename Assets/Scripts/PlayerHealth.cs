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
    bool isDead;
    bool damaged;
    private GameController gameController;

    public void Start ()
    {
        currentHealth = startingHealth;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void OnTriggerEnter (Collider other)
    {
        damaged = true;

        currentHealth = currentHealth - 10;

        healthSlider.value = currentHealth;

        if(currentHealth == 0)
        {
            other.tag = "Player";
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            Destroy(gameObject);
            gameController.GameOver();
        }
    }
}
