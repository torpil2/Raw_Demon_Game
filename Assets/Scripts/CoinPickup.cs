using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;
    public float waitToBeCollected;
    public static CoinPickup instance;
    //public bool isCollected = false;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
     if(waitToBeCollected>0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (/*isCollected && */other.tag=="Player" && waitToBeCollected<= 0)
        {
            LevelManager.instance.GetCoins(coinValue);
           // isCollected = true;
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(6);
        }
     

    }
}
