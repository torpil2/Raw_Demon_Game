using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToLoad = 4f;

    public bool isPaused;

    public int currentCoins;

  //  public string nextLevel;
    

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUnpause();
        }
    }

    /* public IEnumerator LevelEnd()
      {
         AudioManager.instance.PlayLevelWin();

         yield return new WaitForSeconds(waitToLoad);



      }*/

    public void pauseUnpause()
    {
        if(!isPaused)
        {
            UIController.instance.pauseMenu.SetActive(true);
            isPaused = true;

            Time.timeScale = 0f;
        }
        else
        {
            UIController.instance.pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void GetCoins(int amount)
    {
        currentCoins += amount;
        UIController.instance.TotalCoin.text = currentCoins.ToString();
    }
    public void SpendCoins(int amount)
    {
        if (currentCoins-amount >= 0)
        {
            currentCoins -= amount;
        }
        UIController.instance.TotalCoin.text = currentCoins.ToString();


    }
}
