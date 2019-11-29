using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMaker : MonoBehaviour
{
    [SerializeField]private GameObject[] particles;
    public GameObject[] Particle
    {
        get { return particles; }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void PaticleMake(GameObject particle,Vector3 pos)
    {
        Instantiate(particle,pos,new Quaternion());
    }
}
