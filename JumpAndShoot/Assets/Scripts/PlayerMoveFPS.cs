using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMoveFPS : MonoBehaviour
{
    #region singleton
    public static PlayerMoveFPS Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion

    public CharacterController characterController;
    public Animator playerAnimator;
    public Transform groundCheck;
    public LayerMask groundMask;
   // public GameObject AKGun;
   // public GameObject BigGun;
   // public GameObject GunPanel;
   // public GameObject AkGunIcon;
   // public GameObject BigGunIcon;
   // public Text gunPanelTxt;
    public float speed = 5f;
    public float gForce = -15f;
    public int changeGun = 0;
    public int nextScene;
    public bool action;
    float groundDistance = 0.5f;
    float jumpStrength = 3f;
    float realTime;
    bool isGrounded;
    bool isTwoGun = false;
    bool hitAble;
    Vector3 velocicy;    
   
    // Start is called before the first frame update
    void Start()
    {
        action = true;
       /* if(PlayerPrefs.GetInt("IsTwoGun")>1)
        {
            setGun();
        }*/
        realTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(action)
        {
            move();
        }      
    }
    void move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocicy.y < 0)
        {
            velocicy.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        if (x != 0 || z != 0)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterController.Move(move * speed * 1.5f * Time.deltaTime);
            playerAnimator.SetBool("isRunning", true);
            playerAnimator.SetBool("isWalking", false);
        }
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.LeftShift) == false)
        {
            playerAnimator.SetBool("isRunning", false);
        }

        characterController.Move(move * speed * Time.deltaTime);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocicy.y = Mathf.Sqrt(jumpStrength * -2f * gForce);
        }
       /* if (isTwoGun && Input.GetKeyDown(KeyCode.Q))
        {
            changeGun++;
            changingGun();
            PlayerShoot.Instance.updateBullet();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (PlayerShoot.Instance.isFullMag() == false)
            {
                PlayerShoot.Instance.Reloading();
            }
        }*/
        if (Time.time > realTime + 0.2f)
        {
            hitAble = true;
        }
       
        velocicy.y += gForce * Time.deltaTime;
        characterController.Move(velocicy * Time.deltaTime);
    }
   
   /* void setGun()
    {
        isTwoGun = true;
        GunPanel.SetActive(true);
    }
    void changingGun()
    {
        if (changeGun % 2 == 0)
        {
            AKGun.SetActive(true);
            BigGunIcon.SetActive(false);
            AkGunIcon.SetActive(true);
            gunPanelTxt.text = "AK SciFi";
            BigGun.SetActive(false);
        }
        else
        {
            AKGun.SetActive(false);
            AkGunIcon.SetActive(false);
            BigGunIcon.SetActive(true);
            gunPanelTxt.text = "HellWailer";
            BigGun.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag.Equals("ExitGate"))
        {
            SceneManager.LoadScene(nextScene);
        }
        if(other.transform.tag.Equals("BigGunItem"))
        {
            setGun();
            PlayerPrefs.SetInt("IsTwoGun", 2);
            Destroy(other.gameObject);
        }
        if(hitAble)
        {
            if (other.transform.tag.Equals("hitBox"))
            {
                PlayerShoot.Instance.PlayerTakeDamnge(RobotAutoController.Instance.Danmge);
                hitAble = false;
                realTime = Time.time;
            }
            if (other.transform.tag.Equals("hitBoxBigEnemy"))
            {
                PlayerShoot.Instance.PlayerTakeDamnge(BigRobotController.Instance.Danmge);
                hitAble = false;
                realTime = Time.time;
            }
            if(other.transform.tag.Equals("spiderBossHitBox"))
            {
                PlayerShoot.Instance.PlayerTakeDamnge(SpiderBoss.Instance.Danmge);
                hitAble = false;
                realTime = Time.time;
            }
            if(other.transform.tag.Equals("hitBoxTallSpider"))
            {
                PlayerShoot.Instance.PlayerTakeDamnge(TallSpider.Instance.Danmge);
                hitAble = false;
                realTime = Time.time;
            }
            //if (other.transform.tag.Equals("hitBox"))
            //{
            //    PlayerShoot.Instance.PlayerTakeDamnge(RobotAutoController.Instance.Danmge);
            //    hitAble = false;
            //    realTime = Time.time;
            //}
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("trapTrigger"))
        {
            TheCityControll.instance.isTrapped();
        }
    }*/
}
