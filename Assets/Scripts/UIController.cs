using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour    
{

    public static UIController instance;

    public Slider healthSlider;
    public Text healthText;
    public GameObject deathScreen;
    public Slider ManaBarSlider;
    public Animator anim;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ManaBarSlider.maxValue = PlayerController.instance.dashCooldown;   
    }

    // Update is called once per frame
    void Update()
    {

        if (instance.ManaBarSlider.value >= 1)
        {

            anim.SetBool("ManaBarValueCheck", true);

        }
        if (instance.ManaBarSlider.value <= 0)
        {
            anim.SetBool("ManaBarValueCheck", false);
        }

    }


}
