using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour
{
    public float gap = 20;
    public float followers = 2;
    public GameObject prefab;

    void Awake()
    {
        GameObject leader = CreateLeader();

        for (int i = 1; i <= followers; i++)
        {
            // Spawn right
            Vector3 offset = new Vector3(gap * i, 0, -gap * i);
            GameObject follower = CreateFollower(offset, leader.GetComponent<Boid>());
            // Spawn left
            offset = new Vector3(-gap * i, 0, -gap * i);
            follower = CreateFollower(offset, leader.GetComponent<Boid>());
        }

    }

    GameObject CreateLeader()
    {
        GameObject leader = Instantiate(prefab);
        leader.name = "Leader";
        leader.transform.parent = transform;
        leader.transform.position = transform.position;
        leader.transform.rotation = transform.rotation;

        GameObject target = new GameObject();
        target.transform.position = leader.transform.position + leader.transform.forward.normalized * 1000;

        Seek seek = leader.AddComponent<Seek>();
        seek.target = target.transform.position;

        FollowCamera camera = FindObjectOfType<FollowCamera>();
        camera.target = leader;

        return leader;
    }

    GameObject CreateFollower(Vector3 offset, Boid leader)
    {
        GameObject follower = Instantiate(prefab);
        follower.name = "Follower";
        follower.transform.position = transform.TransformPoint(offset);
        follower.transform.parent = transform;
        follower.transform.rotation = transform.rotation;

        OffsetPursue offsetPursue = follower.AddComponent<OffsetPursue>();
        offsetPursue.leader = leader;

        return follower;
    }
}
