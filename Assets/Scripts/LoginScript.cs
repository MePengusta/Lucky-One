using UnityEngine;
using System;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{

    public InputField uName;
    public InputField password;

    public Text loginCheck;

    public void loginPlayer()
    {
        if (uName.text != "")
        {
            if (password.text != "")
            {
                loginCheck.text = "";
                App42API.Initialize("47675efb1d62deab9e46b9d43795e45c381c341bec9033abb9622fa1a88e5720", "256e86ad5fe1038aaf20334d51c894f1dfa9839996dbaa2e92376141761a07f0");
                UserService userService = App42API.BuildUserService();
                userService.Authenticate(uName.text, password.text, new UnityCallBack());

            }
            else
            {

                loginCheck.text = "Password is empty";
            }
        }
        else
        {
            loginCheck.text = "Username is empty";
        }
    }


}
public class UnityCallBack : App42CallBack
{

    public void OnSuccess(object response)
    {
        User user = (User)response;
        string _userName = user.GetUserName();
        Debug.Log("userName is " + _userName);
        Debug.Log("sessionId is " + user.GetSessionId());
        TestUser._testUser = new TestUser(_userName);
        SceneManager.LoadScene(1);
    }
    public void OnException(Exception e)
    {
        GameObject loginBtn = GameObject.Find("loginButton");
        LoginScript ls = loginBtn.GetComponent<LoginScript>();

        if (e.Message.Contains("2002"))
        {
            ls.loginCheck.text = "Username/Password doesn't match";
        }
        Debug.Log("Exception : " + e);
    }
}

