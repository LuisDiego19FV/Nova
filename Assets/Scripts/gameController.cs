using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour
{
    public GameObject squad;
    public GameObject player;

    public int level = 1;

    public TMP_Text enemiesTxt;
    public TMP_Text levelsTxt;

    private float timer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < level * 5; i++)
        {
            GameObject squadTmp = Instantiate(squad);
            squadTmp.transform.position = player.transform.position;
            squadTmp.transform.Translate(1000f * (Random.Range(0, 2) * 2 - 1) * Random.Range(0.5f, 1), 0f, 1000f * (Random.Range(0, 2) * 2 - 1) * Random.Range(0.5f, 1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1f)
        {
            timer = 0;
            float childCounter = 0;
            GameObject[] cloneSearch = GameObject.FindGameObjectsWithTag("enemy");

            for (int i = 0; i < cloneSearch.Length; i++)
            {
                childCounter += cloneSearch[i].transform.childCount;
            }

            if (cloneSearch.Length == 0)
            {
                level++;
                for (int i = 0; i < level * 5; i++)
                {
                    GameObject squadTmp = Instantiate(squad);
                    squadTmp.transform.position = player.transform.position;
                    squadTmp.transform.Translate(1000f * (Random.Range(0, 2) * 2 - 1) * Random.Range(0.5f, 1), 0f, 1000f * (Random.Range(0, 2) * 2 - 1) * Random.Range(0.5f, 1));
                }
            }

            enemiesTxt.text = childCounter.ToString();
            levelsTxt.text = level.ToString();

        }
    }
}
