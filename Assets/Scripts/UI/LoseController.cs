using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LoseController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeLeft;

    private TimerController timerController;
    private void Awake()
    {
        timerController = FindObjectOfType<TimerController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("TitleScreenScene");
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetTimer(float _timeLeft) 
    {
        timeLeft.text = "You survived " + (timerController.maxTimeOfGame - _timeLeft).ToString("0") + " seconds";
        Debug.Log("Time " + timerController.maxTimeOfGame + " - Left " + _timeLeft);
    }

}
