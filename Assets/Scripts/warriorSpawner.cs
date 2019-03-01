
using UnityEngine;

public class warriorSpawner : Photon.MonoBehaviour {

    public Camera myCamera;
    private string selectedWarrior;
    public static int spawnCountForMaster,spawnCountForGuest =0;
    private int spawnLimit = 9;
    public static int instantiateCountForMaster=0;
    public static int instantiateCountForGuest=0;
    private int instantiateLimit = 3;
    public GameObject core1;
    public GameObject core2;

    private void Start()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            core1.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player);
            core2.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.otherPlayers[0]);
            core1.SetActive(true);
            core2.SetActive(false);
        }
        else
        {
            core1.SetActive(false);
            core2.SetActive(true);
        }
    }
    private void OnMouseDown()
    {
 
            PhotonView pv = PhotonView.Get(this);
            Vector2 rawPos = clickLocation();
            Vector2 roundedPos = snapToGrid(rawPos);
            selectedWarrior = warriorSelectButton.selectedWarrior.name;

        if (PhotonNetwork.player.IsMasterClient && instantiateCountForMaster < instantiateLimit && spawnCountForMaster < spawnLimit)
            {
                PhotonNetwork.Instantiate(selectedWarrior, roundedPos, Quaternion.identity, 0).name = TestUser._testUser.UserName;
                instantiateCountForMaster++;
                spawnCountForMaster++;
            }

        else if(!PhotonNetwork.player.IsMasterClient && instantiateCountForGuest < instantiateLimit && spawnCountForGuest < spawnLimit)
            {
                PhotonNetwork.Instantiate(selectedWarrior, roundedPos, Quaternion.Euler(0, 180, 0), 0).name = TestUser._testUser.UserName;
                instantiateCountForGuest++;
                spawnCountForGuest++;
            }

        warriorSelectButton.makeAllWhite();

        Debug.Log("spawn count for master is : " + spawnCountForMaster + "spawn count for guest is " + spawnCountForGuest);
    }

    Vector2 snapToGrid(Vector2 worldPos){

        float newX = Mathf.RoundToInt(worldPos.x);
        float newY = Mathf.RoundToInt(worldPos.y);

        return new Vector2(newX,newY);
    }

    Vector2 clickLocation(){

        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        float uselessDistance = 10f;

        Vector3 triplet = new Vector3(mouseX, mouseY, uselessDistance);
        Vector2 worldPos = myCamera.ScreenToWorldPoint(triplet);

        return worldPos;
    }
    
}
