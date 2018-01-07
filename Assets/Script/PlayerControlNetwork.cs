using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;


public class PlayerControlNetwork : NetworkBehaviour {
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
    private int spikeStartTimer = 10;

    [SerializeField]
    public bool isSpikeCollected = false;

    private int points = 0;

    private MeshRenderer currentMesh;

    private Combat combat = null;

    void Start()
    {
        if (isLocalPlayer)
        {
            ParseColor();
            followMe = GameObject.Find("CameraObject");
            camera = GameObject.Find("Camera");
            offset = gameObject.transform.position - followMe.transform.position;
            rigidbody = gameObject.GetComponent<Rigidbody>();
            currentMesh = gameObject.GetComponent<MeshRenderer>();
            //generalTime = (int)Time.time;
            combat = GetComponent<Combat>();
        }
    }
    void ParseColor()
    {
            string colorString = PlayerPrefs.GetString("color");

            var start = colorString.IndexOf("(");
            var length = colorString.IndexOf(")") - start - 2;
            var s = colorString.Substring(start + 1, length);

            string[] nums = s.Split(","[0]);

            float _red;
            float _green;
            float _blue;
            float _a;

            float.TryParse(nums[0], out _red);
            float.TryParse(nums[1], out _green);
            float.TryParse(nums[2], out _blue);
            float.TryParse(nums[3], out _a);

            Debug.Log(_red + "," + _green + "," + _blue + "," + _a);
            Debug.Log("Im HEre Mother");
            playerObject = GameObject.Find("Player");
            playerObject.gameObject.GetComponent<Renderer>().material.color = new Color(_red, _green, _blue, _a);
    }
    void Update()
    {
        if (isLocalPlayer)
        {
            //generalTime = (int)Time.time - generalTime;
            if (combat.health <= 0)
            {
                StopCoroutine("TimerCountUp");
                Destroy(gameObject);
            }
        }

    }
    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            Vector3 dir = camera.transform.TransformDirection(movement);
            dir.Set(dir.x, 0, dir.z);
            dir = dir.normalized * movement.magnitude;
            rigidbody.AddForce(dir * speed);
            followMe.transform.position = gameObject.transform.position;

            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y");

            mouseY = Mathf.Clamp(mouseY, Y_ANGLE_MIN, Y_ANGLE_MAX);
            Debug.Log(moveHorizontal);
        }
            
    }
    void LateUpdate()
    {
        if (isLocalPlayer)
        {
            Vector3 dir = new Vector3(0, 0, -3.5f);
            Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
            camera.transform.position = gameObject.transform.position + rotation * dir;

            camera.transform.LookAt(gameObject.transform.position);

            if (isSpikeCollected)
            {
                if (spikeStartTimer <= 0)
                {
                    //spikeTimerText.gameObject.SetActive(false);
                    playerObject.SetActive(true);
                    //spikeTimerText.gameObject.SetActive(false);
                    isSpikeCollected = false;
                    spikeStartTimer = 10;
                    StopCoroutine("SpikeCountDown");
                }
            }
        }
        
    }
    IEnumerator SpikeCountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            spikeStartTimer -= 1;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (isLocalPlayer)
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



    }
    void SpikeCollide(Collider other)
    {
        Destroy(other.gameObject);
        Debug.Log("Spike Collide");
        spikesObject.SetActive(true);
        playerObject.SetActive(false);
        //spikeTimerText.gameObject.SetActive(true);
        isSpikeCollected = true;
        StartCoroutine("SpikeCountDown");
    }
    void CoinCollide(Collider other)
    {
        Destroy(other.gameObject);
        Debug.Log("Coin Collide");
        points++;
        //pointsText.text = "Points: " + points;
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
        //gameOverMenu.SetActive(true);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }
}
