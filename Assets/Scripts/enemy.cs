using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float movementSpeed = 10f;
    public GameObject shot;

    private GameObject Player;
    private int healthIndicator = 1;
    private float timer = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("drone");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;

        timer += Time.deltaTime;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 400f))
        {
            if (hit.transform.tag == "player1" && timer > 5f)
            {
                GameObject backShotTmp = Instantiate(shot);
                backShotTmp.transform.parent = transform;
                backShotTmp.transform.position = shot.transform.position;
                backShotTmp.gameObject.tag = "shotEnemy";
                backShotTmp.SetActive(true);
                backShotTmp.transform.LookAt(Player.transform);
                backShotTmp.GetComponent<Rigidbody>().velocity = transform.forward * 200f;
                backShotTmp.transform.parent = null;
                backShotTmp.transform.localScale = new Vector3(2, 2, 2);
                timer = 0;
            }
        }
    }


    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "shot")
        {
            healthIndicator--;

            if (healthIndicator == 0)
            {
                Destroy(gameObject);
            }
        }

        else if (other.gameObject.tag == "player1")
        {
            Destroy(gameObject);
        }
    }
}
