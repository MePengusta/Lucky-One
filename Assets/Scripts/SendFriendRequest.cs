using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.buddy;
using UnityEngine;
using System;

public class SendFriendRequest : MonoBehaviour {
   
    public GameObject sendRequest;
    string buddyName;

    string message = "Accept Pl0x";

    public void callRequests()
    {
        UsersButtonScript buttonScript = sendRequest.GetComponent<UsersButtonScript>();
        buddyName = buttonScript.nameLabel.text;

        App42API.Initialize("47675efb1d62deab9e46b9d43795e45c381c341bec9033abb9622fa1a88e5720", "256e86ad5fe1038aaf20334d51c894f1dfa9839996dbaa2e92376141761a07f0");

        BuddyService buddyService = App42API.BuildBuddyService();
        buddyService.SendFriendRequest(TestUser._testUser.UserName, buddyName, message, new UnityCallBack());
    }
    
    public class UnityCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            Buddy buddy = (Buddy)response;
        }  
        public void OnException(Exception ex)
        {
            Debug.Log("Exception is : " + ex);
        }
    }

}
