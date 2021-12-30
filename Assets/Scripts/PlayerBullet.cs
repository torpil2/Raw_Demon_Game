using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D theRB;
    public GameObject impactEffect;
    public int damageToGive = 50;
 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //objenin yönünü deðiþtirir.
        theRB.velocity = transform.right * speed;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      
        if (other.tag == "Enemy")
        {
          
           GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
            other.GetComponent<EnemyController>().DamageEnemy(damageToGive);
            Destroy(gameObject);
            Destroy((effect),1f);
        }
       
        GameObject effect2 = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy((effect2), 1f);


    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
