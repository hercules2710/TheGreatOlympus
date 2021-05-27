using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerShootOnline : MonoBehaviour
{
    public PhotonView photonView;
    public Camera playerCam;
    public GameObject bulletSpawnPoint;
    public GameObject bulletPrefab;
    public GameObject playerCanvas;
    public Text bulletTxt;
   // public Rigidbody playerRb;
    public ParticleSystem shotFlash;
    public float recoilForce;
    public float reloadTime;
    public float shootForce;
    public float spreadBullet;
    public float shotDelayTime;
    public float upForce;
    public bool isHoldShootinggun;
    public int bulletPertap;
    public int mag;
    public int bullet;
    private int bulletShot;
    bool readyToshoot;
    bool isReloading;
    bool isShooting;
    bool isReset;
    // Start is called before the first frame update
    void Awake()
    {
        if (photonView.isMine)
        {
            bullet = mag;
            isReloading = false;
            readyToshoot = true;
            isReset = true;
            playerCanvas.SetActive(true);
            updateBulletTxt();
        }      
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.isMine)
        {
            inputShoot();
        }
        
    }
    void inputShoot()
    {
        if (isHoldShootinggun)
        {
            isShooting = Input.GetMouseButton(0);
        }
        else
        {
            isShooting = Input.GetMouseButtonDown(0);
        }
        if (!isReloading && bullet > 0 && isShooting && readyToshoot)
        {
            bulletShot = 0;
            shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && bullet < mag)
        {
            reload();
        }
        if (bullet < 1 && !isReloading && isShooting && readyToshoot)
        {
            reload();
        }
    }
    void shoot()
    {
        readyToshoot = false;
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;
        if(Physics.Raycast(ray,out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }
        Vector3 directionShoot = targetPoint - bulletSpawnPoint.transform.position;

        float x = Random.Range(-spreadBullet, spreadBullet);
        float y = Random.Range(-spreadBullet, spreadBullet);

        Vector3 directionSpread = directionShoot + new Vector3(x, y, 0);
        GameObject currentbullet = PhotonNetwork.Instantiate(bulletPrefab.name, bulletSpawnPoint.transform.position, Quaternion.identity,0);
        currentbullet.transform.forward = directionSpread.normalized;
        currentbullet.GetComponent<Rigidbody>().AddForce(directionSpread.normalized * shootForce,ForceMode.Impulse);
        //currentbullet.GetComponent<Rigidbody>().AddForce(playerCam.transform.up * upForce, ForceMode.Impulse);
        bullet--;
        bulletShot++;
        updateBulletTxt();
        if (isReset)
        {
            Invoke("resetShot", shotDelayTime);
           // playerRb.AddForce(directionSpread.normalized * recoilForce,ForceMode.Impulse);
            shotFlash.Play();
            isReset = false;
        }
        if(bulletShot < bulletPertap && bullet > 0)
        {
            Invoke("shoot", shotDelayTime);
        }
    }
    void resetShot()
    {
        isReset = true;
        readyToshoot = true;
    }
    void reload()
    {
        isReloading = true;
        Invoke("reloadFinished", reloadTime);
    }
    void reloadFinished()
    {
        bullet = mag;
        isReloading = false;
    }
    void updateBulletTxt()
    {
        bulletTxt.text = bullet / bulletPertap + " / " + mag / bulletPertap;
    }
}
