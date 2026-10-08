using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CombatManager : MonoBehaviour
{
    //timer stuff
    public float invisableTimer;
    public float visableTimer;
    public bool timerOn = false;
    public TextMeshProUGUI TimerText;
    public GameObject timerTextTest;
    public TextMeshProUGUI Player1TimeUI;
    public TextMeshProUGUI Player2TimeUI;
    public TextMeshProUGUI roundTextTest;
    public TextMeshProUGUI player1HealthText;
    public TextMeshProUGUI player2HealthText;
    public GameObject pressStart;
 //   public TextMeshProUGUI pressStartObject;
    public TextMeshProUGUI PlayerWonMatch;
    public GameObject PlayerWonMatchRemove;

    public GameObject roundText;
    public TextMeshProUGUI roundTextNumber;

    // i have no idea how to do aNYTHING
    public GameObject ornStandby;
    public GameObject ornPunch;
    public GameObject ornHurt;
    public GameObject cianStandby;
    public GameObject cianPunch;
    public GameObject cianHurt;

    bool gameStart = false;

    

    //combat stuff
    bool player1Pressed;
    bool player2Pressed;
   public bool player1Missed = false;
   public bool player2Missed = false;
    public bool gameWon;

    public int player1Health = 3;
    public int player2Health = 3;
    public int roundCount = 0;

    float player1Time;
    float player2Time;

    //sound stuff
    //SoundManager soundManager;


    private void Start()
    {
        //soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    
    private void Update()
    {

        Timer();
        Combat();
        WinCheck();
        TimerHide();
        HealthUpdate();
        EndRound();
    }

    //Match cleanup after winning a match
    void MatchCleanup()
    {
        roundCount = 0;
        player1Health = 3;
        player2Health = 3;
        player1Missed = false;
        player2Missed = false;
        player1Pressed = false;
        player2Pressed = false;
        TimerText.enabled = true;
        invisableTimer = 6;

        PlayerWonMatchRemove.SetActive(false);
        
    }
    void NextRound()
    {
        timerOn = false;
        invisableTimer = 6;

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
            PlayerWonMatch.text = ("YOU ONE! ->");
            PlayerWonMatchRemove.SetActive(true);

            if (Input.GetKey(KeyCode.Space))
            {
                MatchCleanup();
            }
        }
        if (player2Health == 0)
        {
            gameWon = true;
            PlayerWonMatch.text = ("<- YOU ONE!");
            PlayerWonMatchRemove.SetActive(true);
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
       
        if (invisableTimer <= 1)
        {
            TimerText.enabled = false;
        }

    }
    
    
    
    
    
    void Combat()
    {
        //punch controls for player 1
     if (gameStart == true)
        {
            if (visableTimer <= 1)
            {
                if (player1Pressed == false)
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        swapOrn("punch");
                        player1Pressed = true;
                        player1Time = invisableTimer;

                    }
                }
                //punch controls for player 2
                if (player2Pressed == false)
                {
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        swapCian("punch");
                        player2Pressed = true;
                        player2Time = invisableTimer;

                    }
                }
            }
        }
    }
        
       


    void WinCheck()
    {
     
        if (player1Pressed == true && player2Pressed == true)
        {
            invisableTimer = 0;
  
           //  timerTextTest.SetActive(false);


            //player 1 lose
            if (player1Time > player2Time)
            {
                player1Health--;
                swapOrn("hurt");
                swapCian("punch");
                roundTextTest.text = ($"Round {roundCount} Won by Player 2");
                roundText.SetActive(true);
                //soundManager.PlaySFX(soundManager.punch);
                //soundManager.PlaySFX(soundManager.applaud1);
                NextRound();
            }

            //player 2 lose
            else if (player1Time < player2Time)
            {
                player2Health--;
                swapOrn("punch");
                swapCian("hurt");
                roundTextTest.text = ($"Round {roundCount} Won by Player 1");
                roundText.SetActive(true);
                //soundManager.PlaySFX(soundManager.punch);
                //soundManager.PlaySFX(soundManager.applaud1);
                NextRound();
            }
        }

        if (invisableTimer == 0)
        {
            if (player1Pressed == false && player2Pressed == true)
            {
                player1Health--;
                roundTextTest.text = ($"Round {roundCount} Won by Player 2");
                swapOrn("hurt");
                swapCian("punch");
                roundText.SetActive(true);
                //soundManager.PlaySFX(soundManager.punch);
                //soundManager.PlaySFX(soundManager.applaud1);
                NextRound();
            }
           
            else if (player1Pressed == true && player2Pressed == false)
            {
                player2Health--;
                roundTextTest.text = ($"Round {roundCount} Won by Player 1");
                swapOrn("punch");
                swapCian("hurt");
                roundText.SetActive(true);
                //soundManager.PlaySFX(soundManager.punch);
                //soundManager.PlaySFX(soundManager.applaud1);
                NextRound();
            }





            else if (player1Pressed == false && player2Pressed == false)
            {
                swapOrn("hurt");
                swapCian("hurt");
                roundTextTest.text = ($"Round Stalemate");
                roundText.SetActive(true);
                NextRound();
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
            if (gameStart == false)
            {
                roundCount += 1;
                roundTextNumber.text = ($"Round: {roundCount}");
                roundText.SetActive(false);

                swapOrn("standby");
                swapCian("standby");
            }


            timerOn = true;
            gameStart = true;
            TimerText.enabled = true;
            pressStart.SetActive(false);
            //soundManager.PlaySFX(soundManager.round_start);




        }


        //timer

        if (timerOn)
        {
            invisableTimer -= Time.deltaTime;
            visableTimer = invisableTimer / 2;

            if (invisableTimer <= 0)
            {
                invisableTimer = 0;
                timerOn = false;
            }

            visableTimer = invisableTimer / 2f;

          //  updateTimer(invisableTimer);
            updateTimer(visableTimer);
        }
        Player1TimeUI.text = player1Time.ToString(".00");
        Player2TimeUI.text = player2Time.ToString(".00");

       
    }

    void swapOrn(string spr)
    {
        switch (spr) {
            case "standby":
                ornStandby.SetActive(true);
                ornPunch.SetActive(!true);
                ornHurt.SetActive(!true);
                break;
            case "punch":
                ornStandby.SetActive(!true);
                ornPunch.SetActive(true);
                ornHurt.SetActive(!true);
                break;
            case "hurt":
                ornStandby.SetActive(!true);
                ornPunch.SetActive(!true);
                ornHurt.SetActive(true);
                break;
            default:
                Debug.LogWarning("invalid sprite to swap to");
                break;

        };
    }

    void swapCian(string spr)
    {
        switch (spr)
        {
            case "standby":
                cianStandby.SetActive(true);
                cianPunch.SetActive(!true);
                cianHurt.SetActive(!true);
                break;
            case "punch":
                cianStandby.SetActive(!true);
                cianPunch.SetActive(true);
                cianHurt.SetActive(!true);
                break;
            case "hurt":
                cianStandby.SetActive(!true);
                cianPunch.SetActive(!true);
                cianHurt.SetActive(true);
                break;
            default:
                Debug.LogWarning("invalid sprite to swap to");
                break;

        }
        ;
    }

}
