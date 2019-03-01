using UnityEngine;

public class Health : MonoBehaviour {

	public float health = 1f;
	Animator anim;
    float healthBarLength;
    GUIStyle bigFont;
    

    void Start(){
		anim = GetComponent<Animator> ();
        healthBarLength = Screen.width / 6;
        bigFont = new GUIStyle();
        bigFont.fontSize = 40;
    }

    public void dealDamage(float damage) {      
        health -= damage;
		if (health <= 0) {
			playDieAnimation ();           
		}
	}

    void playDieAnimation(){
		anim.SetBool ("isDead", true);
        if(gameObject.GetComponent<PhotonView>().owner.IsMasterClient == true){
            warriorSpawner.instantiateCountForMaster--;
        }
        else{
            warriorSpawner.instantiateCountForGuest--;
        }
        Destroy(gameObject);
    }

	private void OnGUI()
	{
        Vector2 targetPosition;
        targetPosition = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Box(new Rect(targetPosition.x - 40,Screen.height - targetPosition.y - 80,80, 20), health.ToString());
	}
}
