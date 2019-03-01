using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UITools : MonoBehaviour
{
    string gameName = "LuckyOne";
    public Text userText;

    private void Awake()
    {
        App42API.Initialize("47675efb1d62deab9e46b9d43795e45c381c341bec9033abb9622fa1a88e5720", "256e86ad5fe1038aaf20334d51c894f1dfa9839996dbaa2e92376141761a07f0");
        ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.GetScoresByUser(gameName, TestUser._testUser.UserName, new UnityCallBack());
        userText.text = TestUser._testUser.UserName;
    }


    public class UnityCallBack : App42CallBack
    {
        public GameObject getScore;

        public void OnSuccess(object response)
        {            
            Game game = (Game)response;
            getScore = GameObject.FindGameObjectWithTag("scoreTag");

            for (int i = 0; i < game.GetScoreList().Count; i++)
            {                          
                var a = getScore.GetComponent<Text>();
                a.text = game.GetScoreList()[i].GetValue().ToString();
            }
        }

        public void OnException(Exception ex)
        {
            throw new NotImplementedException();
    }
}
}