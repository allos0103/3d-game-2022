using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : MonoBehaviour
{
    public float v1 = 5f;//10e30f;// speed
    public float v2 = 5f;//10e30f;// speed
    public float t = 0;
    public float bigger = 5;
    public float ty = 0;
    public float ground = 0;
    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        ty = 0;
    }

    // Update is called once per frame
    void Update()
    {   // 抛物线移动 x = v1t y = v2t - 5 * t^2
        t += Time.deltaTime ;
        ty += Time.deltaTime;
        
        Vector3 position = transform.position;
        position.x = bigger*(v1 * t);
        position.y = bigger*(v2 * ty -  ty * ty);

        if(position.y<=ground){
            ty = 0;
        }
        transform.position = position;
    }
}
