using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    private const float Y_ANGLE_MIN = -0.1f;
    private const float Y_ANGLE_MAX = 50.0f;

    [SerializeField]
    private float speed;

    [SerializeField]
    GameObject followMe;

    [SerializeField]
    GameObject camera;

    private Rigidbody rigidbody;
    private Vector3 offset;

    private float mouseX = 0.0f;
    private float mouseY = 0.0f;

    [SerializeField]
    private GameObject spikesObject;
    [SerializeField]
    private GameObject playerObject;

    [SerializeField]
    private float spikeStartTimer = 10f;

    private float spikeCountDown;
    [SerializeField]
    private bool isSpikeCollected = false;

    [SerializeField]
    private float generalTime;

    private int points = 0;

    [SerializeField]
    private Text pointsText;
    [SerializeField]
    private Text spikeTimerText;
    [SerializeField]
    private Text timerText;

    [SerializeField]
    private GameObject gameOverMenu;

    private MeshRenderer currentMesh;

    private Combat combat = null;

    void Start()
    {
        offset = gameObject.transform.position - followMe.transform.position;
        rigidbody = GetComponent<Rigidbody>();
        currentMesh = GetComponent<MeshRenderer>();
        generalTime = Time.time;
        spikeCountDown = spikeStartTimer;
        combat = GetComponent<Combat>();
    }
    void Update()
    {
        generalTime = Time.time - generalTime;
        if(combat.health <= 0)
        {
            Destroy(gameObject);
            gameOverMenu.SetActive(true);
        }
    }
    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 dir = camera.transform.TransformDirection(movement);
        dir.Set(dir.x,0,dir.z);
        dir = dir.normalized * movement.magnitude;
        rigidbody.AddForce(dir * speed);
        followMe.transform.position = gameObject.transform.position;

        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");

        mouseY = Mathf.Clamp(mouseY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        
    }
    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -3.5f);
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        camera.transform.position = gameObject.transform.position + rotation * dir;

        camera.transform.LookAt(gameObject.transform.position);


        timerText.text = "Time: " + (int)generalTime;
        if (isSpikeCollected)
        {
            if((generalTime % 10) >= spikeStartTimer-1)
            {
                spikeTimerText.gameObject.SetActive(false);
                playerObject.SetActive(true);
                spikeTimerText.gameObject.SetActive(false);
            }
            else
            {
                spikeCountDown = (generalTime % 10);
                spikeTimerText.text = "Spikes Timer: " + (int)spikeCountDown;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        
        switch (other.tag)
        {
            case "Spikes":
                SpikeCollide(other);
                break;
            case "Health":
                HealthCollide(other);
                break;
            case "Coin":
                CoinCollide(other);
                break;
            case "DeathZone":
                DeathZoneCollide(other);
                break;
        }
        

    }
    void SpikeCollide(Collider other)
    {
        Destroy(other.gameObject);
        Debug.Log("Spike Collide");
        spikesObject.SetActive(true);
        playerObject.SetActive(false);
        spikeTimerText.gameObject.SetActive(true);
        isSpikeCollected = true;
    }
    void CoinCollide(Collider other)
    {
        Destroy(other.gameObject);
        Debug.Log("Coin Collide");
        points++;
        pointsText.text = "Points: " + points;
    }
    void HealthCollide(Collider other)
    {
        Destroy(other.gameObject);
        Debug.Log("Health Collide");
        combat.AddHealth();
    }
    void DeathZoneCollide(Collider other)
    {
        Destroy(gameObject);
        gameOverMenu.SetActive(true);
    }
}
