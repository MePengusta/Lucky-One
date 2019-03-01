using System;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : Photon.PunBehaviour
{

    string _gameVersion = "1";
    public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;
    public byte maxPlayersPerRoom = 2;
    public Text player1name;
    public Text player2name;
    public GameObject playerScore;
    public Text progressLabel;


    private void Awake()
    {
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.logLevel = Loglevel;
    }

    public void Connect()
    {
        PhotonNetwork.playerName = TestUser._testUser.UserName;
        var a = playerScore.GetComponent<Text>();
        PhotonNetwork.player.SetScore(Convert.ToInt32(a.text));
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("MASTER BAGLANDİ");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxPlayersPerRoom }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        if (PhotonNetwork.playerList.Length > 1)
        {   
            player1name.text = PhotonNetwork.playerName + "\n (" + PhotonNetwork.player.GetScore().ToString() + " LP)";
            player2name.text = PhotonNetwork.otherPlayers[0].NickName + "\n (" + PhotonNetwork.otherPlayers[0].GetScore().ToString() + " LP)";
            progressLabel.text = "Game is starting now";
            Invoke("loadGame", 3f);
        }
        else
        {
            player1name.text = PhotonNetwork.playerName + "\n (" + PhotonNetwork.player.GetScore().ToString() + " LP)";
        }
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        if (PhotonNetwork.playerList.Length == 2)
        {        
            player2name.text = newPlayer.NickName.ToString() + "\n (" + newPlayer.GetScore().ToString() + " LP)";
            progressLabel.text = "Game is starting now";
            Invoke("loadGame", 3f);          
        }

        else if (PhotonNetwork.playerList.Length == 1)
        {
            Debug.Log("Not Enough PLayers");
        }

    }
    public void loadGame()
    {
        PhotonNetwork.LoadLevel("Game");
    }

}
