using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBlast : MonoBehaviour
{
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > 4f)
        {
            Destroy(gameObject);
        }
    }
}
