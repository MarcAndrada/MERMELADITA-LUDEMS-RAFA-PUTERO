using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private float maxTimeOfGame;

    [HideInInspector] public float timer;

    [SerializeField]
    private TextMeshProUGUI textToPrint;

    [SerializeField]
    CanonController canonController;

    private bool timeEnded;

    [SerializeField]
    private GameObject player;

    //Zoom

    private Camera camera;

    private bool zoomEnded;

    private void Awake()
    {
        timer = maxTimeOfGame;
        timeEnded = false;
        camera = Camera.main;
        zoomEnded = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0.1)
        {
            timeEnded = true;
          
            camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.x, camera.transform.localPosition.z);
            ZoomCamera();
        }
        if (timeEnded == false)
        {
            textToPrint.text = timer.ToString("0");
            timer -= Time.deltaTime;
        }
    }

    void ZoomCamera()
    {
        if (camera.orthographicSize <= 0.01)
        {
            zoomEnded = true;

        }
        if(zoomEnded == false)
            camera.orthographicSize -= Time.deltaTime;
    }

    public float GetTimer()
    {
        return timer;
    }
}
