using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour
{
    
    void OnEnable()
    {
        GetComponent<AudioSource>().Play();
    }

    public void BtnReplay()
    {
        SceneManager.LoadScene("MainScene");
    }
}