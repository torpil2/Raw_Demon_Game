using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource level1Music, GameOverMusic, winMusic;
    /// <summary>
    ///SFX
    /// </summary>
    public AudioSource[] sfx;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGameOver()
    {
        level1Music.Stop();
        GameOverMusic.Play();
    }

    public void PlayLevelWin()
    {
        level1Music.Stop();
        winMusic.Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

   

}
