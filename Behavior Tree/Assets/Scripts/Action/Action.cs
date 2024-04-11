using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : Node
{

}
public class CompareBool : Action
{
    private string key = "None";

    public CompareBool(string key)
    {
        this.key = key;
    }

    public override NodeStatus Update()
    {
        if (blackboard.bools.ContainsKey(key) == false)
        {
            return NodeStatus.Failure;
        }

        if (blackboard.bools[key] == false)
        {
            return NodeStatus.Failure;
        }

        return NodeStatus.Success;
    }
}
public class DetectMob : Action
{
    public override NodeStatus Update()
    {
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
        if (mobs.Length == 0)
        {
            return NodeStatus.Failure;
        }
        else
        {
            foreach (GameObject m in mobs)
            {
                if (Vector3.Distance(m.gameObject.transform.position, transform.position) <
                    blackboard.floats["DetectRadius"] && blackboard.objects["TargetMob"] == null)
                {
                    blackboard.objects["TargetMob"] = m;
                    if (!m.GetComponent<Mob>().blackboard.bools["IsDead"])
                    {
                        blackboard.bools["IsMobAround"] = true;
                    }
                }
            }
            return NodeStatus.Success;
        }
    }
}


