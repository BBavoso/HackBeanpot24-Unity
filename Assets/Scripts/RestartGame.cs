using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RestartGame : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public void Update()
    {
        scoreText.text = Mathf.Round(GameControl.score).ToString() + '%';
    }
    public void StartGame()
    {
        SceneManager.LoadScene("StartScene");
    }
}
