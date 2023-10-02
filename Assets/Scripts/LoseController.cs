using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LoseController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeLeft;
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
        timeLeft.text = "You survived " + (60 - _timeLeft).ToString("0") + " seconds";
    }

}
