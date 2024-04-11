using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectDyingGuard : Action
{
    public override NodeStatus Update()
    {
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard");
        if (guards.Length == 0)
        {
            return NodeStatus.Failure;
        }
        else
        {
            blackboard.bools["Fired"] = false;
            foreach (GameObject g in guards)
            {
                if (Vector3.Distance(g.gameObject.transform.position, transform.position) <
                    blackboard.floats["DetectRadius"] && g.GetComponent<Guard>().blackboard.ints["HP"] <= g.GetComponent<Guard>().blackboard.ints["DyingHP"])
                {
                    blackboard.bools["IsDyingGuardAround"] = true;
                    blackboard.objects["TargetGuard"] = g;
                }
            }
            return NodeStatus.Success;
        }
    }
}

public class HealGuard : Action
{
    float speed;
    public HealGuard(float speed)
    {
        this.speed = speed;
    }
    public override NodeStatus Update()
    {
        GameObject targetGuard = blackboard.objects["TargetGuard"];
        Transform firetrans = blackboard.trans["FireTransform"];
        if (targetGuard == null)
        {
            return NodeStatus.Failure;
        }
        transform.LookAt(targetGuard.transform);
        //fire healing to the target guard
        if (!blackboard.bools["Fired"])
        {
            GameObject.Instantiate(blackboard.objects["Healing"], firetrans.position, firetrans.rotation);
            blackboard.bools["Fired"] = true;
            return NodeStatus.Running;
        }
        else
        {
            GameObject healing = GameObject.FindGameObjectWithTag("Healing");
            if (healing != null)
            {
                healing.transform.position = Vector3.MoveTowards(healing.transform.position, targetGuard.transform.position, speed * Time.deltaTime);
                return NodeStatus.Running;
            }
            else
            {
                blackboard.objects["TargetGuard"].GetComponent<Guard>().blackboard.ints["HP"] = 10;
                blackboard.objects["TargetGuard"].GetComponent<Guard>().blackboard.bools["IsHealed"] = true;
                blackboard.objects["TargetGuard"] = null;
                blackboard.bools["IsDyingGuardAround"] = false;
                return NodeStatus.Success;
            }
        }
    }
}

public class FireMob : Action
{
    float speed;
    public FireMob(float speed)
    {
        this.speed = speed;
    }
    public override NodeStatus Update()
    {
        GameObject targetMob = blackboard.objects["TargetMob"];
        Transform firetrans = blackboard.trans["FireTransform"];
        if (targetMob == null)
        {
            return NodeStatus.Failure;
        }
        transform.LookAt(targetMob.transform);
        if (!blackboard.bools["Fired"])
        {
            GameObject.Instantiate(blackboard.objects["Shell"], firetrans.position, firetrans.rotation);
            blackboard.bools["Fired"] = true;
            return NodeStatus.Running;
        }
        else
        {
            GameObject shell = GameObject.FindGameObjectWithTag("Shell");
            if (shell != null)
            {
                shell.transform.position = Vector3.MoveTowards(shell.transform.position, targetMob.transform.position, speed * Time.deltaTime);
                return NodeStatus.Running;
            }
            else
            {
                blackboard.objects["TargetMob"].GetComponent<Mob>().blackboard.ints["HP"] = 0;
                blackboard.objects["TargetMob"] = null;
                blackboard.bools["IsMobAround"] = false;
                return NodeStatus.Success;
            }
        }
    }
}

public class Patrol_Tank : Action
{
    private float speed = 7.5f;
    private List<GameObject> points = new List<GameObject>();
    private GameObject nextPoint;
    private int index;
    private bool inited = false;
    public Patrol_Tank(float speed)
    {
        this.speed = speed;
    }
    public override NodeStatus Update()
    {
        if (inited == false)
        {
            float distance = 5000;
            for (int i = 0; i < GameObject.Find("PatrolPoints").transform.childCount; i++)
            {
                float dis = Vector3.Distance(transform.position,
                    GameObject.Find("PatrolPoints").transform.GetChild(i).transform.position);
                if (dis < distance)
                {
                    index = i;
                    distance = dis;
                }
                points.Add(GameObject.Find("PatrolPoints").transform.GetChild(i).gameObject);
            }
            nextPoint = points[index];
            inited = true;
        }
        if (transform.position != nextPoint.transform.position)
        {
            transform.LookAt(nextPoint.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, nextPoint.transform.position, speed * Time.deltaTime);
        }
        else
        {
            index++;
            if (index >= points.Count)
            {
                index -= points.Count;
            }
            nextPoint = points[index];
        }
        return NodeStatus.Success;
    }
}
