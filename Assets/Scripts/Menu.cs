using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    static int currentSceneIndex;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPlayButton()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
