using UnityEngine;
using System;

public class VehicleComponents : MonoBehaviour
{
    //public List<VehicleComponent> m_components = new List<VehicleComponent>();
    [SerializeField] private VehicleComponent[] m_components;
    [SerializeField, Range(0, 1)] float m_health = 1.0f;
    public GameObject m_body;


    public VehicleComponent[] GetVehicleComponents() { return m_components; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(m_components.Length == 0)
        {
            if(m_body != null)
            {
                m_components = m_body.GetComponentsInChildren<VehicleComponent>();
            }
        }
    }



    // Update is called once per frame
    //void Update()
    //{
    //    if (m_health <= 0.0f)
    //    {
    //        //m_body.transform.DetachChildren();

    //        if(m_component)
    //        {
    //            m_component.transform.SetParent(null);
    //            if (m_component && m_component.TryGetComponent<Rigidbody>(out Rigidbody rb))
    //            {
    //                rb.SetActive(true);
    //            }
    //            else
    //            {
    //                m_component.AddComponent<Rigidbody>();

    //            }
    //        }

    //    }
    //}


    private void OnApplicationQuit()
    {
        Debug.Log("Game session ended !!!!!");

        //clear arrray m_component on exiting app 
        Array.Clear(m_components, 0, m_components.Length);
    }
}
