using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    
    List<GameObject> currentCollisions = new List<GameObject>();
    int collisionNo = 0;

    private void Update()
    {
        if (currentCollisions.Count == 0)
        {
            CancelInvoke();
        }
    }
    void Start()
    {
        Rigidbody2D myRigidBody = gameObject.AddComponent<Rigidbody2D>();
        myRigidBody.isKinematic = true;
        myRigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        myRigidBody.gravityScale = 0;
        myRigidBody.freezeRotation = true;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionNo++;
        GameObject obj = collision.gameObject;
        currentCollisions.Add(obj);

        if (collisionNo == 1)
        {
            InvokeRepeating("dealDamageToEnemy", 0, 3f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentCollisions.Remove(collision.gameObject);
        collisionNo--;
    }


    void dealDamageToEnemy()
    {
        foreach (GameObject collisionObject in currentCollisions.ToArray())
        {
            Health health = collisionObject.GetComponent<Health>();
            health.dealDamage(1);
        }
    }
}

