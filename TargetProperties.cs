using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProperties : MonoBehaviour
{
    [Header("オブジェクトの設定")]
    public float stayDuration = 3f;
    public bool giveMoney = false;

    public int RandomMoney()
    {
        int min = 300;
        int max = 1000;
        int step = 100;

        int money = Random.Range(min / step, (max / step) + 1) * step;
        return money;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
