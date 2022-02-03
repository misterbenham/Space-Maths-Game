using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Problem[] problems;
    public int curProblem;
    public float timePerProblem;

    public float remainingTime;

    public Text remainingLives;
    public int lives;
    
    public PlayerController player;

    public static GameManager instance;

    public Text endTextScore;
    public int gameScore;
    public Text scoreText;

    public AudioSource playSound1;
    public AudioSource playSound2;

    public playFabManager pfm; //référence to the playfabmanager script to use his fucntions.
    private bool endGame = false;

    void Awake()
    {
        instance = this;
    }

    void Win()
    {
        Time.timeScale = 0.0f;
        SetEndTextScore();
        UI.instance.SetEndText(true);
        
    }

    void Lose()
    {
        Time.timeScale = 0.0f;
        UI.instance.SetEndText(false);
        scoreText.text = "Score : "+ gameScore;
        //send the score to the leaderboard
        pfm.SendLeaderboardHighScoreAllTime(gameScore);
    }

    void setProblem (int problem)
    {
        curProblem = problem;
        remainingTime = timePerProblem;

        UI.instance.SetProblemText(problems[curProblem]);
    }

    void CorrectAnswer()
    {
        playSound1.Play();
        gameScore++;
        if (problems.Length - 1 == curProblem)
            Win();
        else
            setProblem(curProblem + 1);
    }

    void SetRemainingLives()
    {
        this.remainingLives.text = lives.ToString();
    }

    void SetEndTextScore()
    {
        this.endTextScore.text = gameScore.ToString();
    }

    public void removeLife()
    {
        lives--;
        if(lives <= 0)
            {
                Destroy(player);
                Lose();
            }
    }

    void OnTriggerEnter2D(Collider2D planet)
    {
        if (planet.gameObject.CompareTag("Player"))
        {
            removeLife();
        }

    }

    void IncorrectAnswer( )
    {
        playSound2.Play();
        removeLife();
    }

    private void blackText()
    {
        if (lives <= 3)
            remainingLives.color = Color.black;

    }

    public void OnPlayerEnterPlanet (int planet)
    {
        if (planet == problems[curProblem].correctPlanet)
            CorrectAnswer();
            
        else
            IncorrectAnswer();
    }


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        setProblem(0);
    }

    // Update is called once per frame
    void Update()
    {
        
        remainingTime -= Time.deltaTime;
        if(remainingTime <= 0.0f)
        {
            if (endGame == false)
            {
                Lose();
                endGame = true;
            }
        }

        remainingLives.text = lives.ToString();
        blackText();


        
    }
}
