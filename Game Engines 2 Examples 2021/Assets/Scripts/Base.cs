using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float tiberium = 0;

    public TextMeshPro text;

    public GameObject fighterPrefab;

    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CollectTiberium", 1f, 1f);

        color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);

        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tiberium >= 10)
        {
            SpawnFighter();
        }
        text.text = "" + tiberium;
    }

    void CollectTiberium()
    {
        tiberium += 1;
    }

    void SpawnFighter()
    {
        GameObject fighter = Instantiate(fighterPrefab, transform.position, transform.rotation);

        foreach (Renderer r in fighter.GetComponentsInChildren<Renderer>())
        {
            r.material.color = color;
        }

        fighter.transform.SetParent(transform);
        tiberium -= fighter.GetComponent<Fighter>().cost;
    }

    public void OnTriggerEnter(Collider c)
    {
        if (c.tag == "bullet")
        {
            tiberium -= 0.5f;
            Destroy(c.gameObject);
        }
    }
}
