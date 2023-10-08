using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    public float maxTimeOfGame;

    [HideInInspector] public float timer;

    [SerializeField]
    private TextMeshProUGUI textToPrint;

    [SerializeField]
    CanonController canonController;

    private bool timeEnded;

    [SerializeField]
    private PlayerController player;

    //Zoom

    private Camera cam;

    private void Awake()
    {
        timer = maxTimeOfGame;
        timeEnded = false;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!timeEnded)
        {
            textToPrint.text = timer.ToString("0");
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                canonController.gameObject.SetActive(false);
                timeEnded = true;
                Destroy(cam.GetComponent<CameraShake>());
                cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, cam.transform.position.z);
                player.GetComponent<Collider2D>().enabled = false;
                player.GetComponent<Rigidbody2D>().simulated = false;
                player.enabled = false;
            }
        }
        else
        {
            ZoomCamera();
        }
    }

    void ZoomCamera()
    {

        if (cam.orthographicSize <= 0.001)
        {
            //FIN
            SceneManager.LoadScene("TitleScreenScene");
        }
        else
        {
            cam.orthographicSize -= Time.deltaTime;
        }


    }

    public float GetTimer()
    {
        return timer;
    }
}
