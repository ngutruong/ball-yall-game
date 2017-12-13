using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {
    [SerializeField]
    private float speed = 100f;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, speed * Time.deltaTime, 0, Space.World);
    }
}
