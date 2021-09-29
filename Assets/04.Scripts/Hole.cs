using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HoleState
{
    None,
    Open,
    Idle,
    Close,
    Catch
}


public class Hole : MonoBehaviour
{
    
    public HoleState HS;

    public Texture[] open_Images;
    public Texture[] idle_Images;
    public Texture[] close_Images;
    public Texture[] catch_Images;

    public bool goodMole;
    public int perGood = 20;

    public Texture[] open2_Images;
    public Texture[] idle2_Images;
    public Texture[] close2_Images;
    public Texture[] catch2_Images;

    public float ani_Speed;
    public float ani_Time;

    int ani_Count;

    public AudioClip open_Audio;
    public AudioClip catch_Audio;

    public GameManager GM;
        
    void Update()
    {
        if (ani_Time >= ani_Speed)
        {
            if (HS == HoleState.Open)
            {
                Open_Ing();
            }
            if (HS == HoleState.Idle)
            {
                Idle_Ing();
            }
            if (HS == HoleState.Close)
            {
                Close_Ing();
            }
            if (HS == HoleState.Catch)
            {
                Catch_Ing();
            }
            ani_Time = 0;
        }
        else ani_Time += Time.deltaTime;

        if (GM.limitTime <= 0)
        { 
            this.gameObject.SetActive(false);
            HS = HoleState.None;
            GetComponent<AudioSource>().Stop();
        }
    }

    public void Open_On()
    {
        HS = HoleState.Open;
        ani_Count = 0;
        GetComponent<AudioSource>().clip = open_Audio;
        GetComponent<AudioSource>().Play();
        int a = Random.Range(0, 100);
        if (a <= perGood)
            goodMole = true;
        else goodMole = false;
        if (GM.GS == GameState.Ready)
            GM.Go();
    }

    public void Open_Ing()
    {
        if(goodMole == false) 
            GetComponent<Renderer>().material.mainTexture = open_Images[ani_Count];
        else GetComponent<Renderer>().material.mainTexture = open2_Images[ani_Count];
        ani_Count += 1;
        if (ani_Count >= open_Images.Length)
        {
            Idle_On();
        }
    }

    public void Idle_On()
    {
        HS = HoleState.Idle;
        ani_Count = 0;
    }

    public void Idle_Ing()
    {
        if(goodMole == false)
            GetComponent<Renderer>().material.mainTexture = idle_Images[ani_Count];
        else GetComponent<Renderer>().material.mainTexture = idle2_Images[ani_Count];
        ani_Count += 1;
        if (ani_Count >= idle_Images.Length)
        {
            Close_On();
        }
    }

    public void Close_On()
    {
        HS = HoleState.Close;
        ani_Count = 0;
    }

    public void Close_Ing()
    {
        if(goodMole == false)
            GetComponent<Renderer>().material.mainTexture = close_Images[ani_Count];
        else GetComponent<Renderer>().material.mainTexture = close2_Images[ani_Count];
        ani_Count += 1;
        if (ani_Count >= close_Images.Length)
        {
            StartCoroutine(Wait());
        }
    }

    public void Catch_On()
    {
        HS = HoleState.Catch;
        ani_Count = 0;
        GetComponent<AudioSource>().clip = catch_Audio;
        GetComponent<AudioSource>().Play();
        if (goodMole == false)
            GM.count_Bad += 1;
        else GM.count_Good += 1;
    }

    public void Catch_Ing()
    {
        if(goodMole == false)
            GetComponent<Renderer>().material.mainTexture = catch_Images[ani_Count];
        else GetComponent<Renderer>().material.mainTexture = catch2_Images[ani_Count];
        ani_Count += 1;
        if (ani_Count >= catch_Images.Length)
        {
            StartCoroutine(Wait());
        }
    }

    public IEnumerator Wait()
    {
        HS = HoleState.None;
        ani_Count = 0;
        float wait_Time = Random.Range(0.5f, 4.5f);
        yield return new WaitForSeconds(wait_Time);
        Open_On();
    }

    void OnMouseDown()
    {
        if( HS == HoleState.Idle || HS == HoleState.Close)
            Catch_On();
    }
}
