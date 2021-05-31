using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onlineRoomController : MonoBehaviour
{
    public PhotonView photonView;
    public GameObject playerPrefab;
    public GameObject startPanel;
    public GameObject leaveRoomPanel;
    public GameObject zoneCam;
    public GameObject feedMessage;
    public GameObject messageGrid;
    public GameObject[] playerPointArray;
    public Text pingTxt;
    bool isInputEsc = false;
    bool isStarting;
    // Start is called before the first frame update
    void Start()
    {
        isStarting = true;
        isInputEsc = false;
        playerPointArray = GameObject.FindGameObjectsWithTag("playerSpawnPoint");
        if (photonView.isMine)
        {
            activateMouse();
        }
    }

    // Update is called once per frame
    void Update()
    {
        pingTxt.text =  "Ping: " + PhotonNetwork.GetPing();
        if(!isInputEsc && Input.GetKeyUp(KeyCode.Escape))
        {
            isInputEsc = true;
            leaveRoomPanel.SetActive(true);
            activateMouse();
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            isInputEsc = false;
            leaveRoomPanel.SetActive(false);
            DeactivateMouse();
        }
    }
    public void spawnPlayer()
    {       
        zoneCam.SetActive(false);
        startPanel.SetActive(false);
        int startPoint = Random.Range(0, playerPointArray.Length);
        PhotonNetwork.Instantiate(playerPrefab.name, playerPointArray[startPoint].transform.position, Quaternion.identity, 0);
    }
    public void leaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MenuMain");
    }
    private void activateMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }
    private void DeactivateMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnPhotonPlayerConnected(PhotonPlayer photonPlayer)
    {
        GameObject obj = Instantiate(feedMessage, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(messageGrid.transform, false);
        obj.GetComponent<Text>().text = photonPlayer.name + " has join the room";
        obj.GetComponent<Text>().color = Color.blue;
    }
    private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer)
    {
        GameObject obj = Instantiate(feedMessage, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(messageGrid.transform, false);
        obj.GetComponent<Text>().text = photonPlayer.name + " has left the room";
        obj.GetComponent<Text>().color = Color.red;
    }
}
