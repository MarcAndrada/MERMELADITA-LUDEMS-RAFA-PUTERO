using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables de la velocidad

    [SerializeField]
    private float accelerationSpeed;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;

    private Vector2 inputValue;
    private float acceleration;


    #region Parry Variables

    private bool isParring = false;
    private bool canParry = true;
    [SerializeField]
    private float parryDuration = 0.2f;
    private float timeWaitedParring = 0;
    [SerializeField]
    private float parryCD = 1;
    private float timeWaitedParryCD = 0;
    [SerializeField]
    private Vector3 minScale;
    [SerializeField]
    private Vector3 maxScale;
    private float lerpProcess;

    private Rigidbody2D rb2d;

    #endregion

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputValue = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        RotatePlayer();
        MovePlayer();
        Parry();
    }

    private void MovePlayer()
    {


        if (inputValue == Vector2.zero)
            acceleration = 0;
        else
		    acceleration += accelerationSpeed * Time.deltaTime;


        acceleration  = Mathf.Clamp01(acceleration);    

        // Apply speed
        rb2d.velocity = inputValue * speed * acceleration;
    }
    private void RotatePlayer() 
    {
        Vector3 lookAtPos = ((transform.position + (Vector3)inputValue) - transform.position) * - 1;

        transform.up = Vector3.Lerp(transform.up, lookAtPos, Time.deltaTime * rotationSpeed);
    }

    #region Parry Functions
    private void Parry() {
        //Activar Parry
        if(Input.GetKey(KeyCode.Space) && !isParring && canParry)
        {
            isParring = true;
            canParry = false;
        }

        // Espera la duracion del parry
        if (isParring)
        {
            DoingParry();

        }
        else if (!canParry)
        {
            //Espera al CD del parry
            WaitParryCD();
        }

        ScalingParry();

    }

    private void DoingParry()
    {
        timeWaitedParring += Time.deltaTime;

        if(timeWaitedParring >= parryDuration)
        {
            timeWaitedParring = 0;
            lerpProcess = 0;
            isParring = false;
        }
		    
    }

    private void WaitParryCD()
    {
        timeWaitedParryCD += Time.deltaTime;

        if (timeWaitedParryCD >= parryCD)
        {
            timeWaitedParryCD = 0;
            canParry = true;
        }

    }

    private void ScalingParry()
    {
        if (isParring)
        {
            lerpProcess += Time.deltaTime / 0.1f;

            if (lerpProcess > 1)
            {
                lerpProcess = 1;
            }

            transform.localScale = Vector3.Lerp(minScale, maxScale, lerpProcess);
        }
        else if (transform.localScale.x > minScale.x)
        {
            lerpProcess += Time.deltaTime / 0.3f;

            if (lerpProcess > 1)
            {
                lerpProcess = 1;
            }

            transform.localScale = Vector3.Lerp(maxScale, minScale, lerpProcess);
            //Reseteamos el valor del lerp
            if (transform.localScale.x == minScale.x)
                lerpProcess = 0;
        }
    }

    #endregion


}
