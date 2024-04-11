using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Agent
{
    void Start()
    {
        type = "Mob";
        tree = GetComponent<MobTree>();
        tree.InitRootNode(blackboard);
        tree.InitTree();

        blackboard.objects.Add("TargetGuard", null);

        blackboard.floats.Add("DetectRadius", 10f);
        blackboard.ints.Add("HP", 15);

        blackboard.bools.Add("IsDead", false);
        blackboard.bools.Add("IsArrived", false);
        blackboard.bools.Add("IsGuardAround", false);
    }
}
