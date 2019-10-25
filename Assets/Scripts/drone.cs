using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drone : MonoBehaviour
{
    public float speed = 5f;
    public float turningSpeed = 5f;
    public float verticalSpeed = 5f;
    public float health = 15f;
    public float shots = 50f;
    public GameObject backshot;
    public GameObject fireBehind;
    public Image healthBar;
    public Image manaBar;
    public Image backBar;
    public Image staminaBar;

    private bool horizontalLock = false;
    private bool verticalLock = false;
    private bool lockShooting = false;
    private float shotingTime = 0f;
    private float regenTime = 0f;
    private float backshotTime = 3f;
    private float speedTime = 5f;
    private float originalSpeed;
    private GameObject compass;
    private Rigidbody rb;
    private AudioSource[] audios;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        compass = transform.GetChild(1).gameObject;
        audios = GetComponents<AudioSource>();
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        shotingTime += Time.deltaTime;
        regenTime += Time.deltaTime;
        backshotTime += Time.deltaTime;

        rb.velocity = transform.forward * speed;

        if (Input.GetAxis("Horizontal") != 0 && !horizontalLock)
        {
            verticalLock = true;

            transform.Rotate(transform.up * turningSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);
            
            if (Input.GetAxis("Horizontal") > 0 && compass.transform.localRotation.z > -0.33f)
            {
                compass.transform.Rotate(0, 0, -1.5f);
            }

            else if (Input.GetAxis("Horizontal") < 0 && compass.transform.localRotation.z < 0.33f)
            {
                compass.transform.Rotate(0, 0, 1.5f);
            } 
        }
        
        else if (Input.GetAxis("Vertical") != 0 && !verticalLock)
        {
            horizontalLock = true;

            transform.Translate(0f, verticalSpeed * Input.GetAxis("Vertical") * Time.deltaTime, 0f);

            if (Input.GetAxis("Vertical") > 0 && compass.transform.localRotation.x > -0.15f)
            {
                compass.transform.Rotate(-0.5f, 0, 0);
            }

            else if (Input.GetAxis("Vertical") < 0 && compass.transform.localRotation.x < 0.15f)
            {
                compass.transform.Rotate(0.5f, 0, 0);
            }
        }
        
        if (Input.GetAxis("Horizontal") == 0 && !horizontalLock)
        {
            if (compass.transform.localRotation.z > 0.005f)
                compass.transform.Rotate(0, 0, -1f);
            else if (compass.transform.localRotation.z < -0.005f)
                compass.transform.Rotate(0, 0, 1f);

            else
            {
                verticalLock = false;
            }

        }
        
        if (Input.GetAxis("Vertical") == 0 && !verticalLock)
        {
             if (compass.transform.localRotation.x > 0.005f)
                compass.transform.Rotate(-1f, 0, 0f);
            else if (compass.transform.localRotation.x < -0.005f)
                compass.transform.Rotate(1f, 0, 0f);

            else
            {
                horizontalLock = false;
            }

        }
  
        if ((Input.GetAxis("Jump") != 0 || Input.GetAxis("Fire2") != 0) && !lockShooting && shots > 0)
        {
            GameObject blasts = transform.GetChild(2).gameObject;
            audios[1].Play();

            for (int i = 0; i < blasts.transform.childCount; i++) 
            {
                if (blasts.transform.GetChild(i).gameObject.tag != "shot")
                {
                    GameObject blastTmp = blasts.transform.GetChild(i).gameObject;
                    blastTmp.gameObject.tag = "shot";
                    blastTmp.SetActive(true);
                    blastTmp.GetComponent<Rigidbody>().velocity = transform.forward * 400f;
                    blastTmp.transform.parent = null;
                    lockShooting = true;
                    shotingTime = 0f;
                    shots--;
                    break;
                }
            }
        }

        if ((Input.GetKeyDown(KeyCode.R) || Input.GetAxis("Fire1") != 0) && backshotTime > 3f) 
        {

            GameObject backShotTmp = Instantiate(backshot);
            backShotTmp.transform.parent = transform;
            backShotTmp.transform.position = backshot.transform.position;
            backShotTmp.gameObject.tag = "shot";
            backShotTmp.SetActive(true);
            backShotTmp.GetComponent<Rigidbody>().velocity = transform.forward * -400f;
            backShotTmp.transform.parent = null;
            backshotTime = 0;
            backBar.fillAmount = 0;
            audios[0].Play();
        }

        else if (backshotTime <= 3f)
        {
            backBar.fillAmount = backshotTime/3f;
        }

        if (Input.GetAxis("Fire3") != 0 && speedTime > 0)
        {
            speed = originalSpeed * 2;
            speedTime -= Time.deltaTime;
            fireBehind.transform.localScale = new Vector3 (10,2,10);
        }
        else
        {
            speed = originalSpeed;
            fireBehind.transform.localScale = new Vector3(5, 2, 5);

            if (speedTime < 5f)
            {
                speedTime += Time.deltaTime/2;
            }
        }

        if (shotingTime > 0.25f)
        {
            lockShooting = false;
        }

        if (shots < 50f && regenTime > 0.5f)
        {
            shots += 0.66f;
            regenTime = 0;
        }

        manaBar.fillAmount = shots / 50f;
        healthBar.fillAmount = health / 15f;
        staminaBar.fillAmount = speedTime / 5f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "shotEnemy")
        {
            health--;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "enemy")
        {
            health -= 2;
        }
    }
}
