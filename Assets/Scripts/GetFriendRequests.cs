using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.buddy;
using UnityEngine;
using System;
using System.Collections.Generic;

public class GetFriendRequests : MonoBehaviour
{
    public GameObject myGO;
    public GameObject deleteChildsOf;
    public static IList<Buddy> buddy;

    private void Awake()
    {
        getRequests();
    }
    void Start()
    {     
        Invoke("myFunction", 1);
    }
    public void getRequests()
    {
        DeleteChilds();
        App42API.Initialize("47675efb1d62deab9e46b9d43795e45c381c341bec9033abb9622fa1a88e5720", "256e86ad5fe1038aaf20334d51c894f1dfa9839996dbaa2e92376141761a07f0");
        BuddyService buddyService = App42API.BuildBuddyService();
        UnityCallBack _callBack = new UnityCallBack();
        buddyService.GetFriendRequest(TestUser._testUser.UserName, _callBack);
    }
    public void myFunction()
    {
        getRequests();
        Invoke("CreateRequestPanels", 1);
    }
    void CreateRequestPanels()
    {
        if (buddy.Count > 0)
        {
            for (int i = 0; i < buddy.Count; i++)
            {
                GameObject newPanel = Instantiate(myGO) as GameObject;
                newPanel.name = buddy[i].GetBuddyName();
                newPanel.transform.SetParent(deleteChildsOf.transform, false);
                PanelScripts panelScript = newPanel.GetComponent<PanelScripts>();
                panelScript.buddyName.text = buddy[i].GetBuddyName();
            }
        }
        else
        {
            Debug.Log("No Friend Request :(");
        }
        
    }

    void DeleteChilds()
    {
        foreach (RectTransform item in deleteChildsOf.GetComponentsInChildren<RectTransform>())
        {
            if (item.CompareTag("requestTag"))
                Destroy(item.gameObject);
        }
    }

    public class UnityCallBack : App42CallBack
    {

        public void OnSuccess(object response)
        {
            buddy = (List<Buddy>)response;
        }

        public void OnException(Exception e)
        {
            if (e.Message.Contains("4602"))
            {
                Debug.Log("no friend request");

            }
            Debug.Log("Exception : " + e);
        }

    }

}
