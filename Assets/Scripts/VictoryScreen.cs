using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{

    public float waitForKey = 2f;

    public GameObject anyKeyText;

    public string MainMenuScene;




    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(waitForKey>0)
        {
            waitForKey -= Time.deltaTime;
        }
        else if (waitForKey <= 0)
        {
            anyKeyText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
            
                SceneManager.LoadScene(MainMenuScene);
            }
        }
        

    }
}
