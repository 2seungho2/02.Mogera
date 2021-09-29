using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Ready,
    Play,
    End
}

public class GameManager : MonoBehaviour
{
    public GameState GS;

    public Hole[] holes;
    public float limitTime;
    public Text timeText;
    public int count_Bad;
    public int count_Good;

    public GameObject finishGUI;
    public Text final_Count_Bad;
    public Text final_Count_Good;
    public Text final_Score;

    public AudioClip ready_Audio;
    public AudioClip go_Audio;
    public AudioClip end_Audio;
    
    private void Start()
    {
        GetComponent<AudioSource>().clip = ready_Audio;
        GetComponent<AudioSource>().Play();
        limitTime = 10.0f;
    }

    public void Go()
    {
        GS = GameState.Play;
        GetComponent<AudioSource>().clip = go_Audio;
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (GS == GameState.Play)
            limitTime -= Time.deltaTime;
        if (limitTime <= 0)
        {
            limitTime = 0;
            End();
        }
        timeText.text = string.Format("{0:N2}", limitTime);
    }

    void End()
    {
        GS = GameState.End;
        final_Count_Bad.text = string.Format("나쁜 두더지:" + "{0}", count_Bad);
        final_Count_Good.text = string.Format("착한 두더지:" + "{0}", count_Good);
        final_Score.text = string.Format("총점: " + "{0}", count_Bad * 100 - count_Good * 1000);
        finishGUI.gameObject.SetActive(true);
    }   
}
