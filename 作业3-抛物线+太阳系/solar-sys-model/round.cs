using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class round : MonoBehaviour
{   // 设定旋转轴
    public float x;
    public float y;
    public float z;

    public float anglepersec; // 设定每秒旋转的角度

    // 通过设定不同的旋转轴和角速度，为每个星球设定不同的法平面和旋转速度

    Vector3 CenterPosition = Vector3.zero; //圆心的位置，设定在（0，0）点
    Vector3 r; //圆半径，也就是要旋转的向量。

    Vector3 thecenter;

    // Start is called before the first frame update
    void Start()
    {
        r = transform.position - CenterPosition; //圆心指向“我”的向量，就是半径
        thecenter.x = x;
        thecenter.y = y;
        thecenter.z = z;
    }

    // Update is called once per frame
    void Update()
    {   
        // this.transform.RotateAround(targetTrans.position, Vector3.forward, 180 * Time.deltaTime);
        //以每秒180度的速度旋转“半径”向量，旋转轴是Z轴。
        // r = Quaternion.AngleAxis(180 * Time.deltaTime, Vector3.up) * r;
        r = Quaternion.AngleAxis(anglepersec * Time.deltaTime, thecenter) * r;
        //圆心位置 + 半径 = 圆上的点
        transform.position = CenterPosition + r;
    }
}
