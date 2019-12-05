using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    LogManager log;
    public void AnimStart(LogManager l)
    {
        log = l;
        GetComponent<Animator>().SetTrigger("Start");
    }
    public void Break()
    {
        log.LogDeleteStart();
        GameObject.Destroy(gameObject);
    }
}
