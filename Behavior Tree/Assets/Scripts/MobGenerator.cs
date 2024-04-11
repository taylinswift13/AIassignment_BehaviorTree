using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenerator : MonoBehaviour
{
    public GameObject mob;
    public List<Transform> spawnPlaces = new List<Transform>();
    public float delta = 1.0f;
    private float AT = 0.0f;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPlaces.Add(transform.GetChild(i));
        }
    }
    private void Update()
    {
        AT += Time.deltaTime;
        if (AT >= delta)
        {
            AT = 0.0f;
            GameObject agent = GameObject.Find("Agents");
            GameObject.Instantiate(mob, spawnPlaces[Random.Range(0, spawnPlaces.Count)].position, Quaternion.Euler(0, 0, 0), agent.transform);
        }
    }
}

