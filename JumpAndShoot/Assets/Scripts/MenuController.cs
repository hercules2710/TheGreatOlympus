using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject multiplayerPanel;
    public GameObject userPanel;
    public GameObject startMultiBtn;
    public InputField userNameInp;
    public InputField createRoomInp;
    public InputField joinRoomInp;
    private string versionName;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(versionName);
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("connected");
    }
    public void checkInputUserName()
    {
        if(userNameInp.text.Length < 5)
        {
            startMultiBtn.SetActive(false);
        }
        else
        {
            startMultiBtn.SetActive(true);
        }
    } 
    public void setUserName()
    {
        userPanel.SetActive(false);
        multiplayerPanel.SetActive(true);
        PhotonNetwork.playerName = userNameInp.text;
        Debug.Log(PhotonNetwork.playerName);
    }
    public void multiplayerBtn()
    {
        userPanel.SetActive(true);
    }
    public void play()
    {
        PlayerPrefs.SetInt("IsTwoGun", 1);
        SceneManager.LoadScene(1);
    }
    public void continueBtn()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }
    public void exitBtn()
    {
        Application.Quit();
    }
    public void createRoomOnline()
    {
        PhotonNetwork.CreateRoom(createRoomInp.text, new RoomOptions() { maxPlayers = 5 }, null);
    }
    public void joinRoomOnline()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.maxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom(joinRoomInp.text,roomOptions , TypedLobby.Default);
    }
    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Online");
    }
}
