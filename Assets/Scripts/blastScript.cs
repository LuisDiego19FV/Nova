using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blastScript : MonoBehaviour
{
    public GameObject parent;

    private float time = 0f;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 7f)
        {
            transform.SetParent(parent.transform);
            transform.tag = "Untagged";
            transform.localPosition = new Vector3(0.7277751f, 1.892639f, 15.40078f);
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        time = 0f;
    }

    private void OnEnable()
    {
        time = 0f;
    }
}
