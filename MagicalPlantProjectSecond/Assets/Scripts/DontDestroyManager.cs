﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyManager : MonoBehaviour
{
    static public DontDestroyManager my;
    SoundManager sound;
    SceneChangeManager scene;
    public SoundManager Sound
    {
        get { return sound; }
    }
    public SceneChangeManager Scene
    {
        get { return scene; }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (my == null)
        {
            my = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        sound = GetComponent<SoundManager>();
        scene = GetComponent<SceneChangeManager>();
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    StartCoroutine(sound.FadeIn());
        //}
        //else if (Input.GetMouseButtonDown(1))
        //{
        //    StartCoroutine(sound.FadeOut());
        //}
    }
    
}
