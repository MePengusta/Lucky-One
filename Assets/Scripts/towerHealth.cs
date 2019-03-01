using UnityEngine;

public class towerHealth : Photon.MonoBehaviour {

    public float health;
    public static string loser;

    private void OnGUI()
    {
        Vector2 targetPosition;
        targetPosition = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Box(new Rect(targetPosition.x - 40, Screen.height - targetPosition.y - 80, 80, 20), health.ToString());
    }

    public void dealDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0)
        {            
            loser = gameObject.GetComponent<PhotonView>().owner.NickName;
            gameObject.SetActive(false);
        }
    }
}
