using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Agent
{
    public GameObject shell;
    public GameObject healing;
    public Transform fireTrans;
    void Start()
    {
        type = "Tank";
        tree = GetComponent<TankTree>();
        tree.InitRootNode(blackboard);
        tree.InitTree();

        blackboard.objects.Add("TargetMob", null);
        blackboard.objects.Add("TargetGuard", null);
        blackboard.objects.Add("Shell", shell);
        blackboard.objects.Add("Healing", healing);
        blackboard.trans.Add("FireTransform", fireTrans);
        blackboard.floats.Add("DetectRadius", 30f);

        blackboard.bools.Add("IsMobAround", false);
        blackboard.bools.Add("IsDyingGuardAround", false);
        blackboard.bools.Add("Fired", false);
    }
}
