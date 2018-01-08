using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private int spikeStartTimer = 10;

    private float spikeCountDown;
    [SerializeField]
    public bool isSpikeCollected = false;

    [SerializeField]
    private int generalTime = 0;

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

    [SerializeField]
    private int numOFCoins = 0;

    [SerializeField]
    public GameObject nextGame;
    [SerializeField]
    private Button buttonNextGAme;

    void Start()
    {
        ParseColor();
        
        offset = gameObject.transform.position - followMe.transform.position;
        rigidbody = GetComponent<Rigidbody>();
        currentMesh = GetComponent<MeshRenderer>();
        //generalTime = (int)Time.time;
        combat = GetComponent<Combat>();
        StartCoroutine("TimerCountUp");
        numOFCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        Debug.Log(numOFCoins);
        pointsText.text = "Coins: " + points + "/" + numOFCoins;
    }
    void ParseColor()
    {
        string colorString = PlayerPrefs.GetString("color");

        var start = colorString.IndexOf("(");
        var length = colorString.IndexOf(")") - start - 2;
        var s = colorString.Substring(start + 1, length);

        string [] nums = s.Split(","[0]);

        float _red;
        float _green;
        float _blue;
        float _a;

        float.TryParse(nums[0], out _red);
        float.TryParse(nums[1], out _green);
        float.TryParse(nums[2], out _blue);
        float.TryParse(nums[3],out  _a);

        Debug.Log(_red + "," + _green + "," + _blue + "," + _a);
        playerObject.gameObject.GetComponent<Renderer>().material.color = new Color(_red, _green, _blue, _a);
    }
    void Update()
    {
        //generalTime = (int)Time.time - generalTime;
        if(combat.health <= 0)
        {
            StopCoroutine("TimerCountUp");
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
            if(spikeStartTimer <= 0)
            {
                spikeTimerText.gameObject.SetActive(false);
                playerObject.SetActive(true);
                spikeTimerText.gameObject.SetActive(false);
                isSpikeCollected = false;
                spikeStartTimer = 10;
                StopCoroutine("SpikeCountDown");
            }
            else
            {   
                spikeTimerText.text = "Spikes Timer: " + (int)spikeStartTimer;
            }
        }
    }
    IEnumerator SpikeCountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            spikeStartTimer -=1;
        }
    }
    IEnumerator TimerCountUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            generalTime+=1;
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
        StartCoroutine("SpikeCountDown");
    }
    void CoinCollide(Collider other)
    {
        Destroy(other.gameObject);
        Debug.Log("Coin Collide");
        points++;
        pointsText.text = "Coins: " + points +"/"+numOFCoins;
        if(points >= numOFCoins)
        {
            Destroy(gameObject);
            EndLevelShowResults();
        }
    }
    void EndLevelShowResults()
    {
        nextGame.SetActive(true);
        //NextLevePane.SetActive(true);
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
    public void BackToMenu()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }
}
