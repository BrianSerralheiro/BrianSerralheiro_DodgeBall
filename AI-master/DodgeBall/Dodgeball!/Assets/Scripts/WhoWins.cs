using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhoWins : MonoBehaviour {

    // Use this for initialization
    public void OnMouseClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
