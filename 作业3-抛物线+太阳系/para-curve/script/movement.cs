using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 1   
// 直接修改transform.position的位置
public class movement : MonoBehaviour
{   // 初速度
    public float v1 = 10f;// speed x
    public float v2 = 5f;// speed y
    public float t = 0;  // total time
    public float bigger = 5;// 放大坐标，更易看
    // Start is called before the first frame update
    void Start()
    {
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {   // 抛物线移动 x = v1t y = v2t - 5 * t^2
        t += Time.deltaTime ;
        Vector3 position = transform.position;

        position.x = bigger * (v1 * t);
        position.y = bigger * (v2 * t -  5 * t * t);

        transform.position = position;
        
    }
}
