using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] List<GameObject> m_sceneTargets;
    [SerializeField] private String m_sceneTargetTag;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject.FindGameObjectsWithTag(m_sceneTargetTag, m_sceneTargets);
    }

    // Update is called once per frame
    void Update()
    {
        //compute the closest target 
    }
}
