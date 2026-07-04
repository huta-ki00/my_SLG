using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloud;
    //public float duration;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenCloud", 1, 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Instantiate(cloud, transform.position, transform.rotation);
    }

    void GenCloud()
    {
        Instantiate(cloud, new Vector3(-10, -5f + 9 * Random.value, 0), Quaternion.identity);
    }
}
