using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllCamera : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    // Use this for initialization
    void Start () {
        transform.position = player.transform.position + offset;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            transform.LookAt(player.transform);
            transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 10f);

        }
    }
}
