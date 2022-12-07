using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Start_Scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = transform.GetComponent<Button>();
        btn.onClick.AddListener(eventListener);
    }

    // Update is called once per frame
    void Update() {}

    void eventListener(){
        SceneManager.LoadScene("Scene2");
    }
}
