using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using UnityEngine;
using System;

public class GetLeaderboard : MonoBehaviour {

    public GameObject myGO;
    public GameObject deleteChildsOf;
    public int rankUser = 1;
    public static Game game;
    string gameName = "LuckyOne";

    private void Awake()
    {
        GetLeaderboardUsers();
    }
    void Start()
    {
        Invoke("myFunction", 1);
    }

    void GetLeaderboardUsers()
    {
        DeleteChilds();
        App42API.Initialize("47675efb1d62deab9e46b9d43795e45c381c341bec9033abb9622fa1a88e5720", "256e86ad5fe1038aaf20334d51c894f1dfa9839996dbaa2e92376141761a07f0");
        ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
        scoreBoardService.GetTopRankings(gameName, new UnityCallBack());
    }
    void DeleteChilds()
    {
        foreach (RectTransform item in deleteChildsOf.GetComponentsInChildren<RectTransform>())
        {
            if (item.CompareTag("leaderboardTag"))
                Destroy(item.gameObject);
        }
        rankUser = 1;
    }

    void CreateLeaderboard()
    {

        for (int i = 0; i < game.GetScoreList().Count; i++)
        {
            GameObject newPanel = Instantiate(myGO) as GameObject;
            newPanel.name = game.GetScoreList()[i].GetUserName();
            newPanel.transform.SetParent(deleteChildsOf.transform, false);
            PanelScripts panelScript = newPanel.GetComponent<PanelScripts>();
            panelScript.buddyName.text = game.GetScoreList()[i].GetUserName();
            panelScript.userScore.text = game.GetScoreList()[i].GetValue().ToString();
            panelScript.userRank.text = rankUser.ToString();
            rankUser++;

        }

    }
    public void myFunction()
    {
        GetLeaderboardUsers();
        Invoke("CreateLeaderboard", 1);
    }
    public class UnityCallBack : App42CallBack
    {

        public void OnSuccess(object response)
        {          
           game = (Game)response;
        }
        public void OnException(Exception e)
        {
            Debug.Log("Exception : " + e);
        }
    }
}
