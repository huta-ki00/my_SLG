using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float speed;
    public Camera mainCamera;


    private void Awake()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed, 0, 0, Space.World);
        if (IsOffScreenLight())
        {
            Destroy(gameObject);
        }
    }

    bool IsOffScreenLight()
    {
        Vector3 viewPos = mainCamera.WorldToScreenPoint(transform.position);
        return viewPos.x > 900;
    }
}
