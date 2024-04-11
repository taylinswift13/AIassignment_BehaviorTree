using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardTree : BehaviorTree
{
    public override void InitTree()
    {
        restart = true;

        Parallel parallel = new Parallel();
        Selector selector = new Selector();
        Sequencer sequencer1 = new Sequencer();
        Sequencer sequencer2 = new Sequencer();
        Sequencer sequencer3 = new Sequencer();
        Sequencer sequencer4 = new Sequencer();

        CompareBool compareBool1 = new CompareBool("IsDead");
        CompareBool compareBool2 = new CompareBool("IsHealed");
        CompareBool compareBool3 = new CompareBool("IsMobAround");



        DeadthChecker checker = new DeadthChecker();
        GetHeal getheal = new GetHeal(1f);
        GuardDie die = new GuardDie();
        DetectMob detectMobs = new DetectMob();
        Melee melee = new Melee();
        Patrol_Guard patrol = new Patrol_Guard(7f);

        root.AddChild(parallel);

        parallel.AddChild(selector);
        parallel.AddChild(sequencer4);
        sequencer4.AddChild(compareBool2);
        sequencer4.AddChild(getheal);

        selector.AddChild(sequencer1);
        selector.AddChild(sequencer2);
        selector.AddChild(sequencer3);

        sequencer1.AddChild(checker);
        sequencer1.AddChild(compareBool1);
        sequencer1.AddChild(die);

        sequencer2.AddChild(detectMobs);
        sequencer2.AddChild(compareBool3);
        sequencer2.AddChild(melee);

        sequencer3.AddChild(patrol);
    }
}
