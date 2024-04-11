using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Agent : MonoBehaviour
{
    [HideInInspector]
    public string type = "None";
    [HideInInspector]
    public BehaviorTree tree;
    [HideInInspector]
    public BlackBoard blackboard;

    private void Awake()
    {
        blackboard = new BlackBoard();
        blackboard.Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Guard" && other.tag == "Healing")
        {
            Destroy(other.gameObject);
        }
    }
    void OnDisable()
    {
        Destroy(gameObject);
    }
}
