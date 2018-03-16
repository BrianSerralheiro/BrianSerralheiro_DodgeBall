using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchTime : MonoBehaviour
{
    public static bool playerWins;
    
    [SerializeField]
    private float Timer;
    [SerializeField]
    private Transform[] enemyTeam;
    [SerializeField]
    private Transform[] playerTeam;
    [SerializeField]
    private Text timerText;

    // Update is called once per frame
    void Update()
    {
        timerText.text = Timer.ToString();
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            SceneManager.LoadScene("WhoWins");
        }

        int i = 0;
        //verifies how many objects on the list are deactivated
        foreach(var t in enemyTeam)
        {
            if(!t.gameObject.activeSelf)
            {
                i++;
            }
        }
        if(i == enemyTeam.Length)
        {
            playerWins = true;
            SceneManager.LoadScene("WhoWins");
        }
        //reset the counter
        i = 0;
        foreach (var t in playerTeam)
        {
            if (!t.gameObject.activeSelf)
            {
                i++;
            }
        }
        if (i == playerTeam.Length)
        {
            SceneManager.LoadScene("WhoWins");
        }
    }
}
