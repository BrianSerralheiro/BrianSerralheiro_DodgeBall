using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnMouseClick()
    {
        SceneManager.LoadScene("dodgeball");
    }
}
