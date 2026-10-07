using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CombatManager : MonoBehaviour
{
    //timer stuff
    public float timeLeft;
    public bool timerOn = false;
    public TextMeshProUGUI TimerText;
    public GameObject timerTextTest;
    public TextMeshProUGUI Player1TimeUI;
    public TextMeshProUGUI Player2TimeUI;
    public TextMeshProUGUI roundTextTest;
    public TextMeshProUGUI player1HealthText;
    public TextMeshProUGUI player2HealthText;
    public GameObject pressStart;
    public TextMeshProUGUI PlayerWonMatch;
    public GameObject PlayerWonMatchRemove;

    public GameObject roundText;

    bool gameStart = false;

    bool test;

    //combat stuff
    bool player1Pressed;
    bool player2Pressed;
   public bool player1Missed = false;
   public bool player2Missed = false;
    bool gameWon;

    public int player1Health = 3;
    public int player2Health = 3;
    public int roundCount = 1;

    float player1Time;
    float player2Time;

    private void Start()
    {
       
    }

    
    private void Update()
    {

        Timer();
        Combat();
        WinCheck();
        TimerHide();
        HealthUpdate();
    }

    //Match cleanup after winning a match
    void MatchCleanup()
    {
        player1Health = 3;
        player2Health = 3;
        player1Missed = false;
        player2Missed = false;
        player1Pressed = false;
        player2Pressed = false;
        TimerText.enabled = true;
        timeLeft = 3;
        
    }
    void NextRound()
    {
        timerOn = false;
        timeLeft = 3;

        player1Missed = false;
        player2Missed = false;
        player1Pressed = false;
        player2Pressed = false;
        pressStart.SetActive(true);
        gameStart = false;
       
        
        TimerText.enabled = true;
        
    }

    public void EndRound()
    {

        if (player1Health == 0)
        {
            gameWon = true;
            PlayerWonMatch.text = ("Player 1 Has ONE the Match!");

            if (Input.GetKey(KeyCode.Space))
            {
                MatchCleanup();
            }
        }
        else if (player2Health == 0)
        {
            gameWon = true;
            PlayerWonMatch.text = ("Player 2 Has ONE the Match!");
            
            if (Input.GetKey(KeyCode.Space))
            {
                MatchCleanup();
            }
        }
       
    }


    void HealthUpdate()
    {
        player1HealthText.text = ($"{player1Health}");
        player2HealthText.text = ($"{player2Health}");
    }


   void TimerHide()
    {
       
        if (timeLeft <= 1)
        {
            TimerText.enabled = false;
        }

    }
    
    
    
    
    
    void Combat()
    {
        //punch controls for player 1
     if (gameStart == true)
        {
            if (player1Pressed == false)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    player1Pressed = true;
                    player1Time = timeLeft;

                }
            }
            //punch controls for player 2
            if (player2Pressed == false)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    player2Pressed = true;
                    player2Time = timeLeft;

                }
            }

        }
    }
        
       


    void WinCheck()
    {
     
        if (player1Pressed == true && player2Pressed == true)
        {
            timeLeft = 0;
  
           //  timerTextTest.SetActive(false);


            //player 1 lose
            if (player1Time > player2Time)
            {
                player1Health--;
                roundTextTest.text = ($"Round {roundCount} Won by Player 2");
                roundText.SetActive(true);
                NextRound();
            }

            //player 2 lose
            else if (player1Time < player2Time)
            {
                player2Health--;
                roundTextTest.text = ($"Round {roundCount} Won by Player 1");
                roundText.SetActive(true);
                NextRound();
            }
        }

        if (timeLeft == 0)
        {
            if (player1Pressed == true && player2Pressed == false)
            {
                player1Health--;
                roundTextTest.text = ($"Round {roundCount} Won by Player 2");
                roundText.SetActive(true);
                NextRound();
            }
           
            if (player1Pressed == false && player2Pressed == true)
            {
                player2Health--;
                roundTextTest.text = ($"Round {roundCount} Won by Player 1");
                roundText.SetActive(true);
                NextRound();
            }





            if (player1Pressed == false && player2Pressed == false)
            {
                roundTextTest.text = ($"Round Stalemate");
                roundText.SetActive(true);
            }
        }
        
       

        

    }




    void updateTimer(float currentTime)

    {
        
        //timer formating
        float seconds = Mathf.FloorToInt(currentTime);
        int milliseconds = Mathf.FloorToInt((currentTime - seconds) * 100);

       TimerText.text = string.Format("{0:0}.{1:0}", seconds, milliseconds);
    }

    void Timer()
    {


        if (Input.GetKeyUp(KeyCode.Space))
        {
            timerOn = true;
            gameStart = true;
            TimerText.enabled = true;
            pressStart.SetActive(false);
            roundText.SetActive(false);
        }


        //timer

        if (timerOn)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                timeLeft = 0;
                timerOn = false;
            }
            updateTimer(timeLeft);
        }
        Player1TimeUI.text = player1Time.ToString(".00");
        Player2TimeUI.text = player2Time.ToString(".00");

       
    }

}
