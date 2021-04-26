using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour {

    public GameObject resporcePrefab;

    public float radius = 50;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnResources());
	}

    Vector3 NextFlowerPos()
    {
        return transform.position + new Vector3(Random.Range(-radius, radius), -0.5f, Random.Range(-radius, radius));
    }

    System.Collections.IEnumerator SpawnResources()
    {
        while (true)
        {
            GameObject[] flowers = GameObject.FindGameObjectsWithTag("flower");
            if (flowers.Length < 10)
            {
                GameObject flower = GameObject.Instantiate<GameObject>(resporcePrefab);
                flower.transform.position = NextFlowerPos();
                flower.transform.parent = this.transform;
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
