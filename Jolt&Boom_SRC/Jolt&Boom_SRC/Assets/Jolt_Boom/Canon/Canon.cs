using System.Linq;
using UnityEditor;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private Transform m_debugInSight;


    [SerializeField] private float m_dot_not_normalise;
    [SerializeField] private float m_dot;
    [SerializeField] private float m_dot_1_minus;
    [SerializeField, Range(0f, 360f)] private float m_rot_speed= 45;
    [SerializeField, Range(0.0f, 0.2f)] private float m_in_sight_epsilon = 1e-2f;
    [SerializeField] private Transform m_ballistic;
    [SerializeField] private Transform m_body;
    public float m_dist_to_target = 0.0f;
    [SerializeField, Range(1.0f, 100.0f)] private float m_ballistic_impluse = 10.0f;
    [SerializeField, Range(0.0f, 5.0f)] private float m_ballistic_cooldown = 0.5f;
    private float m_ballistic_time = 0.0f;


    [SerializeField] private Transform m_targetVehicleComponents;
    [SerializeField, Range(0.1f, 100.0f)] private float m_playerRangeThreshold = 20.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //check main target range 
        //which is the m_targetVehicleComponents
        if (Mathf.Abs(Vector3.SqrMagnitude(transform.position - m_targetVehicleComponents.position)) < m_playerRangeThreshold * m_playerRangeThreshold)
        {
            if (/*m_target == null &&*/ m_targetVehicleComponents != null)
            {

                if (m_targetVehicleComponents.TryGetComponent<VehicleComponents>(out VehicleComponents vehicleComponents))
                {
                    Debug.Log("Founbd components " + vehicleComponents.GetVehicleComponents().Count());
                    Transform target = null;
                    float rating = float.MaxValue;
                    foreach (var v_c in vehicleComponents.GetVehicleComponents())
                    {
                        //
                        if (v_c.Health <= 0.0)
                            continue;

                        float rate = v_c.Health / v_c.MaxHealth;
                        rate *= Vector3.Magnitude(transform.position - v_c.transform.position);

                        if (rate < rating)
                        {
                            target = v_c.transform;
                            rating = rate;
                        }
                    }

                    if (target != null)
                        m_target = target;
                    else
                        m_target = m_targetVehicleComponents;

                }
            }



            if (m_target != null)
            {
                Vector3 to_target = m_target.position - transform.position;
                m_dist_to_target = to_target.magnitude;
                Vector3 fwd = transform.forward;
                float m_dot = Vector3.Dot(fwd, to_target.normalized);

                Quaternion look_rot = Quaternion.LookRotation(to_target);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, look_rot, m_rot_speed * Time.deltaTime);
                //transform.rotation = Quaternion.FromToRotation(fwd, to_target);

                bool in_sight = (1 - m_dot) < m_in_sight_epsilon;

                if (in_sight)
                {
                    if (m_debugInSight != null)
                        m_debugInSight.gameObject.SetActive(true);

                    if (m_ballistic && m_ballistic_time > m_ballistic_cooldown)
                    {
                        GameObject obj = Instantiate(m_ballistic, transform.position, Quaternion.identity, m_body).gameObject;

                        if (obj.TryGetComponent<Rigidbody>(out Rigidbody rb))
                        {
                            Vector3 impluse = transform.forward * m_ballistic_impluse * m_dist_to_target;
                            rb.AddForce(impluse, ForceMode.Impulse);
                        }

                        m_ballistic_time = 0.0f;
                    }
                }
                else
                {
                    if (m_debugInSight != null)
                        m_debugInSight.gameObject.SetActive(false);
                }
            }
        } //Vector3.Distance(transform.position, m_targetVehicleComponents.position) < m_playerRangeThreshold


        m_ballistic_time += Time.deltaTime;
    }



    private void OnDrawGizmosSelected()
    {


            //draw forward 
            Gizmos.color = Color.blue;
        Vector3 pos = transform.position;
        Vector3 fwd = transform.forward;
        Gizmos.DrawLine(pos, pos + fwd);


        //draw vector top target
        if(m_target != null )
        {
            if (Mathf.Abs(Vector3.SqrMagnitude(transform.position - m_targetVehicleComponents.position)) < m_playerRangeThreshold * m_playerRangeThreshold)
            {            
             
                Gizmos.color = Color.yellow;
                Vector3 to_target = m_target.position - pos;    
                Gizmos.DrawLine (pos, pos + to_target);

                m_dot_not_normalise = Vector3.Dot(fwd, to_target);
                //unit vector
                Gizmos.color = Color.green;
                Gizmos.DrawLine (pos, pos + to_target.normalized);

                m_dot = Vector3.Dot(fwd, to_target.normalized);


                m_dot_1_minus = 1 - m_dot;
            }
        }



        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_playerRangeThreshold);

    }




}
