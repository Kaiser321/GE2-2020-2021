using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int cost = 10;

    public int ammo = 7;

    public GameObject target;

    public GameObject bulletPrehab;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> bases = new List<GameObject>(GameObject.FindGameObjectsWithTag("Base"));
        bases.Remove(transform.parent.gameObject);
        target = bases[Random.Range(0, bases.Count)];

        GetComponent<StateMachine>().ChangeState(new GoAttack());
        //GetComponent<StateMachine>().SetGlobalState(new Alive());
    }

    //public void OnTriggerEnter(Collider c)
    //{
    //    if (c.tag == "Bullet")
    //    {
    //        if (GetComponent<Fighter>().health > 0)
    //        {
    //            GetComponent<Fighter>().health--;
    //        }
    //        Destroy(c.gameObject);
    //        if (GetComponent<StateMachine>().currentState.GetType() != typeof(Dead))
    //        {
    //            GetComponent<StateMachine>().ChangeState(new DefendState());
    //        }
    //    }
    //}

}
