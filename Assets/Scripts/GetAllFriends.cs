using System.Collections.Generic;
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.buddy;
using System;

public class GetAllFriends : MonoBehaviour {

    public GameObject myGO;
    public GameObject deleteChildsOf;
    public static IList<Buddy> buddy;

    private void Awake()
    {
        GetFriends();
    }
    void Start()
    {
        Invoke("myFunction", 1);
    }   
    void GetFriends()
    {
        DeleteChilds();
        App42API.Initialize("47675efb1d62deab9e46b9d43795e45c381c341bec9033abb9622fa1a88e5720", "256e86ad5fe1038aaf20334d51c894f1dfa9839996dbaa2e92376141761a07f0");
        BuddyService buddyService = App42API.BuildBuddyService();
        buddyService.GetAllFriends(TestUser._testUser.UserName, new UnityCallBack());
    }
    void DeleteChilds()
    {
        foreach (RectTransform item in deleteChildsOf.GetComponentsInChildren<RectTransform>())
        {
            if (item.CompareTag("friendPanelTag"))
            {
                Destroy(item.gameObject);
            }          
        }
    }
    void CreateFriendList()
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
    public void myFunction()
    {
        GetFriends();
        Invoke("CreateFriendList", 1);
    }
    public class UnityCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            buddy = (List<Buddy>)response;
        }
        public void OnException(Exception e)
        {
            Debug.Log("Exception : " + e);
        }
    }
}
