using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour    
{

    public static UIController instance;

    public Slider healthSlider;
    public Text healthText;
    public GameObject deathScreen;
    public Slider ManaBarSlider;
    public Animator anim;
    public GameObject nextLevelScreen;
    public Image fadeScreen;

    public float fadeSpeed;
    private bool fadeToBlack, fadeOutBlack;

    public string newGameScene, mainMenuScene;

    public GameObject pauseMenu;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ManaBarSlider.maxValue = PlayerController.instance.dashCooldown;
        fadeOutBlack = true;
        fadeToBlack = false;
    }

    // Update is called once per frame
    void Update()
    {
        ///Screen Fade////
       if(fadeOutBlack==true)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f,fadeSpeed*Time.deltaTime));
            if(fadeScreen.color.a==0f)
            {
                fadeOutBlack = false;
            }
        }

        if(fadeToBlack==true)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }


        if (instance.ManaBarSlider.value >= 1)
        {

            anim.SetBool("ManaBarValueCheck", true);

        }
        if (instance.ManaBarSlider.value <= 0)
        {
            anim.SetBool("ManaBarValueCheck", false);
        }

    }
    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameScene);
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void Resume()
    {
        LevelManager.instance.pauseUnpause();
    }

}
