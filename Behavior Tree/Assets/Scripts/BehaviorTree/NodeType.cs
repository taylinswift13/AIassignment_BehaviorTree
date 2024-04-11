using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : Node
{
    private Node child;

    public RootNode(GameObject parentTree, BlackBoard blackboard)
    {
        gameObject = parentTree;
        transform = parentTree.transform;
        base.blackboard = blackboard;
    }

    public override NodeStatus Update()
    {
        return child.Update();
    }

    public void AddChild(Node child)
    {
        this.child = child;

        child.gameObject = gameObject;
        child.transform = transform;
        child.blackboard = blackboard;
    }
}

public class Decorater : Node
{
    protected Node child;

    public void AddChild(Node child)
    {
        this.child = child;

        child.gameObject = gameObject;
        child.transform = transform;
        child.blackboard = blackboard;
    }
}

public class Inverter : Decorater
{
    public override NodeStatus Update()
    {
        if (child == null)
        {
            Debug.LogError("Inverter needs at least one child!");
        }

        NodeStatus ret = child.Update();

        if (ret != NodeStatus.Failure)
        {
            return NodeStatus.Failure;
        }
        else
        {
            return NodeStatus.Success;
        }
    }
};

public class Composite : Node
{
    protected List<Node> children = new List<Node>();
    protected int currentIndex = 0;

    public void AddChild(Node child)
    {
        children.Add(child);

        child.gameObject = gameObject;
        child.transform = transform;
        child.blackboard = blackboard;
    }
}

public class Parallel : Composite
{
    private NodeStatus result = NodeStatus.Failure;

    public override NodeStatus Update()
    {
        if (children.Count == 0)
        {
            Debug.LogError("Parallel needs at least one child!");
        }

        foreach (Node child in children)
        {
            NodeStatus ret = child.Update();

            if (ret != NodeStatus.Success)
            {
                result = ret;
            }
        }

        return result;
    }
};

public class Selector : Composite
{
    public override NodeStatus Update()
    {
        if (children.Count == 0)
        {
            Debug.LogError("Selector needs at least one child!");
        }

        for (int i = currentIndex; i < children.Count; i++)
        {
            NodeStatus ret = children[i].Update();

            if (ret == NodeStatus.Success)
            {
                currentIndex = 0;
                return NodeStatus.Success;    //Already one child returns success
            }
            else if (ret == NodeStatus.Running)
            {
                return NodeStatus.Running;  //Keep updating this child
            }
            else if (ret == NodeStatus.Failure)
            {
                currentIndex++;
            }
        }

        currentIndex = 0;        //Reset index to 0   
        return NodeStatus.Failure;    //All children fail then return failure
    }
};

public class Sequencer : Composite
{
    public override NodeStatus Update()
    {
        if (children.Count == 0)
        {
            Debug.LogError("Sequencer needs at least one child!");
        }

        for (int i = currentIndex; i < children.Count; i++)
        {
            NodeStatus ret = children[i].Update();

            if (ret == NodeStatus.Success)
            {
                currentIndex++;        //Move to next child
            }
            else if (ret == NodeStatus.Running)
            {
                return NodeStatus.Running;  //Keep updating this child
            }
            else if (ret == NodeStatus.Failure)
            {
                currentIndex = 0;
                return NodeStatus.Failure;    //Reset index to 0
            }
        }

        currentIndex = 0;                //All children are updated, reset index to 0
        return NodeStatus.Success;
    }
};
