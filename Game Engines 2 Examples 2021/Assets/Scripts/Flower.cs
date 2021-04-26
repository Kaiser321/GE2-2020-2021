using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public float pollen = 0;
    // Use this for initialization
    void Start()
    {
        pollen = Random.Range(1, 5);
        transform.localScale = new Vector3(1, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale, new Vector3(1, pollen, 1), Time.deltaTime);
        Vector3 pos = transform.position;
        pos.y = transform.localScale.y / 2;
        transform.position = pos;

        if(pollen <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
