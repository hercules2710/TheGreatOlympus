using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AutoWaveSpawn : MonoBehaviour
{
    public enum waveState { spawning,counting,waiting,Finished}
    [System.Serializable]
    public class wave
    {
        //public int waveNum;
        public int countEnemy;
        public float enemyRate;
    }
    public wave[] waves;
    public waveState state = waveState.counting;
    public GameObject[] enemy;
    public GameObject ArrowGuid;
    public GameObject Gate;
    public GameObject waveUI;
    public Text txtWave;
    public int timeBreak;
    public int exitScene;
    GameObject[] spawnZone;
    float searchTime;
    private int nextWave = 0;
    private float waveCountdown;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("IsTwoGun", 1);
        waveCountdown = timeBreak;
        spawnZone = GameObject.FindGameObjectsWithTag("spawnZone");
    }

    // Update is called once per frame
    void Update()
    {
        if (state == waveState.waiting)
        {
            if (isEnemyKilled())
            {
                waveCompleted();
            }
            else
            {
                return;
            }
        }
        if (state == waveState.Finished)
        {
            return;
        }
        if (waveCountdown < 0)
        {
            if (state != waveState.spawning)
            {
                StartCoroutine(spawn(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }   
    }
    IEnumerator spawn(wave waveNum)
    {
        waveUI.SetActive(true);
        txtWave.text = "WAVE " + (nextWave + 1);
        state = waveState.spawning;
        for(int i = 0; i<waveNum.countEnemy;i++)
        {
            spawning();
            yield return new WaitForSeconds(waveNum.enemyRate);
        }
        state = waveState.waiting;
        waveUI.SetActive(false);
        yield break;
    }
    void spawning()
    {
        int zone = Random.Range(0, spawnZone.Length);
        int type = Random.Range(0, enemy.Length);
        if(nextWave <1)
        {
            Instantiate(enemy[0], spawnZone[zone].transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(enemy[type], spawnZone[zone].transform.position, Quaternion.identity);
        }    
    }
    bool isEnemyKilled()
    {
        searchTime -= Time.deltaTime;
        if(searchTime<= 0)
        {
            searchTime = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return true;
            }
        }      
        return false;
    }
    void waveCompleted()
    {
        if(nextWave< waves.Length-1)
        {
            waveCountdown = timeBreak;
            state = waveState.counting;
            nextWave++;
        }
        else
        {
            state = waveState.Finished;
           // Debug.Log(state);
            ArrowGuid.SetActive(true);
            Gate.SetActive(true);
        }
    }
    public void Exit()
    {
        SceneManager.LoadScene(exitScene);
    }
}
