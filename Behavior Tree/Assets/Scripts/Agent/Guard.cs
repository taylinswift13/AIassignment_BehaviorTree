using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guard : Agent
{
    public List<Vector3> points = new List<Vector3>();
    public Slider healthbar;
    public GameObject vfx_heal;
    void Start()
    {
        type = "Guard";
        tree = GetComponent<GuardTree>();
        tree.InitRootNode(blackboard);
        tree.InitTree();

        blackboard.objects.Add("TargetMob", null);
        blackboard.objects.Add("VFX_Heal", vfx_heal);
        blackboard.floats.Add("DetectRadius", 10f);

        blackboard.ints.Add("HP", 10);
        blackboard.ints.Add("StartHP", 10);
        blackboard.ints.Add("DyingHP", 7);

        blackboard.bools.Add("IsMobAround", false);
        blackboard.bools.Add("IsDead", false);
        blackboard.bools.Add("IsDying", false);
        blackboard.bools.Add("IsHealed", false);
    }
    private void Update()
    {
        healthbar.value = (float)blackboard.ints["HP"] / (float)blackboard.ints["StartHP"];
    }
}
