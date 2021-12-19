using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour

{

    public static PlayerHealthController instance;
    public int currentHealth;
    public int maxHealth;


    public float damageInvincLength = 1f;
    private float �nvincCount;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(�nvincCount>0)
        {
            �nvincCount -= Time.deltaTime;

            if(�nvincCount <= 0)
            {
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 1f);

            }

        }
    }

    public void DamagePlayer()
    {
        if (�nvincCount <= 0)
        {
                    
        currentHealth--;

            �nvincCount = damageInvincLength;
            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, .5f);

      if (currentHealth <= 0)
      {
            PlayerController.instance.gameObject.SetActive(false);
            UIController.instance.deathScreen.SetActive(true);
            AudioManager.instance.PlayGameOver();
      }

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }

    }

    public void MakeInvinsible(float Length)
    {

        if (�nvincCount <= 0)
        {


            �nvincCount = damageInvincLength;
            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, .5f);
        }
    }

    //calling health pickup
    public void HealPlayer(int HealAmount)
    {
        currentHealth += HealAmount;
        if(currentHealth>maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

    }

    public void MaxHealthPlus(int MaxHealAmount)
    {
        maxHealth += MaxHealAmount;      
        
           
        
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

}
