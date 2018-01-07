using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtPlayer : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<PlayerControl>().isSpikeCollected == false)
            {
                collision.gameObject.GetComponent<Combat>().TakeDamage(10);
            }
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.Reflect(Vector3.back,
                                      collision.contacts[0].normal) * 10,
                       ForceMode.Impulse);
        }
        
    }
    
}
