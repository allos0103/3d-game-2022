using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor;

public class DiskData : MonoBehaviour
{   
    public int speed; // angle per sec
    public int point;
    
    public Vector3 r; //圆半径，也就是要旋转的向量。
    public Vector3 CenterPosition = new Vector3();
    public Vector3 direction = new Vector3();

    // Start is called before the first frame update
    void Start()
    {    
        r = transform.position - CenterPosition; //圆心指向“我”的向量，就是半径
    }

    // Update is called once per frame
    void Update()
    {
        // this.transform.RotateAround(targetTrans.position, Vector3.forward, 180 * Time.deltaTime);
        //以每秒180度的速度旋转“半径”向量，旋转轴是Z轴。
        // r = Quaternion.AngleAxis(180 * Time.deltaTime, Vector3.up) * r;
        r = Quaternion.AngleAxis(speed * Time.deltaTime, direction) * r;
        //圆心位置 + 半径 = 圆上的点
        transform.position = CenterPosition + r;
    }
}
[CustomEditor (typeof(DiskData))]
public class DiskDataEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DiskData d = (DiskData)target;

        d.speed = EditorGUILayout.IntSlider ("Speed", d.speed, 0, 360);
        ProgressBar (d.speed / 360.0f, "Speed");

        d.point= EditorGUILayout.IntSlider ("Point", d.point, 0, 10);
        ProgressBar (d.point / 10.0f, "Point");

        d.direction= EditorGUILayout.Vector3Field("旋转轴",d.direction);

        d.CenterPosition = EditorGUILayout.Vector3Field("旋转中心点",d.CenterPosition);

        d.transform.position = EditorGUILayout.Vector3Field("旋转初始点",d.transform.position);

    }

    // Custom GUILayout progress bar.
    void ProgressBar (float value, string label)
    {
        // Get a rect for the progress bar using the same margins as a textfield:
        Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
        EditorGUI.ProgressBar (rect, value, label);
        EditorGUILayout.Space ();
    }
}