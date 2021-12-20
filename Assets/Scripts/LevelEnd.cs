using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{

    public string LevelToLoad;
    private bool LevelCompleted;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&& LevelCompleted==true)
        {
            UIController.instance.StartFadeToBlack();
            SceneManager.LoadScene(LevelToLoad);
         
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canMove = false;
            AudioManager.instance.PlayLevelWin();
          

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UIController.instance.nextLevelScreen.SetActive(true);
           
            LevelCompleted = true;
            // StartCoroutine(LevelManager.instance.LevelEnd());
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UIController.instance.nextLevelScreen.SetActive(false);
            AudioManager.instance.StopLevelWin();
            LevelCompleted = false;
            // StartCoroutine(LevelManager.instance.LevelEnd());
        }
    }
}
   
