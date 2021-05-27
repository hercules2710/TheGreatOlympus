using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMoveOnline : MonoBehaviour
{
    #region singleton
    public static playerMoveOnline Instance;
   
    #endregion
    public PhotonView photonView;
    public CharacterController characterController;
    public Animator playerAnimator;
    public Transform groundCheck;
    public LayerMask groundMask;
    public GameObject AKGun;
    public GameObject canvasName;
    public GameObject BigGun;
    public GameObject GunPanel;
    public GameObject AkGunIcon;
    public GameObject BigGunIcon;
    public GameObject mainCam;
    public Text playerNameTxt;
    public float speed = 5f;
    public float gForce = -15f;
    public int changeGun = 0;
    public int nextScene;
    float groundDistance = 0.5f;
    float jumpStrength = 3f;
    float realTime;
    bool isGrounded;
    bool hitAble;
    Vector3 velocicy;

    void Awake()
    {
        Instance = this;
        if (photonView.isMine)
        {
            playerNameTxt.text = PhotonNetwork.playerName;
            mainCam.SetActive(true);
        }
        else
        {
            playerNameTxt.text = photonView.owner.name;
            playerNameTxt.color = Color.red;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        realTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            move();
           // photonView.RPC("move", PhotonTargets.AllBuffered);
            animateControll();
        }
        else
        {
           // photonView.RPC("lookAtPlayerCam", PhotonTargets.AllBuffered);
        }
    }
   // [PunRPC]
    void lookAtPlayerCam()
    {
        canvasName.transform.LookAt(mainCam.transform.position);
    }
  //  [PunRPC]
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
      
        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterController.Move(move * speed * 1.5f * Time.deltaTime);
        }
        characterController.Move(move * speed * Time.deltaTime);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocicy.y = Mathf.Sqrt(jumpStrength * -2f * gForce);
        }
        if (Time.time > realTime + 0.2f)
        {
            hitAble = true;
        }

        velocicy.y += gForce * Time.deltaTime;
        characterController.Move(velocicy * Time.deltaTime);
    }
    void animateControll()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
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
            playerAnimator.SetBool("isRunning", true);
            playerAnimator.SetBool("isWalking", false);
        }
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.LeftShift) == false)
        {
            playerAnimator.SetBool("isRunning", false);
        }
    }
}
