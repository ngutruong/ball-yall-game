using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPLayer : MonoBehaviour {
    [SerializeField]
    private Transform player;
    // Use this for initialization
    void Start () {
        if (player != null)
            transform.position = player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player != null)
            transform.position = player.transform.position;
        
        
    }
}
