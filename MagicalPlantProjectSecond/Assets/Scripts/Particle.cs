using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    ParticleSystem ps;
    // Start is called before the first frame update
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(ParticleBreak());
    }
    IEnumerator ParticleBreak()
    {
        while (true)
        {
            if (ps.isPlaying)
            {
                yield return null;
            }
            else
            {
                break;
            }
        }
        Destroy(gameObject);
    }
}
