using TMPro;
using UnityEngine;

public class Pause : MonoBehaviour
{
    static bool isPaused;
    public GameObject retryButton;
    public GameObject quitButton;
    public GameObject textObj;

    private void Start()
    {
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            Time.timeScale = 0f;
            isPaused = true;
            retryButton.SetActive(true);
            quitButton.SetActive(true);
            textObj.GetComponent<TextMeshProUGUI>().text = "Paused";
            textObj.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            Time.timeScale = 1f;
            isPaused = false;
            retryButton.SetActive(false);
            quitButton.SetActive(false);
            textObj.SetActive(false);
        }
    }
}
