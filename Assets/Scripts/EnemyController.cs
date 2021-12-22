using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;

    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    public Animator anim;

    public int health = 150;

    public GameObject[] deathSplatters;
    public int dissapeartime = 5;
    public GameObject enemyImpactEffect;

    public bool shouldShoot;

    public GameObject bullet;
    public Transform firepoint;
    public float fireRate;
    private float fireCounter;
    public float shootRange;
    public float DontShootRange;
    public Vector3 pushBack;

    public SpriteRenderer theBody;
    public int damageSound;
   // private Transform targetTransform;


   // private float zOffset = 5f;   

    // Start is called before the first frame update
    void Start()
    {
      //  targetTransform.position = transform.position;


    }

    // Update is called once per frame
    void Update()
    {
       if(theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer  )
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else
            {
                moveDirection = Vector3.zero;
            }


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
        Vector3 pushXpos = new Vector3(0.5f, 0f, 0f);
          Vector3 pushYpos = new Vector3(0f, 0.5f, 0f);

        if(Mathf.Abs(Mathf.Round( PlayerController.instance.position.y)) >= Mathf.Abs(Mathf.Round( transform.position.y)))
        {   

        if ( PlayerController.instance.position.x <  transform.position.x)
        {
            transform.position += pushXpos;
        }
        else
        {
            transform.position -= pushXpos;
        }
        }
        else if (Mathf.Abs(Mathf.Round(PlayerController.instance.position.y)) <= Mathf.Abs(Mathf.Round(transform.position.y)))
        {                
        
        if ( PlayerController.instance.position.y < transform.position.y)
        {
            transform.position += pushYpos;
        }
        else
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
