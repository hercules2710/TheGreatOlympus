using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour
{
    public GameObject wave1;
    public GameObject wave2;
    public GameObject wave3;
    public GameObject Wave;
    public GameObject ArrowGuid;
    public GameObject Gate;
    public Text txtWave;
    public Text txtTime;
    float timeBreak = 10f;
    float realTime;
    float timeForWave1 = 20f;
    float timeForWave2 = 60f;
    float timeForWave3 = 80f;
    int waveNum;
   // GameObject checkEnemy;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Level",1);
        PlayerPrefs.SetInt("IsTwoGun", 1);
        // checkEnemy = GameObject.FindGameObjectWithTag("Spider");       
        realTime = Time.time;
        waveNum = 1;
        timeForWave1 += realTime;
        timeForWave2 += realTime;
        timeForWave3 += realTime;
        //Gate = GameObject.FindGameObjectWithTag("ExitGate");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > realTime+timeBreak)
        {
            if(waveNum == 1)
            {
                StartWave1();
            }
            if (waveNum == 2)
            {
                StartWave2();
            }
            if (waveNum == 3)
            {
                StartWave3();
            }
        }
        txtTime.text = Time.time.ToString("0");
    }
    void StartWave1()
    {
        if(Time.time < timeForWave1 + timeBreak)
        {
            wave1.SetActive(true);
            txtWave.text = "WAVE " + 1;
            Wave.SetActive(true);
        }
        else
        {
            Wave.SetActive(false);
            wave1.SetActive(false);
            realTime = Time.time;
            waveNum = 2;
        }
    }
    void StartWave2()
    {
        if (Time.time > realTime +timeBreak && Time.time < timeForWave2 + timeBreak)
        {
            wave1.SetActive(true);
            wave2.SetActive(true);
            txtWave.text = "WAVE " + 2;
            Wave.SetActive(true);
        }
        else if(Time.time > timeForWave2 + timeBreak)
        {
            Wave.SetActive(false);
            wave1.SetActive(false);
            wave2.SetActive(false);
            realTime = Time.time;
            waveNum = 3;
        }
    }
    void StartWave3()
    {
        if (Time.time > realTime + timeBreak && Time.time < timeForWave3 + timeBreak)
        {
            wave1.SetActive(true);
            wave3.SetActive(true);
            txtWave.text = "WAVE " + 3;
            Wave.SetActive(true);
        }
        else if(Time.time > timeForWave3 + timeBreak)
        {
            Wave.SetActive(false);
            wave1.SetActive(false);
            wave3.SetActive(false);
            realTime = Time.time;
            waveNum = 0;
            Gate.SetActive(true);
            ArrowGuid.SetActive(true);
        }
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
