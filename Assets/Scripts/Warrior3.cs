using UnityEngine;

public class Warrior3 : MonoBehaviour {

	private Attacker attacker;
	private Animator anim;

    void Start()
    {
        attacker = GetComponent<Attacker>();
        anim = GetComponent<Animator>();

    }

	void OnTriggerEnter2D(Collider2D collider){
		GameObject obj = collider.gameObject;

        if (!obj.GetComponent<Attacker>() && !obj.GetComponent<Tower>())
        {
			return;
		}
        else {
                anim.SetBool("isAttacking", true);
                attacker.Attack(obj);
        }
	}
}
