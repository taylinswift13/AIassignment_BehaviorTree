using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthChecker : Action
{
    public override NodeStatus Update()
    {
        int HP = blackboard.ints["HP"];
        if (HP <= 0)
        {
            blackboard.bools["IsDead"] = true;
        }
        else if (HP <= blackboard.ints["DyingHP"])
        {
            blackboard.bools["IsDying"] = true;
        }
        else
        {
            blackboard.bools["IsDead"] = false;
            blackboard.bools["IsDying"] = false;
        }
        return NodeStatus.Success;
    }
}
public class GetHeal : Action
{
    float duration;
    float AT = 0;
    public GetHeal(float duration)
    {
        this.duration = duration;
    }
    public override NodeStatus Update()
    {
        AT += Time.deltaTime;
        blackboard.objects["VFX_Heal"].SetActive(true);
        if (AT > duration)
        {
            blackboard.objects["VFX_Heal"].SetActive(false);
            blackboard.bools["IsHealed"] = false;
            AT = 0;
            return NodeStatus.Success;
        }
        return NodeStatus.Running;
    }
}

public class GuardDie : Action
{
    public override NodeStatus Update()
    {
        gameObject.SetActive(false);
        return NodeStatus.Success;
    }
}

public class Melee : Action
{
    public override NodeStatus Update()
    {
        if (blackboard.objects["TargetMob"] == null)
        {
            return NodeStatus.Failure;
        }
        transform.LookAt(blackboard.objects["TargetMob"].transform);
        if (gameObject.GetComponent<Animation>().isPlaying == false)
        {
            gameObject.GetComponent<Animation>().Play("Attack");
            blackboard.objects["TargetMob"].GetComponent<Mob>().blackboard.ints["HP"] -= 5;
        }
        if (blackboard.objects["TargetMob"].GetComponent<Mob>().blackboard.ints["HP"] <= 0)
        {
            blackboard.objects["TargetMob"] = null;
            blackboard.bools["IsMobAround"] = false;
        }
        return NodeStatus.Success;
    }
}

public class Patrol_Guard : Action
{
    private float speed = 7.5f;
    private List<Vector3> points = new List<Vector3>();
    private Vector3 nextPoint;
    private int index;
    private bool inited = false;
    public Patrol_Guard(float speed)
    {
        this.speed = speed;
    }
    public override NodeStatus Update()
    {
        if (inited == false)
        {
            float distance = 5000;
            for (int i = 0; i < gameObject.GetComponent<Guard>().points.Count; i++)
            {
                float dis = Vector3.Distance(transform.position,
                    gameObject.GetComponent<Guard>().points[i]);
                if (dis < distance)
                {
                    index = i;
                    distance = dis;
                }
                points.Add(gameObject.GetComponent<Guard>().points[i]);
            }
            nextPoint = points[index];
            inited = true;
        }
        if (transform.position != nextPoint)
        {
            transform.LookAt(nextPoint);
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);
            gameObject.GetComponent<Animation>().Play("Walk");
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


