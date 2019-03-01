using System.Collections.Generic;
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using System;


public class CreateUserButtons : MonoBehaviour {
    public GameObject myGO;
    public GameObject deleteChildsOf;
    public static IList<User> user;

    private void Awake()
    {
        GetAllUser();
    }
    void Start()
    {
        Invoke("myFunction", 1);
    }
    void GetAllUser()
    {
        DeleteChilds();
        App42API.Initialize("47675efb1d62deab9e46b9d43795e45c381c341bec9033abb9622fa1a88e5720", "256e86ad5fe1038aaf20334d51c894f1dfa9839996dbaa2e92376141761a07f0");
        UserService userService = App42API.BuildUserService();
        userService.GetAllUsers(new UnityCallBack());
    }
    void CreateAllUsers()
    {
        for (int i = 0; i < user.Count; i++)
        {
            if (TestUser._testUser.UserName != user[i].GetUserName())
            {
                GameObject newButton = Instantiate(myGO) as GameObject;
                newButton.name = user[i].GetUserName();
                newButton.transform.SetParent(deleteChildsOf.transform, false);
                UsersButtonScript buttonScript = newButton.GetComponent<UsersButtonScript>();
                buttonScript.nameLabel.text = user[i].GetUserName();
            }

        }
    }
    void DeleteChilds()
    {
        foreach (RectTransform item in deleteChildsOf.GetComponentsInChildren<RectTransform>())
        {
            if (item.CompareTag("prefabTag"))
                Destroy(item.gameObject);
        }
    }
    public void myFunction()
    {
        GetAllUser();
        Invoke("CreateAllUsers", 1);
    }


    public class UnityCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            user = (IList<User>)response;
        }
        public void OnException(Exception e)
        {
            Debug.Log("Exception : " + e);
        }
    }
  
}
