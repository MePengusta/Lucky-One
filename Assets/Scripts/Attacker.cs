using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Attacker : MonoBehaviour {

    Animator anim;
    [Range (-1f,1.5f)] public float currentSpeed;
	private GameObject currentTarget;
    public float attackChance;
    public float randomNumber;
    private AudioSource audioSource;
    public AudioClip hit;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        Rigidbody2D myRigidBody = gameObject.AddComponent<Rigidbody2D>();
        myRigidBody.isKinematic = true;
        myRigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        myRigidBody.gravityScale = 0;
        myRigidBody.freezeRotation = true;

        InvokeRepeating("createRandomNumber", 0, 1.1f);
    }
   
    public float createRandomNumber()
    {
        randomNumber = Random.Range(0, 100);
        return randomNumber;
    }

	void Update () {
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
        if(!currentTarget){
            anim.SetBool("isAttacking", false);         
        }
	}


    public void StrikeTarget(float damage){
		if (currentTarget) {
            audioSource.PlayOneShot(hit);
                PhotonView photonView = PhotonView.Get(this);
            if (randomNumber <= attackChance && PhotonNetwork.player.IsMasterClient)
            {

                //health.dealDamage (damage);
                photonView.RPC("damageRPC", PhotonTargets.All, damage);

            }  
		}
	}

    [PunRPC]
    public void damageRPC(float damage)
    {

        if (currentTarget.GetComponent<Attacker>())
        {
            Health health = currentTarget.GetComponent<Health>();
            health.dealDamage(damage);
        }

        else if(currentTarget.GetComponent<Tower>()){
            towerHealth health = currentTarget.GetComponent<towerHealth>();
            health.dealDamage(damage);
        }
    }

	public void Attack(GameObject obj){
		currentTarget = obj;
	}

    public void setSpeed()
    {
        currentSpeed = 0.3f;

    }

    public void stopMoving(float speed)
    {
        currentSpeed = speed;
    }
}
