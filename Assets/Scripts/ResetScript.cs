using System.Collections.Generic;
using UnityEngine;
using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using UnityEngine.UI;

public class ResetScript : MonoBehaviour {

    public InputField userNameText;
    public Text userCheckText;

   public void testFunction()
    {

        string _userName = userNameText.text;

        Dictionary<string, string> otherMetaHeaders = new Dictionary<string, string>();
        otherMetaHeaders.Add("emailVerification", "true");

        App42API.Initialize("47675efb1d62deab9e46b9d43795e45c381c341bec9033abb9622fa1a88e5720", "256e86ad5fe1038aaf20334d51c894f1dfa9839996dbaa2e92376141761a07f0");

        UserService userService = App42API.BuildUserService();
        userService.SetOtherMetaHeaders(otherMetaHeaders);
        userService.ResetUserPassword(_userName, new UnityCallBack());

    }

    public class UnityCallBack : App42CallBack
    {
        public GameObject myGO;

        public void OnSuccess(object response)
        {
            myGO = GameObject.FindGameObjectWithTag("resetPassTag");
            ResetScript rs = myGO.GetComponent<ResetScript>();
            rs.userCheckText.text = "Password reset information has been sent to -"+rs.userNameText.text+"-'s email address";

            App42Response app42response = (App42Response)response;
            Debug.Log("app42Response is : " + app42response);           
        }
        public void OnException(Exception e)
        {
            if (e.Message.Contains("2000"))
            {
                myGO = GameObject.FindGameObjectWithTag("resetPassTag");
                ResetScript rs = myGO.GetComponent<ResetScript>();
                rs.userCheckText.text = "Username Doesn't Exist";
            }
            Debug.Log("Exception : " + e);
            
        }
    }
}
