using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    private DifficultyController difficultyController;
    private void Awake()
    {
        difficultyController = FindObjectOfType<DifficultyController>();
    }
    public void PlayGame()
    {
        switch (difficultyController.GetDifficulty())
        {
            case DifficultyController.Difficulty.EASY:
                SceneManager.LoadScene("EasyScene");
                break;
            case DifficultyController.Difficulty.NORMAL:
                SceneManager.LoadScene("NormalScene");
                break;
            case DifficultyController.Difficulty.HARD:
                SceneManager.LoadScene("HardScene");
                break;
            default:
                break;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void GoToHowToPlay() 
    {
        SceneManager.LoadScene("HowToPlayScene");
    }
}
