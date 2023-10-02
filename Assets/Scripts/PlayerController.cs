using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Movement Variables
    [Header("Movement"), SerializeField]
    private float accelerationSpeed;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;

    private Vector2 inputValue;
    private float acceleration;
    #endregion

    #region Parry Variables    
    [Space]
    [Header("Dash"), SerializeField]
    private float parryDuration = 0.2f;
    private float timeWaitedParring = 0;
    [SerializeField]
    private float parryCD = 1;
    private float timeWaitedParryCD = 0;
    [SerializeField]
    private float minScale;
    private Vector3 minScaleV;
    [SerializeField]
    private float maxScale;
    private Vector3 maxScaleV;
    private float lerpProcess;
    private bool isParring = false;
    private bool canParry = true;
    [SerializeField]
    private Collider2D parryColl;

    [SerializeField]
    private SpriteRenderer parrySR;
    [SerializeField]
    private Color parryColor;
    private Color currentParryColor;
    private float parryColorProcess;
    #endregion

    #region Dash Variables
    [Space]
    [Header("Dash"), SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float dashCD;
    private float dashTimeWaited = 0;

    private Vector2 dashInput;
    private bool isDashing = false;
    private bool canDash = true;

    [SerializeField]
    private SpriteRenderer dashSR;
    [SerializeField]
    private Color dashColor;
    private Color currentDashColor;
    private float dashColorProcess;

    #endregion

    private Rigidbody2D rb2d;
    private Animator animator;
    private BlastEffect blast;
    [SerializeField]
    private ParticleSystem moveParticle;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        blast = GetComponent<BlastEffect>();
        moveParticle.Stop();

        minScaleV = new Vector3(minScale, minScale, minScale);
        maxScaleV = new Vector3(maxScale, maxScale, maxScale);
    }

    void Update()
    {
        inputValue = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        RotatePlayer();
        Dash();
        MovePlayer();
        Parry();
    }

    private void MovePlayer()
    {
        if (!isDashing)
        {
            if (inputValue == Vector2.zero)
            {
                if(!moveParticle.isStopped)
                    moveParticle.Stop();
                acceleration = 0;
                animator.SetBool("Moving", false);
            }
            else
            {
                if(!moveParticle.isPlaying)
                    moveParticle.Play();
                acceleration += accelerationSpeed * Time.deltaTime;
                animator.SetBool("Moving", true);
            }


            acceleration = Mathf.Clamp01(acceleration);

            // Apply speed
            rb2d.velocity = inputValue * speed * acceleration;
        }
    }
    private void RotatePlayer() 
    {
        Vector3 lookAtPos = ((transform.position + (Vector3)inputValue * 20) - transform.position) * - 1;

        transform.up = Vector3.Lerp(transform.up, lookAtPos, Time.deltaTime * rotationSpeed);
    }

    

    #region Parry Functions
    private void Parry() {
        //Activar Parry
        if(Input.GetKey(KeyCode.Space) && !isParring && canParry)
        {
            isParring = true;
            canParry = false;
            StartCoroutine(blast.Blast());
            parryColl.enabled = true;
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
            parryColl.enabled = false;
        }
		    
    }

    private void WaitParryCD()
    {
        timeWaitedParryCD += Time.deltaTime;

        //Cambiar el color gradualmente
        parryColorProcess = timeWaitedParryCD / parryCD;
        currentParryColor = Color.Lerp(parryColor, Color.white, parryColorProcess);
        parrySR.color = currentParryColor;

        if (timeWaitedParryCD >= parryCD)
        {
            timeWaitedParryCD = 0;
            canParry = true;
            parrySR.color = Color.white;
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

            transform.localScale = Vector3.Lerp(minScaleV, maxScaleV, lerpProcess);
        }
        else if (transform.localScale.x > minScaleV.x)
        {
            lerpProcess += Time.deltaTime / 0.3f;

            if (lerpProcess > 1)
            {
                lerpProcess = 1;
            }

            transform.localScale = Vector3.Lerp(maxScaleV, minScaleV, lerpProcess);
            //Reseteamos el valor del lerp
            if (transform.localScale.x == minScaleV.x)
                lerpProcess = 0;
        }
    }

    #endregion

    #region Dash Functions
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            isDashing = true;
            canDash = false;
            dashInput = inputValue;
            animator.SetTrigger("Dash");
            dashSR.color = dashColor;
            gameObject.layer = LayerMask.NameToLayer("PlayerDash");
        }

        if (isDashing)
        {
            WaitToStopDash();
        }
        else if (!canDash)
        {
            WaitDashCD();
        }
    }

    private void WaitToStopDash()
    {
        dashTimeWaited += Time.deltaTime;
        rb2d.velocity = dashInput * dashSpeed;
        if (dashTimeWaited >= dashDuration)
        {
            isDashing = false;
            dashTimeWaited = 0;
            dashColorProcess = 0;
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    private void WaitDashCD()
    {
        dashTimeWaited += Time.deltaTime;

        //Cambiar el color gradualmente
        dashColorProcess = dashTimeWaited / dashCD;
        currentDashColor = Color.Lerp(dashColor, Color.white, dashColorProcess);
        dashSR.color = currentDashColor;
        
        if (dashTimeWaited >= dashCD)
        {
            canDash = true;
            dashTimeWaited = 0;
            dashSR.color = Color.white;
        }


        

    }
    #endregion



    public void Die() 
    {
        //Esta funcion se llama cuando el player recibe algun golpe (lo mata)
        //TODO

        Debug.Log("HAS MUERTO");
    }
}
