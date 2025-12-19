using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoltBehaviour : MonoBehaviour
{
    public GameObject m_target;
    private Rigidbody m_rb;

    public float joltDistance = 6f;
    public float joltTime = 0.12f;

    private bool joltActive = false;
    private Vector3 joltStart;
    private Vector3 joltEnd;
    private float joltTimer = 0f;


    [Space]
    [Space]
    [SerializeField] List<GameObject> m_sceneTargets;
    [SerializeField] private String m_sceneTargetTag;
    [SerializeField, Range(0.0f, 100.0f)] private float m_selectTargetRange = 10.0f;
    public GameObject target_npc = null;


    void Start()
    {
        m_rb = m_target.GetComponent<Rigidbody>();

        GameObject.FindGameObjectsWithTag(m_sceneTargetTag, m_sceneTargets);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !joltActive)
        {
            StartJolt();
        }
    }

    void FixedUpdate()
    {
        if (!joltActive) return;

        joltTimer += Time.fixedDeltaTime;
        float t = joltTimer / joltTime;

        // Smooth interpolation
        Vector3 newPos = Vector3.Lerp(joltStart, joltEnd, t);

        // Move the rigidbody safely
        m_rb.MovePosition(newPos);

        if (t >= 1f)
        {
            joltActive = false;
        }
    }

    void StartJolt()
    {

        joltStart = m_rb.position;

        //get closest npc if in range
        //GameObject target_npc = null;
        float best_npc = float.MaxValue;
        foreach(var target in m_sceneTargets)
        {
            float dist = Vector3.Distance(joltStart, target.transform.position);
            if(dist < m_selectTargetRange && dist < best_npc)
            {
               best_npc = dist;
               target_npc = target;
            }
        }


        Vector3 jolt_dir = (target_npc != null) ? (target_npc.transform.position - joltStart) : m_target.transform.forward;

        jolt_dir.Normalize();


        joltEnd = joltStart + jolt_dir * joltDistance;

        joltTimer = 0f;
        joltActive = true;

        // optional: stop drift without fighting controller
        m_rb.linearVelocity = Vector3.zero;
        m_rb.angularVelocity = Vector3.zero;
    }



    private void OnDrawGizmosSelected()
    {

        if (m_target != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(m_target.transform.position, m_selectTargetRange);
        }
    }
}
