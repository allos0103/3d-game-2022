using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 1   
// 直接修改transform.position的位置
public class movement2 : MonoBehaviour
{   // 初速度
    public float v1 = 10f;// speed x
    public float v2 = 100f;// speed y
    public float a = 1;// 加速度
    public float bigger = 1;// 放大坐标，更易看
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   // 抛物线移动 x = v1t y = v2t - 5 * t^2
        Vector3 position = transform.position;

        position.x += bigger * (Time.deltaTime*v1);  // 匀直
        position.y += bigger * (Time.deltaTime*(v2/10));

        transform.position = position;
        v2 -= a;   // 有加速度
    }
}
