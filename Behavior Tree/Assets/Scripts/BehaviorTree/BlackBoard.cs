using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard
{
    public Dictionary<string, float> floats;
    public Dictionary<string, int> ints;
    public Dictionary<string, bool> bools;
    public Dictionary<string, string> strings;
    public Dictionary<string, GameObject> objects;
    public Dictionary<string, Transform> trans;

    public void Init()
    {
        floats = new Dictionary<string, float>();
        ints = new Dictionary<string, int>();
        bools = new Dictionary<string, bool>();
        strings = new Dictionary<string, string>();
        objects = new Dictionary<string, GameObject>();
        trans = new Dictionary<string, Transform>();
    }
}
