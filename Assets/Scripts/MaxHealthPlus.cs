using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthPlus : MonoBehaviour
{
    public int PlusMaxHealthAmount = 1;
    public int PickupSound;
   // public float waitToBeCollected2 = .5f;
    // Start is called before the first frame update
    void Start()
    {
       /* if (waitToBeCollected2 > 0)
        {
            waitToBeCollected2 -= Time.deltaTime;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.tag == "Player" && waitToBeCollected2 <= 0)
        if (other.tag == "Player" )
        {
            AudioManager.instance.PlaySFX(PickupSound);
            PlayerHealthController.instance.MaxHealthPlus(PlusMaxHealthAmount);
            Destroy(gameObject);
        }
    }
}
