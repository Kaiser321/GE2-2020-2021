using UnityEngine;

public class SeekFlower : State
{
    public override void Enter()
    {
        owner.GetComponent<Bee>().GoWander();
        owner.GetComponent<Arrive>().targetGameObject = owner.GetComponent<Bee>().target;
        owner.GetComponent<Arrive>().enabled = true;
    }

    public override void Think()
    {
        GameObject[] flowers = GameObject.FindGameObjectsWithTag("flower");
        foreach(GameObject f in flowers)
        {
            if(Vector3.Distance(owner.transform.position, f.transform.position) <= 20)
            {
                owner.GetComponent<Bee>().target = f;
                owner.ChangeState(new GoToFlower());
            }
        }

        if (Vector3.Distance(owner.transform.position, owner.GetComponent<Bee>().target.transform.position) <= 5)
        {
            owner.ChangeState(new SeekFlower());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
    }
}

public class GoToFlower : State
{
    public override void Enter()
    {
        owner.GetComponent<Arrive>().targetGameObject = owner.GetComponent<Bee>().target;
        owner.GetComponent<Arrive>().enabled = true;
    }

    public override void Think()
    {
        if (!owner.GetComponent<Bee>().target)
        {
            owner.ChangeState(new SeekFlower());
        }

        if (Vector3.Distance(owner.transform.position, owner.GetComponent<Bee>().target.transform.position) <= 5)
        {
            owner.ChangeState(new CollectPollen());
        }


    }

    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
    }
}

public class CollectPollen : State
{
    public override void Enter()
    {
        owner.GetComponent<Bee>().collecting = true;
    }

    public override void Think()
    {
        if (!owner.GetComponent<Bee>().target)
        {
            owner.ChangeState(new ReturnToHive());
        }
        //// If flower has no more pollen
        //if (owner.GetComponent<Bee>().target.GetComponent<Flower>().pollen <= 0)
        //{
        //    owner.ChangeState(new ReturnToHive());
        //}
    }

    public override void Exit()
    {
        owner.GetComponent<Bee>().collecting = false;
    }
}

public class ReturnToHive : State
{
    public override void Enter()
    {
        owner.GetComponent<Bee>().target = owner.transform.parent.gameObject;
        owner.GetComponent<Arrive>().targetGameObject = owner.GetComponent<Bee>().target;
        owner.GetComponent<Arrive>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(owner.transform.position, owner.GetComponent<Bee>().target.transform.position) <= 2.5f)
        {
            owner.ChangeState(new DepositPollen());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
    }
}

public class DepositPollen : State
{
    public override void Enter()
    {

    }

    public override void Think()
    {
        if(owner.GetComponent<Bee>().pollen <= 0)
        {
            owner.ChangeState(new SeekFlower());
        }
        else
        {
            owner.GetComponent<Bee>().target.GetComponent<Hive>().pollen += owner.GetComponent<Bee>().pollen;
            owner.GetComponent<Bee>().pollen = 0;
        }
    }

    public override void Exit()
    {
    }
}

//public class ReturnBase : State
//{
//    public override void Enter()
//    {
//        owner.GetComponent<Arrive>().targetGameObject = owner.GetComponent<Fighter>().transform.parent.gameObject;
//        owner.GetComponent<Arrive>().enabled = true;
//    }

//    public override void Think()
//    {
//        if (Vector3.Distance(owner.transform.position, owner.GetComponent<Fighter>().transform.parent.position) <= 2)
//        {
//            if (owner.transform.parent.GetComponent<Base>().tiberium >= 7)
//            {
//                owner.transform.parent.GetComponent<Base>().tiberium -= 7;
//                owner.GetComponent<Fighter>().ammo = 7;
//                owner.ChangeState(new GoAttack());
//            }
//        }
//    }

//    public override void Exit()
//    {
//        owner.GetComponent<Arrive>().enabled = false;
//    }
//}

//public class GoAttack : State
//{
//    public override void Enter()
//    {
//        owner.GetComponent<Arrive>().targetGameObject = owner.GetComponent<Fighter>().target.gameObject;
//        owner.GetComponent<Arrive>().enabled = true;
//    }

//    public override void Think()
//    {
//        if (Vector3.Distance(owner.transform.position, owner.GetComponent<Fighter>().target.transform.position) < 15)
//        {
//            GameObject bullet = GameObject.Instantiate(owner.GetComponent<Fighter>().bulletPrehab, owner.transform.position + owner.transform.forward * 2, owner.transform.rotation);
//            bullet.GetComponent<Renderer>().material.color = owner.GetComponent<Renderer>().material.color;
//            owner.GetComponent<Fighter>().ammo--;

//            if (owner.GetComponent<Fighter>().ammo <= 0)
//            {
//                owner.ChangeState(new ReturnBase());
//            }
//        }
//    }

//    public override void Exit()
//    {
//        owner.GetComponent<Arrive>().enabled = false;
//    }

//}