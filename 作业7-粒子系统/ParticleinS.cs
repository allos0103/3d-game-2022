using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleinS : MonoBehaviour
{   

    ParticleSystem part;
    ParticleSystem.ShapeModule parShape;
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        parShape = part.shape;
        if (SceneManager.GetActiveScene ().name == "Scene1" ){
            parShape.shapeType = ParticleSystemShapeType.Hemisphere;
        }
        else{
            parShape.shapeType = ParticleSystemShapeType.Sphere;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
