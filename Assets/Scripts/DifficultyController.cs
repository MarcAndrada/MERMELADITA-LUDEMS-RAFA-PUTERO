using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DifficultyController : MonoBehaviour
{
    public enum Difficulty { EASY, NORMAL, HARD };

    private Difficulty currentDifficulty = Difficulty.EASY;

    [Space, SerializeField]
    private TextMeshProUGUI currentDifficultyText;



    // Start is called before the first frame update
    void Start()
    {
        SetDifficulty(currentDifficulty);
    }

    private void SetDifficulty(Difficulty _nextDifficulty) 
    {
        currentDifficultyText.text = _nextDifficulty.ToString();
    }

    public void DownDifficulty() 
    {
        currentDifficulty--;
        currentDifficulty = (Difficulty)Mathf.Clamp((int)currentDifficulty, (int)Difficulty.EASY, (int)Difficulty.HARD);
        SetDifficulty(currentDifficulty);
    }

    public void UpDifficulty() 
    {
        currentDifficulty++;
        currentDifficulty = (Difficulty)Mathf.Clamp((int)currentDifficulty, (int)Difficulty.EASY, (int)Difficulty.HARD);
        SetDifficulty(currentDifficulty);
    }

    public Difficulty GetDifficulty() 
    {
        return currentDifficulty;
    }
}
