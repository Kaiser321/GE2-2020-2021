using UnityEngine;

public class ReturnBase : State
{
    public override void Enter()
    {
        owner.GetComponent<Arrive>().targetGameObject = owner.GetComponent<Fighter>().transform.parent.gameObject;
        owner.GetComponent<Arrive>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(owner.transform.position, owner.GetComponent<Fighter>().transform.parent.position) <= 2)
        {
            if (owner.transform.parent.GetComponent<Base>().tiberium >= 7)
            {
                owner.transform.parent.GetComponent<Base>().tiberium -= 7;
                owner.GetComponent<Fighter>().ammo = 7;
                owner.ChangeState(new GoAttack());
            }
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
    }
}

public class GoAttack : State
{
    public override void Enter()
    {
        owner.GetComponent<Arrive>().targetGameObject = owner.GetComponent<Fighter>().target.gameObject;
        owner.GetComponent<Arrive>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(owner.transform.position, owner.GetComponent<Fighter>().target.transform.position) < 15)
        {
            GameObject bullet = GameObject.Instantiate(owner.GetComponent<Fighter>().bulletPrehab, owner.transform.position + owner.transform.forward * 2, owner.transform.rotation);
            bullet.GetComponent<Renderer>().material.color = owner.GetComponent<Renderer>().material.color;
            owner.GetComponent<Fighter>().ammo--;

            if (owner.GetComponent<Fighter>().ammo <= 0)
            {
                owner.ChangeState(new ReturnBase());
            }
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
    }

}
