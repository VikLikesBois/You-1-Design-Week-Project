using UnityEngine;

public class RoundManagerScript : MonoBehaviour
{
    public int player1Health = 3;
    public int player2Health = 3;
    public int roundCount = 1;

    public void StartRound()
    {
        //start timer thing
    }

    public void AwardWin(string player)
    {
        //display win ui
    }

    public void EndRound()
    {

        if (player1Health == 0)
        {
            AwardWin("Player 2");
        }
        else if (player2Health == 0)
        {
            AwardWin("Player 1");
        }
        else
        {
            roundCount += 1;
            StartRound();
        }
    }

}