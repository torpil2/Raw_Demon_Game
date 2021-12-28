using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public  EnemyController instance1;
    public Rigidbody2D theRB;
    public float moveSpeed;

    [Header("Chase Player")]
    public bool shouldChasePlayer;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    [Header("Patrolling")]
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    [Header("Run Away")]
    public bool shouldRunAway;
    public float runAwayRange;

    [Header("Wander")]
    public bool shouldWander;
    public float wanderLength, pauseLength;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;

    public Animator anim;

    public int health = 150;

    public GameObject[] deathSplatters;
    public int dissapeartime = 5;
    public GameObject enemyImpactEffect;

    [Header("Shooting")]
    public bool shouldShoot;
    [Header("Push Back")]
    public bool shouldPush;

    public GameObject bullet;
    public Transform firepoint;
    public float fireRate;
    private float fireCounter;
    public float shootRange;
    public float DontShootRange;
    public Vector3 pushBack;  

    [Header("Variables")]
    public SpriteRenderer theBody;
    public int damageSound;
    Camera cam;
   // private Transform targetTransform;


   // private float zOffset = 5f;   

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //  targetTransform.position = transform.position;
        cam = Camera.main;
      if(shouldWander)
        {
            pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);

        }

    }

    // Update is called once per frame
    void Update()
    {
        /*  if(theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)*/
        if (theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            moveDirection = Vector3.zero;

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer && shouldChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else
            {

                //enemy hellwater//////////////////////
                if (shouldWander)
                {
                    if (wanderCounter > 0)
                    {
                        wanderCounter -= Time.deltaTime;

                        moveDirection = wanderDirection;
                        if (wanderCounter <= 0)
                        {
                            pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
                        }
                    }
                    if (pauseCounter > 0)
                    {
                        pauseCounter -= Time.deltaTime;

                        if (pauseCounter <= 0)
                        {
                            wanderCounter = Random.Range(wanderLength * .75f, wanderLength * 1.25f);

                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), 0);
                        }

                    }

                }
                if (shouldPatrol)
                {
                    //Distance iki mesafe arasýndaki uzaklýðý alýr
                    moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;
                    if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < .2f)
                    {
                        currentPatrolPoint++;
                        if (currentPatrolPoint >= patrolPoints.Length)
                        {
                            currentPatrolPoint = 0;

                        }
                    }
                    }
                
                }

            //enemy - hellboar
            if(shouldRunAway && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < runAwayRange)
            {
                //opposite direction of player     

                moveDirection = transform.position - PlayerController.instance.transform.position;           
                                               
                                               
            }

            /* else
             {
                 moveDirection = Vector3.zero;
             }*/
             
            moveDirection.Normalize();

            theRB.velocity = moveDirection * moveSpeed;

            if (moveDirection != Vector3.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

            if (shouldShoot && Vector3.Distance(transform.position,PlayerController.instance.transform.position)<shootRange &&
                Vector3.Distance(transform.position, PlayerController.instance.transform.position) > DontShootRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firepoint.position, firepoint.rotation);
                }
            }
        }
        else
        {
            theRB.velocity = Vector3.zero;
        }        

    }

    public void DamageEnemy(int damage)
    {
        //push back when hit//////////////
        Vector3 pushXpos = new Vector3(0.5f, 0f, 0f);
        Vector3 pushYpos = new Vector3(0f, 0.5f, 0f);
       
        Vector3 enemyscreenpoint = cam.WorldToScreenPoint(transform.position);
        Vector3 playerscreenpoint = cam.WorldToScreenPoint(PlayerController.instance.position);
        float xpointplayer = playerscreenpoint.x;
        float ypointplayer = playerscreenpoint.y;
        float xpointenemy = enemyscreenpoint.x;
        float ypointenemy = enemyscreenpoint.y;
        float ydiffplayerandenemy = Mathf.Abs(ypointplayer - ypointenemy);

        if (shouldPush)
        {
            if (xpointplayer > xpointenemy && ydiffplayerandenemy <= 100)
            {
                ;
                transform.position -= pushXpos;
            }
            else if (xpointplayer < xpointenemy && ydiffplayerandenemy <= 100)
            {
                transform.position += pushXpos;

            }

            else if (ypointplayer < ypointenemy && ydiffplayerandenemy >= 100)
            {

                transform.position += pushYpos;
            }
            else if (ypointplayer > ypointenemy && ydiffplayerandenemy >= 100)
            {

                transform.position -= pushYpos;
            }
        }
        

        health -= damage;

            GameObject effect =  Instantiate(enemyImpactEffect, transform.position, transform.rotation);

        AudioManager.instance.PlaySFX(damageSound);
        if (health <= 0)
        {
            Destroy(gameObject);

            int selectedSplatter = Random.Range(0, 3);
            int rotation = Random.Range(0, 4);
            GameObject clone= Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f,0f,rotation*90f));
            
           
          //  Destroy((clone),5);         

            // Instantiate(deathSplatter, transform.position, transform.rotation);
        }
        Destroy((effect), 1);

    }

 
}
