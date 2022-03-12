using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Transform parentTransform;

    private int randomIndex;
    private float startTime = 0;
    private float intervalTime = 3.0f;
    private GameObject temp;
    private Vector3 offset = new Vector3(0,-0.5f,0);
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", startTime, intervalTime);
    }

    void Spawn()
    {
        randomIndex = RandomIndexGenerator();
        temp =Instantiate(obstaclePrefabs[randomIndex], transform.position + offset , obstaclePrefabs[randomIndex].transform.rotation);
        temp.transform.SetParent(parentTransform);
    }

    int  RandomIndexGenerator()
    {
        int randomnum = Random.Range(0, obstaclePrefabs.Length);
        return randomnum;
    }
}
