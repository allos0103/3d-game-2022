using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 使用Vector3
public class movement3 : MonoBehaviour
{
    public float v1 = 10f;// speed x
    public float v2 = 100f;// speed y
    public float a = 1;// 加速度
    // public float bigger = 1;// 放大坐标，更易看
    void Start()
    {
        
    }

    void Update()
    {   // 抛物线移动 x = v1t y = v2t - 5 * t^2
        Vector3 change = new Vector3(Time.deltaTime*v1,Time.deltaTime*(v2/10),0);
        transform.position += change;
        // transform.Translate(change);
        v2 -= a;   // 有加速度
    }
}
