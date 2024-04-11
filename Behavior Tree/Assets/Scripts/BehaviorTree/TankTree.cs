using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTree : BehaviorTree
{
    public override void InitTree()
    {
        restart = true;

        Selector selector = new Selector();
        Sequencer sequencer1 = new Sequencer();
        Sequencer sequencer2 = new Sequencer();
        Sequencer sequencer3 = new Sequencer();

        CompareBool compareBool1 = new CompareBool("IsMobAround");
        CompareBool compareBool2 = new CompareBool("IsDyingGuardAround");
        CompareBool compareBool3 = new CompareBool("IsMobAround");
        CompareBool compareBool4 = new CompareBool("IsDyingGuardAround");

        Inverter inverter_1 = new Inverter();
        Inverter inverter_2 = new Inverter();

        DetectMob detectMobs = new DetectMob();
        FireMob fire = new FireMob(50f);
        DetectDyingGuard detectDyingGuard = new DetectDyingGuard();
        HealGuard heal = new HealGuard(50f);
        Patrol_Tank patrol = new Patrol_Tank(10f);

        root.AddChild(selector);

        selector.AddChild(sequencer1);
        selector.AddChild(sequencer2);
        selector.AddChild(sequencer3);

        sequencer1.AddChild(detectMobs);
        sequencer1.AddChild(compareBool1);
        sequencer1.AddChild(fire);

        sequencer2.AddChild(detectDyingGuard);
        sequencer2.AddChild(compareBool2);
        sequencer2.AddChild(heal);

        sequencer3.AddChild(inverter_1);
        sequencer3.AddChild(inverter_2);
        inverter_1.AddChild(compareBool3);
        inverter_2.AddChild(compareBool4);
        sequencer3.AddChild(patrol);
    }
}
