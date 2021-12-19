using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{

    public GameObject brokenpiece;
    public float dissappeartime;

    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;
    public int breakSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PlayerBullet")
        {
            AudioManager.instance.PlaySFX(breakSound);
            Destroy(gameObject);
            int rotation = Random.Range(0, 4);
           GameObject broken =  Instantiate(brokenpiece, transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));
            Destroy(broken, dissappeartime);

            //drop items////////
            if(shouldDropItem)
            {
                float dropChance = Random.Range(0f, 100f);
                if(dropChance < itemDropPercent)
                {
                    int randomItem = Random.Range(0, itemsToDrop.Length);

                    Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
                }

            }
        }
    }
}
