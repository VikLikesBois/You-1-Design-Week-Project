using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CombatManager : MonoBehaviour
{

    //code change hi i need to push to github master asiudhbasiduahbnsdaiusdbn

    //timer stuff
    public float startingTime;
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

    public GameObject ciFour;
    public GameObject ciThree;
    public GameObject ciTwo;
    public GameObject ciOne;

    public UnityEngine.UI.Image ornPointsFill;
    public UnityEngine.UI.Image cianPointsFill;

    bool gameStart = false;

    public GameObject tutorial;
   

    //combat stuff
    bool player1Pressed;
    bool player2Pressed;
    public bool gameWon;

    public int player1Health = 3;
    public int player2Health = 3;
    public int roundCount = 0;



    public float minSpeed;
    public float maxSpeed;



    float player1Time;
    float player2Time;



    //sound stuff
    SoundManager soundManager;


    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
        timerOn = false;
        gameStart = false;

        invisableTimer = startingTime;
        visableTimer = 4f;
    }

    
    private void Update()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            
            tutorial.SetActive(false);
        }


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
        player1Pressed = false;
        player2Pressed = false;
        TimerText.CrossFadeAlpha(1, 0.1f, true);
        startingTime = Random.Range(minSpeed, maxSpeed);
        invisableTimer = startingTime;

        PlayerWonMatchRemove.SetActive(false);
        
    }
    void NextRound()
    {
        timerOn = false;
        startingTime = Random.Range(minSpeed, maxSpeed);
        invisableTimer = startingTime;

        player1Pressed = false;
        player2Pressed = false;
        pressStart.SetActive(true);
        
        gameStart = false;


        TimerText.CrossFadeAlpha(1, 0.1f, true);

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
        switch (player1Health)
        {
            case 3:
                cianPointsFill.fillAmount = 0;
                break;
            case 2:
                cianPointsFill.fillAmount = 0.35f;
                break;
            case 1:
                cianPointsFill.fillAmount = 0.6f;
                break;
            case 0:
                cianPointsFill.fillAmount = 1;
                break;
        }
        switch (player2Health)
        {
            case 3:
                ornPointsFill.fillAmount = 0;
                break;
            case 2:
                ornPointsFill.fillAmount = 0.35f;
                break;
            case 1:
                ornPointsFill.fillAmount = 0.6f;
                break;
            case 0:
                ornPointsFill.fillAmount = 1;
                break;
        }

        player1HealthText.text = ($"{player1Health}");
        player2HealthText.text = ($"{player2Health}");
    }


   void TimerHide()
    {
       
        if (invisableTimer <= 1)
        {
            //TimerText.enabled = false;
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
                soundManager.PlaySFX(soundManager.punch);

                int randomNumber = Random.Range(0, 3);

                if (randomNumber == 0)
                {
                    soundManager.PlaySFX(soundManager.applaud1);
                }
                else if (randomNumber == 1)
                {
                    soundManager.PlaySFX(soundManager.applaud2);
                }
                else if (randomNumber == 2)
                {
                    soundManager.PlaySFX(soundManager.applaud3);
                }


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
                soundManager.PlaySFX(soundManager.punch);

                int randomNumber = Random.Range(0, 3);

                if (randomNumber == 0)
                {
                    soundManager.PlaySFX(soundManager.applaud1);
                }
                else if (randomNumber == 1)
                {
                    soundManager.PlaySFX(soundManager.applaud2);
                }
                else if (randomNumber == 2)
                {
                    soundManager.PlaySFX(soundManager.applaud3);
                }


                NextRound();
            }
        }

        else if (invisableTimer == 0)
        {
            if (player1Pressed == false && player2Pressed == true)
            {
                player1Health--;
                roundTextTest.text = ($"Round {roundCount} Won by Player 2");
                swapOrn("hurt");
                swapCian("punch");
                roundText.SetActive(true);
                soundManager.PlaySFX(soundManager.punch);

                int randomNumber = Random.Range(0, 3);

                if (randomNumber == 0)
                {
                    soundManager.PlaySFX(soundManager.applaud1);
                }
                else if (randomNumber == 1)
                {
                    soundManager.PlaySFX(soundManager.applaud2);
                }
                else if (randomNumber == 2)
                {
                    soundManager.PlaySFX(soundManager.applaud3);
                }


                NextRound();
            }
           
            else if (player1Pressed == true && player2Pressed == false)
            {
                player2Health--;
                roundTextTest.text = ($"Round {roundCount} Won by Player 1");
                swapOrn("punch");
                swapCian("hurt");
                roundText.SetActive(true);
                soundManager.PlaySFX(soundManager.punch);

                int randomNumber = Random.Range(0, 3);

                if (randomNumber == 0)
                {
                    soundManager.PlaySFX(soundManager.applaud1);
                }
                else if (randomNumber == 1)
                {
                    soundManager.PlaySFX(soundManager.applaud2);
                }
                else if (randomNumber == 2)
                {
                    soundManager.PlaySFX(soundManager.applaud3);
                }


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
        int milliseconds = Mathf.FloorToInt((currentTime - seconds) * 1000);

       TimerText.text = string.Format("{0:0}.{1:0}", seconds, milliseconds);
    }

    void Timer()
    {


        if  (Input.GetKeyUp(KeyCode.Space))
        {
            if (gameStart == false )
            {
                roundCount += 1;
                roundTextNumber.text = ($"Round: {roundCount}");
                Player1TimeUI.text = "0.000";
                Player2TimeUI.text = "0.000";
                roundText.SetActive(false);

                swapOrn("standby");
                swapCian("standby");

                timerOn = true;
                gameStart = true;
                TimerText.CrossFadeAlpha(0, startingTime/4*3, false);
                pressStart.SetActive(false);
                soundManager.PlaySFX(soundManager.round_start);
            }
        }


        //timer

        if (timerOn && gameStart)
        {
            invisableTimer -= Time.deltaTime;

            if (invisableTimer <= 0)
            {
                invisableTimer = 0;
                timerOn = false;
            }

            visableTimer = (invisableTimer/startingTime)*4;

            if (visableTimer >= 3)
            {
                ciFour.SetActive(true);
                ciThree.SetActive(false);
                ciTwo.SetActive(false);
                ciOne.SetActive(false);
            }
            else if (visableTimer >= 2)
            {
                ciFour.SetActive(false);
                ciThree.SetActive(true);
                ciTwo.SetActive(false);
                ciOne.SetActive(false);
            }
            else if (visableTimer >= 1)
            {
                ciFour.SetActive(false);
                ciThree.SetActive(false);
                ciTwo.SetActive(true);
                ciOne.SetActive(false);
            }
            else if (visableTimer > 0)
            {
                ciFour.SetActive(false);
                ciThree.SetActive(false);
                ciTwo.SetActive(false);
                ciOne.SetActive(true);
            }
            else
            {
                ciFour.SetActive(false);
                ciThree.SetActive(false);
                ciTwo.SetActive(false);
                ciOne.SetActive(false);
            }

                //  updateTimer(invisableTimer);
                updateTimer(visableTimer);
        }
        Player1TimeUI.text = player1Time.ToString("0.000");
        Player2TimeUI.text = player2Time.ToString("0.000");

       
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
