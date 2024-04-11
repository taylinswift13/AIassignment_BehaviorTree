using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadthChecker : Action
{
    public override NodeStatus Update()
    {
        int HP = blackboard.ints["HP"];
        if (HP <= 0)
        {
            blackboard.bools["IsDead"] = true;
        }
        return NodeStatus.Success;
    }
}
public class MobDie : Action
{
    bool isPlayed = false;
    public override NodeStatus Update()
    {
        if (!isPlayed)
        {
            gameObject.GetComponent<Animation>().Play("Die");
            isPlayed = true;
        }
        if (!gameObject.GetComponent<Animation>().isPlaying)
        {
            gameObject.SetActive(false);
        }
        return NodeStatus.Success;
    }
}

public class DetectGuard : Action
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
            foreach (GameObject g in guards)
            {
                if (Vector3.Distance(g.gameObject.transform.position, transform.position) <
                    blackboard.floats["DetectRadius"])
                {
                    blackboard.bools["IsGuardAround"] = true;
                    blackboard.objects["TargetGuard"] = g;
                }
            }
            return NodeStatus.Success;
        }
    }
}

public class Attack : Action
{
    public override NodeStatus Update()
    {
        if (blackboard.objects["TargetGuard"] == null)
        {
            return NodeStatus.Failure;
        }
        transform.LookAt(blackboard.objects["TargetGuard"].transform);
        if (gameObject.GetComponent<Animation>().isPlaying == false)
        {
            gameObject.GetComponent<Animation>().Play("Attack");
            blackboard.objects["TargetGuard"].GetComponent<Guard>().blackboard.ints["HP"]--;
        }
        if (blackboard.objects["TargetGuard"].GetComponent<Guard>().blackboard.ints["HP"] <= 0)
        {
            blackboard.objects["TargetGuard"] = null;
            blackboard.bools["IsGuardAround"] = false;
        }
        return NodeStatus.Success;
    }
}

public class MoveToTarget : Action
{
    public Vector3 position;
    public float speed;

    public MoveToTarget(float speed)
    {
        this.speed = speed;
        position = Vector3.zero;
    }
    public override NodeStatus Update()
    {
        if (blackboard.objects["TargetGuard"] != null || blackboard.bools["IsGuardAround"])
        {
            return NodeStatus.Failure;
        }
        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        transform.LookAt(position);
        gameObject.GetComponent<Animation>().Play("Walk");
        if (transform.position == position)
            blackboard.bools["IsArrived"] = true;
        else
            blackboard.bools["IsArrived"] = false;
        return NodeStatus.Success;
    }
}

public class Win : Action
{
    bool isPlayed = false;
    public override NodeStatus Update()
    {
        if (!isPlayed)
        {
            gameObject.GetComponent<Animation>().Play("Victory");
            isPlayed = true;
            GameObject.Find("Main Camera").GetComponent<Camera>().fieldOfView = 30;
            return NodeStatus.Running;
        }
        else
        {
            if (!gameObject.GetComponent<Animation>().isPlaying)
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
                return NodeStatus.Success;
            }
            else
            {
                return NodeStatus.Running;
            }
        }

    }
}



