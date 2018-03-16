using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WhoWins : MonoBehaviour
{
    [SerializeField]
    private Text teamPlayerWins;
    [SerializeField]
    private Text teamAIWins;

    void Start()
    {
        teamPlayerWins.enabled = MatchTime.playerWins;
        teamAIWins.enabled = !MatchTime.playerWins;
    }


    public void OnMouseClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
