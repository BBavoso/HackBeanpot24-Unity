using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Time.timeScale = 0f;
    }

    void youWin() {
        Debug.Log("YOU WIN YOU WIN WINNER WINNER WINNER");
    }

    void updateUI(float spawned, float ratio)
    {
        totalSpawnedBar.SetSpawnedSlider(spawned);
        failRatioBar.SetFailSlider(ratio);
    }
}

