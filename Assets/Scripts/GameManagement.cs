using UnityEngine;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using System;

public class GameManagement : Photon.PunBehaviour{

    public GameObject endPanel;

    GameObject t1,t2;

    public Text player1text,player2text,resultText;

    public Text p1, p2;

    string gameName = "LuckyOne";

    static double userScore;

    static string scoreID;

    double win, lose;

    string gameWinner,gameLoser;

    public Button pauseBut;



    private void Start()
    {
        GameMusicPlayer.killMusic();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        win = 25;
        lose = -25;
        getScore();

        if (PhotonNetwork.player.IsMasterClient)
        {
            t1 = PhotonNetwork.Instantiate("Tower1", new Vector2(-4.16f, 0.3f), Quaternion.identity, 0);
            t2 = PhotonNetwork.Instantiate("Tower2", new Vector2(4.16f, 0.3f), Quaternion.identity, 0);

            t1.name = "Tower1";
            t2.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.otherPlayers[0]);
            t2.name = "Tower2";
        }

        player1text.text = PhotonNetwork.playerList[1].NickName; //hamit,bug
        player2text.text = PhotonNetwork.playerList[0].NickName;

        p1.text = PhotonNetwork.masterClient.NickName;

        if (PhotonNetwork.player.IsMasterClient)
        {
            p2.text = PhotonNetwork.otherPlayers[0].NickName;
        }
        else
        {
            p2.text = PhotonNetwork.player.NickName;
        }
        
    }
    private void Update()
    {
        if (towerHealth.loser == null)
        {
            return;
        }
        else
        {
            endTheGame();
        }
        
    }
    
    public override void OnLeftRoom(){
        if (PhotonNetwork.player.IsMasterClient)
        {
            PhotonNetwork.DestroyAll();
        }
    }

    public void endTheGame()
    {
        clearNumofSpawns();
        gameLoser = towerHealth.loser;
        p1.text = "";
        p2.text = "";
        

        foreach (PhotonPlayer pl in PhotonNetwork.playerList)
        {
            if (pl.NickName.Equals(gameLoser))
            {
                gameWinner = pl.NickName;
            }
            else
            {
                gameWinner = pl.NickName;
            }
        }
        if (PhotonNetwork.player.IsMasterClient)
        {
            PhotonNetwork.DestroyAll();
        }
       
        endPanel.SetActive(true);

        if (gameLoser.Equals(TestUser._testUser.UserName))
        {
            resultText.text = "You Lose !";
            userScore = userScore + lose;                          
        }
        else if(gameWinner.Equals(TestUser._testUser.UserName))
        {
            resultText.text = "You win !";
            userScore = userScore + win;                        
        }
        
        setScore(userScore);
        towerHealth.loser = null;
        gameWinner = null;
    }
    public void endTheGame(PhotonPlayer leaver) //opponent leaves
    {
        clearNumofSpawns();
        gameLoser = leaver.NickName;
        gameWinner = PhotonNetwork.playerName;
        userScore = userScore + win;
 
        setScore(userScore);
        p1.text = "";
        p2.text = "";

        if (PhotonNetwork.player.IsMasterClient)
        {
            PhotonNetwork.DestroyAll();
        }

        resultText.text = "Your oponent has left the game!";
        endPanel.SetActive(true);

        gameWinner = null;
        gameLoser = null;

    }
    void clearNumofSpawns()
    {
        warriorSpawner.instantiateCountForMaster = 0;
        warriorSpawner.instantiateCountForGuest = 0;
        warriorSpawner.spawnCountForMaster = 0;
        warriorSpawner.spawnCountForGuest = 0;
    }

	public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
	{        
        endTheGame(otherPlayer);
	}
    
    public void Disconnect()
    {
        PhotonNetwork.Disconnect();   
    }
    public void getScore()
    {
        App42API.Initialize("47675efb1d62deab9e46b9d43795e45c381c341bec9033abb9622fa1a88e5720", "256e86ad5fe1038aaf20334d51c894f1dfa9839996dbaa2e92376141761a07f0");
        ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.GetScoresByUser(gameName, TestUser._testUser.UserName, new UnityCallBack());

    }
    public void setScore(double userScore)
    {
        App42API.Initialize("47675efb1d62deab9e46b9d43795e45c381c341bec9033abb9622fa1a88e5720", "256e86ad5fe1038aaf20334d51c894f1dfa9839996dbaa2e92376141761a07f0");
        ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.EditScoreValueById(scoreID, userScore, new UnityCallBack());
    }
    public class UnityCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            Game game = (Game)response;
            for (int i = 0; i < game.GetScoreList().Count; i++)
            {
                GameManagement.userScore = game.GetScoreList()[i].GetValue();
                GameManagement.scoreID =   game.GetScoreList()[i].GetScoreId();
            }
        }

        public void OnException(Exception ex)
        {
            throw new NotImplementedException();
        }
    }

}
