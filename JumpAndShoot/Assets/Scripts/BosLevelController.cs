using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BosLevelController : MonoBehaviour
{
    #region singleton
    public static BosLevelController Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion
    public GameObject spiderBossCub;
    public float elevatorSpeed;
    GameObject elevator;
    GameObject eleStopPoint;
    GameObject Player;
    AudioSource audioS;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Level", 3);
        Player = GameObject.FindGameObjectWithTag("Player");
        elevator = GameObject.FindGameObjectWithTag("elevator");
        eleStopPoint = GameObject.FindGameObjectWithTag("eleStopPoint");
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(elevator.transform.position,Player.transform.position)<9)
        {
            elevatorMove();
        }
        if(spiderBossCub != null)
        {
            if (spiderBossCub.GetComponent<Target>().health <= 0)
            {
                gameObject.GetComponent<OpenGate>().isOpenGate = true;
            }
        }      
    }
    public void elevatorMove()
    {
        elevator.transform.position = Vector3.Lerp(elevator.transform.position, eleStopPoint.transform.position, elevatorSpeed*Time.deltaTime);
    }
    public void PlayAudio()
    {
        audioS.Play();
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
