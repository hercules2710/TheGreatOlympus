using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheCityControll : MonoBehaviour
{
    #region singleton
    public static TheCityControll instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;
    public GameObject lastEnemy;
    public AudioSource citySound;
    public GameObject Trap;
    public GameObject[] standByEnemy;
    public GameObject trapWarning;
    public int enemyNum = 0;
    public bool finalShow;
    public bool isStandBy;
    bool activeOne = true;
    void Start()
    {
        finalShow = false;
        isStandBy = true;
        Time.timeScale = 1;
        citySound = GetComponent<AudioSource>();
        standByEnemy = GameObject.FindGameObjectsWithTag("standByEnemy");
        PlayerPrefs.SetInt("Level", 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(lastEnemy != null)
        {
            if (activeOne)
            {
                if (Vector3.Distance(player.transform.position, lastEnemy.transform.position) < 50f)
                {
                    isStandBy = false;
                    enemyNum = 0;
                    finalShow = true;
                    activeOne = false;
                }
            }
        }
        else
        {
            passTheChallenge();
        }
        if (standByEnemy.Length > 0)
        {
            if (isStandBy)
            {
                if (enemyNum < standByEnemy.Length)
                {
                    standByEnemy[enemyNum].GetComponent<RobotAutoController>().lookZone = 0;
                    standByEnemy[enemyNum].GetComponent<Target>().isImortal = true;
                    enemyNum++;
                }
            }
            if (finalShow)
            {
                isDanger();
            }
        }
    }
    public void isDanger()
    {
        if (enemyNum < standByEnemy.Length)
        {
            standByEnemy[enemyNum].GetComponent<RobotAutoController>().lookZone = 60f;
            standByEnemy[enemyNum].GetComponent<Target>().isImortal = false;
            enemyNum++;
        }
        else
        {
            finalShow = false;
        }
    }
    public void isTrapped()
    {
        trapWarning.SetActive(true);
    }
    public void OpenGate()
    {
        citySound.Play();
    }
    public void passTheChallenge()
    {
        Destroy(Trap,2);
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
