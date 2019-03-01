using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp.game;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class registerScript : MonoBehaviour {

    public InputField uName, eMail, password, passConfirm;
    public Text uNameCheck, eMailCheck, passwordCheck, passConfirmCheck;

    public GameObject disabledPanel;
    public GameObject activatedPanel;

    double gameScore = 100;
    String gameName = "LuckyOne";

    string userUsername, userEmail, userPass, userPassConfirm;

    public bool ValidateEmail(string emailAddress)
    {
        string regexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
        Match matches = Regex.Match(emailAddress, regexPattern);
        return matches.Success;
    }

    public void registerPlayer()
    {
        
        userUsername = uName.text;
        userEmail = eMail.text;
        userPass = password.text;
        userPassConfirm = passConfirm.text;

        //clearAll();

        if (userUsername != "" && userUsername.Length < 10)
        {
            if (userEmail != "")
            {
                if (userPass != "" && userPassConfirm != "")
                {
                    if (userPass == userPassConfirm)
                    {
                        if (ValidateEmail(userEmail) == true)
                        {
                            App42API.Initialize("47675efb1d62deab9e46b9d43795e45c381c341bec9033abb9622fa1a88e5720", "256e86ad5fe1038aaf20334d51c894f1dfa9839996dbaa2e92376141761a07f0");
                            UserService userService = App42API.BuildUserService();
                            
                            ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();
                            scoreBoardService.SaveUserScore(gameName, userUsername, gameScore, new UnityCallBack());
                            userService.CreateUser(userUsername, userPass, userEmail, new UnityCallBack());
                            passwordCheck.text = "";                                                  
                        }

                        else
                        {
                            clearLabels();
                            eMailCheck.text = "Invalid Email";
                        }

                    }
                    else
                    {
                        clearLabels();
                        passwordCheck.text = "Password do not match";
                    }
                }
                else
                {
                    clearLabels();
                    passwordCheck.text = "Enter password";                   
                }
            }
            else
            {
                clearLabels();
                eMailCheck.text = "Enter your email";               
            }

        }
        else
        {
            clearLabels();
            uNameCheck.text = "Enter username";
        }
        

    }

    void clearLabels()
    {
        uNameCheck.text = "";
        eMailCheck.text = "";
        passwordCheck.text = "";
        passConfirmCheck.text = "";
    }

    void clearAll()
    {
        uName.text = "";
        eMail.text = "";
        password.text = "";
        passConfirm.text = "";

    }

    
    public class UnityCallBack : App42CallBack

    {
       
        public void OnSuccess(object response)
        {
            GameObject registerBtn = GameObject.Find("registerButton");
            registerScript rs = registerBtn.GetComponent<registerScript>();
            
                  
            User user = (User)response;
            Debug.Log("userName is " + user.GetUserName());
            Debug.Log("emailId is " + user.GetEmail());
           
           

            rs.disabledPanel.SetActive(false);
            rs.activatedPanel.SetActive(true);

        }


        public void OnException(Exception e)
        {
            GameObject registerBtn = GameObject.Find("registerButton");
            registerScript rs = registerBtn.GetComponent<registerScript>();
            Debug.Log("Exception : " + e);

            if (e.Message.Contains("2001"))
            {               
                rs.clearLabels();
                rs.uNameCheck.text = "Username Already Exists";
               
            }
            if (e.Message.Contains("2005"))
            {
                rs.clearLabels();
                rs.eMailCheck.text = "Email Already Exists";
            }
            
        }
    }

}
