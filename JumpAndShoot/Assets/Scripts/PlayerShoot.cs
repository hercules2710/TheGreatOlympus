using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerShoot : MonoBehaviour
{
    #region Singleton
    public static PlayerShoot Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion

    public Camera fpsCam;
    public ParticleSystem FlashLight;
    public ParticleSystem hitEffect;
    public AudioSource shotSound;
    public GameObject BloodEffect;
    public GameObject BoomEffect;
    public GameObject gameOverPanel;
    public GameObject mag;
    public Text txtScore;
    public Text bulletTxt;
    [Header("Health bar")]
    public HealthBar H_bar;
    public int bulletAK;
    public int bulletBigGun;
    public int reloadScene;
    public int kill;
    public int maxBulletAK = 200;
    public int maxBulletBigGun = 60;
    public int AkDanmge = 15;
    public int BigGunDanmge = 60;
    int Damnge;
    float Range = 50f;
    float hearth = 100f;   
    float timeShootingDelay = 0.3f;
    float RealTime;
    Animator GunAnim;
    GameObject Blood;
    bool isGameOver = false;
    bool isReload = false;
    //public GameObject nextLevelPanel;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Level")==1)
        {
            kill = 0;
        }
        else
        {
            kill = PlayerPrefs.GetInt("Kill");
        }
        txtScore.text = kill.ToString();
        bulletBigGun = maxBulletBigGun;
        bulletAK = maxBulletAK;
        shotSound = GetComponent<AudioSource>();
        GunAnim = GetComponent<Animator>();
        gameOverPanel.SetActive(false);
        updateBullet();
        H_bar.setMaxHealth(hearth);
        H_bar.setHealth(hearth);
        //nextLevelPanel.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isGameOver == false)
        {
            if(isReload == false)
            {
                if (Input.GetMouseButton(0))
                {
                    shoot();
                }
            }
        }      
        if (Time.time > RealTime + timeShootingDelay)
        {
            GunAnim.SetBool("isShooting", false);
            //shotSound.Stop();
        }
        if(GunAnim == null)
        {
            GunAnim = GetComponent<Animator>();
        }
        //if(kill >= 5)
        //{
        //    survival();
        //}

    }
    void shoot()
    {
        RealTime = Time.time;
        //shotSound.Play();
        GunAnim.SetBool("isShooting", true);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward,out hit,Range))
        {
            Target target = hit.transform.GetComponent<Target>();
            ObjectTakeDamnge objectTakeDamnge = hit.transform.GetComponent<ObjectTakeDamnge>();
            ObjectTaget objectTaget = hit.transform.GetComponent<ObjectTaget>();
            BossShield bossShield = hit.transform.GetComponent<BossShield>();
            if(target != null)
            {
                target.TakeDamnge(Damnge);               
            }
            if(objectTakeDamnge != null)
            {
                objectTakeDamnge.TakeDamnge(Damnge);
            }
            if (objectTaget != null)
            {
                objectTaget.TakeDamnge(Damnge);
            }
            if(bossShield != null)
            {
                bossShield.TakeDamnge(Damnge);
            }
            if (isMainGun())
            {
                Blood = Instantiate(BloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Damnge = AkDanmge;
            }
            else
            {
                Blood = Instantiate(BoomEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Damnge = BigGunDanmge;
            }
            Destroy(Blood, 2);
            
        }
        if (isMainGun())
        {
            if (bulletAK <1)
            {
                Reloading();
            }
            else
            {
                bulletAK--;
            }
        }
        else
        {
            if (bulletBigGun < 1)
            {
                Reloading();
            }
            else
            {
                bulletBigGun--;
            }
        }
        updateBullet();
    }
    public void updateBullet()
    {
        if(isMainGun())
        {
            bulletTxt.text = bulletAK.ToString();
        }
        else
        {
            bulletTxt.text = bulletBigGun.ToString();  
        }
        
    }
    public void ShotEffect()
    {
        FlashLight.Play();
        shotSound.Play();
    }
    public void PlayAgain()
    {

        SceneManager.LoadScene(reloadScene);
        Time.timeScale = 1;
    }
    public void GetPoint()
    {
        kill++;
        PlayerPrefs.SetInt("Kill", kill);
        txtScore.text = kill.ToString();
    }
    public void PlayerTakeDamnge(float damnge)
    {
        hitEffect.Play();
        hearth -= damnge;
        //playerHealthBar.GetComponent<HealthBar>().setHealth(hearth);
        H_bar.setHealth(hearth);
        if (hearth <= 0)
        {
            gameOverPanel.SetActive(true);
            isGameOver = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            //hearth = 100f;
        }
    }
    public void Reloading()
    {
        isReload = true;
        GunAnim.SetTrigger("isReloading");
        mag.SetActive(true);
        bulletTxt.text = "Loading";
    }
    public void Reloaded()
    {
        isReload = false;
        mag.SetActive(false);
        if(isMainGun())
        {
            bulletAK = maxBulletAK;
        }
        else
        {
            bulletBigGun = maxBulletBigGun;
        }
        GunAnim.ResetTrigger("isReloading");
        updateBullet();
    }
    bool isMainGun()
    {
        if(PlayerMoveFPS.Instance.changeGun % 2 ==0)
        {
            return true;
        }
        return false;
    }
    public bool isFullMag()
    {
        if (isMainGun())
        {
            if (bulletAK == maxBulletAK)
            {
                return true;
            }
        }
        else
        {
            if (bulletBigGun == maxBulletBigGun)
            {
                return true;
            }
           
        }
        return false;
    }
}
