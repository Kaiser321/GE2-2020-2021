using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public int pollen = 0;
    public GameObject target;
    private GameObject spot;
    public bool collecting = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CollectPollen());
        GetComponent<StateMachine>().ChangeState(new SeekFlower());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoWander()
    {
        if(spot)
        {
            Destroy(spot);
        }
        Vector3 pos = Random.insideUnitSphere * 50;
        spot = new GameObject("Spot");
        spot.tag = "Spot";
        spot.transform.position = new Vector3(pos.x, 0, pos.z);
        target = spot;
    }

    System.Collections.IEnumerator CollectPollen()
    {
        while (true)
        {
            if (collecting)
            {
                target.GetComponent<Flower>().pollen -= 1;
                pollen += 1;
            }
            yield return new WaitForSeconds(1f);
        }

    }
}
