using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour
{
    private AudioSource finishsound;

    private bool LevelComp = false;
    private void Start()
    {
        finishsound = GetComponent<AudioSource>();     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !LevelComp)
        {
            finishsound.Play();
            LevelComp = true;
            Invoke("Completelevel", 2f);
        }
    }

    private void Completelevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}
