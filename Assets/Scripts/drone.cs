using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drone : MonoBehaviour
{
    public float speed = 5f;
    public float turningSpeed = 5f;
    public float verticalSpeed = 5f;
    public GameObject backshot;

    private bool horizontalLock = false;
    private bool verticalLock = false;
    private bool lockShooting = false;
    private float shotingTime = 0f;
    private GameObject compass;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        compass = transform.GetChild(1).gameObject;
}

    // Update is called once per frame
    void Update()
    {
        shotingTime += Time.deltaTime;

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
  
        if (Input.GetAxis("Jump") != 0 && !lockShooting)
        {
            GameObject blasts = transform.GetChild(2).gameObject;

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
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject backShotTmp = Instantiate(backshot);
            backShotTmp.transform.parent = transform;
            backShotTmp.transform.position = backshot.transform.position;
            backShotTmp.gameObject.tag = "shot";
            backShotTmp.SetActive(true);
            backShotTmp.GetComponent<Rigidbody>().velocity = transform.forward * -400f;
            backShotTmp.transform.parent = null;
        }

        if (shotingTime > 0.25f)
        {
            lockShooting = false;
        }
    }
}
