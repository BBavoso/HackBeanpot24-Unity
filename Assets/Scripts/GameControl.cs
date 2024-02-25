using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public float totalPlastics = 11577f;
    public float totalSpawned = 0f;
    public float plasticsOnScreen = 0f;
    public float plasticsRatio = 0f;
    public float threshold = 0.15f;
    public float plasticsRemoved = 0f;
    public TotalSpawnedBar totalSpawnedBar;
    public FailRatioBar failRatioBar;
    public static float score;
    float int_score = 0f;
       
    void Start() 
    {
        totalSpawnedBar.SetSpawnedSlider(0);
        failRatioBar.SetFailSlider(0);
    }

    // Update is called once per frame
    void Update()
    {
        plasticsOnScreen = GameObject.FindGameObjectsWithTag("trash").Length;
        plasticsRemoved = totalSpawned - plasticsOnScreen;
        plasticsRatio = plasticsOnScreen / totalPlastics;
        updateUI(totalSpawned, plasticsRatio);
        int_score = plasticsRemoved /totalPlastics * 100;
        if (plasticsRatio > threshold) {
            Debug.Log("LOSER LOSER LOSER");
            gameOver();
        }
        if (totalSpawned == totalPlastics && plasticsRatio < threshold) {
            youWin();
        }
    }

    void gameOver() {
        Debug.Log("GAME OVER");
        GameControl.score = int_score;
        SceneManager.LoadScene("LoseScreen");
    }

    void youWin() {
        Debug.Log("YOU WIN YOU WIN WINNER WINNER WINNER");
        GameControl.score = int_score;
        SceneManager.LoadScene("WinScreen");
    }

    void updateUI(float spawned, float ratio)
    {
        totalSpawnedBar.SetSpawnedSlider(spawned);
        failRatioBar.SetFailSlider(ratio);
    }
}

