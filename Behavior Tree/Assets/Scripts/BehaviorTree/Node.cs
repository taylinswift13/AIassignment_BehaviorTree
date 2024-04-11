using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeStatus
{
    Success,
    Failure,
    Running
}

public class Node
{
    public GameObject gameObject;
    public Transform transform;
    public BlackBoard blackboard;

    public virtual NodeStatus Update()
    {
        return NodeStatus.Success;
    }
};

