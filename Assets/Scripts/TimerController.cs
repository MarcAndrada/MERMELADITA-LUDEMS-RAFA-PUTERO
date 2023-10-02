using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private float maxTimeOfGame;

    private float timer;

    [SerializeField]
    private TextMeshProUGUI textToPrint;

    [SerializeField]
    CanonController canonController;

    private void Awake()
    {
        timer = maxTimeOfGame;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textToPrint.text = timer.ToString("0");
        timer -= Time.deltaTime;

        if (timer < 40)
            canonController.SetCanSpawnLaser(true);
        if(timer <= 20)
            canonController.SetCanSpawnSaw(true);
    }

    public float GetTimer()
    {
        return timer;
    }
}
