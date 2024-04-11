using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    public RootNode root;

    public bool restart = false;

    private bool ableToRun = true;

    public void InitRootNode(BlackBoard blackboard)
    {
        root = new RootNode(gameObject, blackboard);
    }

    private void FixedUpdate()
    {
        NodeStatus ret = NodeStatus.Success;

        if (ableToRun == true)
        {
            ret = root.Update();
        }

        if (ret != NodeStatus.Running && restart == false)
        {
            ableToRun = false;
        }
    }

    public virtual void InitTree()
    {

    }
}
