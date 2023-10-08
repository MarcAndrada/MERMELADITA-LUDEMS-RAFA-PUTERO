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
                break;
            case DifficultyController.Difficulty.NORMAL:
                break;
            case DifficultyController.Difficulty.HARD:
                break;
            default:
                break;
        }
        SceneManager.LoadScene("BalllsScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }
}
