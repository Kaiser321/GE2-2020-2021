using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prey")
        {
            GetComponent<BigBoid>().pursueEnabled = false;
            GetComponent<BigBoid>().fleeEnabled = true;
            GetComponent<BigBoid>().fleeTargetTransform = other.transform;

        }
    }
}
