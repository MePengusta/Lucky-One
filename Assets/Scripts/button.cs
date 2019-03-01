using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour {

    public GameObject activateTarget;
    public GameObject[] disableTargets;

    public void ActivateTarget()
    {
        
        activateTarget.SetActive(true);

        if (disableTargets.Length>0)
        {
            foreach (GameObject item in disableTargets)
            {
                item.SetActive(false);
            }
        }
        
    }

    public void goBackToMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(1);   
        GameMusicPlayer.startMusic();
    }
    


}
