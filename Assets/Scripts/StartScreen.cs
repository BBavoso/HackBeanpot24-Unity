using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public string firstLevel;

void Start(){}
void update(){}
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }
}
