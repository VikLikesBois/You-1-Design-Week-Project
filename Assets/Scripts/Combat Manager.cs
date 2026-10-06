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
    public TextMeshProUGUI Player1TimeUI;
    public TextMeshProUGUI Player2TimeUI;

    //combat stuff
    bool player1Pressed;
    bool player2Pressed;
   public bool player1Missed = false;
   public bool player2Missed = false;

    float player1Time;
    float player2Time;

    private void Start()
    {
        timerOn = true;
    }

    private void Update()
    {
        timer();
        Combat();
        WinCheck();
    }
    void Combat()
    {
        //punch controls for player 1
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


    void WinCheck()
    {
     
        if (player1Pressed == true && player2Pressed == true)
        {
            if (player1Time > player2Time)
            {
                Debug.Log("player 2 wins");
            }
            else if (player1Time < player2Time)
            {
                Debug.Log("player 1 wins");
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

    void timer()
    {
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
