using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    private Vector2 moveInput;
    public Rigidbody2D theRB;
    public Transform gunArm;

    private Camera TheCam;

    public Animator anim;

    public GameObject bulletToFire;

    public Transform firePoint;

    public float timeBetweenShots;

    private float shotCounter;

    public SpriteRenderer bodySR;

    public GameObject demonMode;
    public int dashSoundFX;
    

    private float activeMoveSpeed,activeTimeBetweenShots;
   
    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 1f, dashInvincibility = .5f,demonModeAttackBetweenShoots;
    public float dashCounter, dashCoolCounter;

    [HideInInspector]
    public bool canMove = true;
    

    

    private void Awake()
    {
        instance = this;
        activeMoveSpeed = moveSpeed;
        activeTimeBetweenShots = timeBetweenShots;
    }



    // Start is called before the first frame update
    void Start()
    {
        TheCam = Camera.main;
        demonMode.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            moveInput.Normalize();
            //time*deltaTime oyunda geçen frame baþýna süre
            // transform.position += new Vector3(moveInput.x*Time.deltaTime*moveSpeed, moveInput.y*Time.deltaTime*moveSpeed,0f);
            theRB.velocity = moveInput * activeMoveSpeed;

            //where mouse cursor currently is//
            Vector3 mousePos = Input.mousePosition;
            //////////////////////


            //get screen point///
            Vector3 screenPoint = TheCam.WorldToScreenPoint(transform.localPosition);
            //////////////////

            //rotate gun arm //////////////////
            if (mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
                gunArm.localScale = Vector3.one;

            }

            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);

            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

            gunArm.rotation = Quaternion.Euler(0, 0, angle);
            ///////////////////////////////////

            //Firing Bullets///////////////////

            if (Input.GetMouseButtonDown(0))
            {

                GameObject bullet = Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                // Instantiate(bulletToFire,firePoint.position,firePoint.rotation);
                shotCounter = activeTimeBetweenShots;
                Destroy(bullet, 5f);

            }

            if (Input.GetMouseButton(0))
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    GameObject bullet = Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    // Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    shotCounter = activeTimeBetweenShots;
                    Destroy(bullet, 5f);
                }
            }
            /////////////////////////////////////


            //DASH ACTION///////////////////////
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    AudioManager.instance.PlaySFX(dashSoundFX);
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;
                    // PlayerHealthController.instance.MakeInvinsible(dashInvincibility);
                    demonMode.SetActive(true);
                    anim.SetTrigger("dash");

                    activeTimeBetweenShots = demonModeAttackBetweenShoots;
                    UIController.instance.ManaBarSlider.value = 0;
                }

            }
            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCooldown;
                    activeTimeBetweenShots = timeBetweenShots;
                    demonMode.SetActive(false);
                    UIController.instance.ManaBarSlider.value = 1;

                }
            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }

            ////////////////////////////////////// 



            if (moveInput != Vector2.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }

    }
   
       
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
