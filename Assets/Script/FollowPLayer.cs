using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPLayer : MonoBehaviour {
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 offset;
    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offset;
        transform.LookAt(player.position);
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 10f * Time.deltaTime);
            Debug.Log("D buton was presed");
        }
            
    }
}
