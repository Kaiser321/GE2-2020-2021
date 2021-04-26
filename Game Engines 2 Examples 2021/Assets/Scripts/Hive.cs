using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    public int pollen = 10;
    public int bee = 0;
    public GameObject beePrehab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBees());
    }

    // Update is called once per frame
    void Update()
    {

    }

    System.Collections.IEnumerator SpawnBees()
    {
        while (true)
        {
            if (bee < 10 && pollen >= 5)
            {
                GameObject newBee = Instantiate(beePrehab);
                newBee.transform.parent = transform;
                pollen -= 5;
                bee += 1;
            }
            yield return new WaitForSeconds(2f);
        }

    }
}

