using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour {

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float speed = 10f;

    private bool playerEnterTrigger = false;


    void Start()
    {

    }

    // Update is called once per frame
    void Update () {
        if (playerEnterTrigger)
        {
            gameObject.transform.LookAt(target);
            gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }   
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerEnterTrigger = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerEnterTrigger = false;
        }
    }

}
