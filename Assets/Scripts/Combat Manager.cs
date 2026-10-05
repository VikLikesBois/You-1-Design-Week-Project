using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public float timeLeft;
    public bool timerOn = false;
    public TextMeshProUGUI TimerText;



    private void Start()
    {
        timerOn = true;
    }

    private void Update()
    {
        timer();
        Combat();
    }
    void Combat()
    {
        
    }







    void updateTimer(float currentTime)

    {
        

        float seconds = Mathf.FloorToInt(currentTime);
        int milliseconds = Mathf.FloorToInt((currentTime - seconds) * 100);

       TimerText.text = string.Format("{0:0}.{1:0}", seconds, milliseconds);
    }

    void timer()
    {
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
    }

}
