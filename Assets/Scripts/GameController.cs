using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject spider;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    private int score;

    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        StartCoroutine(SpawnWaves());// used with the yield(paused using yield and resumed using yield return) in the Ienumerator Spawnwaves
        UpdateScore();
    }

    void Update()
    {
       if(gameOver && !restart)
       {
         anim.SetTrigger("GameOver");//set the trigger for the gameover animation (blue screen with black writing)
       }

       if(restart)
        {
            if(Input.GetKeyDown(KeyCode.R))//if the player hits the r key to restart the game
            {
                SceneManager.LoadScene("_Scene");//reload the scene
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);//wait for a coupe of seconds before spawning spiders
        while(true)
        {
            for(int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, Random.Range(-spawnValues.z, spawnValues.z));//spawn the spiders from random points on the screen on the left hand side
                Quaternion spawnRotation = Quaternion.identity;//represent rotation
                Instantiate(spider, spawnPosition, spawnRotation);//clones the spider
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);//wait for a few seconds between spawns

            if(gameOver)//if the game is over
            {
                restartText.text = "Press 'R' for Restart";//display the restart text
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;//add the score from the destroy by contact script from the on trigger enter method
        UpdateScore();
    }

    void UpdateScore()//display the score on screen to the player
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void GameOver()//game over text to appear when the game is over
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
