using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTree : BehaviorTree
{
    public override void InitTree()
    {
        restart = true;

        Selector selector = new Selector();
        Sequencer sequencer1 = new Sequencer();
        Sequencer sequencer2 = new Sequencer();
        Sequencer sequencer3 = new Sequencer();
        Sequencer sequencer4 = new Sequencer();

        CompareBool compareBool1 = new CompareBool("IsDead");
        CompareBool compareBool2 = new CompareBool("IsGuardAround");
        CompareBool compareBool3 = new CompareBool("IsArrived");
        CompareBool compareBool4 = new CompareBool("IsArrived");

        Inverter inverter1 = new Inverter();

        DeadthChecker deathChecker = new DeadthChecker();
        MobDie die = new MobDie();
        DetectGuard detectGuard = new DetectGuard();
        Attack attack = new Attack();
        MoveToTarget moveToTarget = new MoveToTarget(3f);
        Win destroyTarget = new Win();


        root.AddChild(selector);

        selector.AddChild(sequencer1);
        selector.AddChild(sequencer2);
        selector.AddChild(sequencer3);
        selector.AddChild(sequencer4);

        sequencer1.AddChild(deathChecker);
        sequencer1.AddChild(compareBool1);
        sequencer1.AddChild(die);

        sequencer2.AddChild(detectGuard);
        sequencer2.AddChild(compareBool2);
        sequencer2.AddChild(attack);

        sequencer3.AddChild(inverter1);
        inverter1.AddChild(compareBool3);
        sequencer3.AddChild(moveToTarget);

        sequencer4.AddChild(compareBool4);
        sequencer4.AddChild(destroyTarget);
    }
}
