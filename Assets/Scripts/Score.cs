using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public GameObject gameoverpanel;

    void ShowOverPanel()
    {
        gameoverpanel.SetActive(true);
    }

    public void GameOver()
    {
        ShowOverPanel(); 
    }

    public void Restart()
    {
        SceneManager.LoadScene("Dino");
    }
}
