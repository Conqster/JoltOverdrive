using UnityEngine;
using UnityEngine.UI;

public class VehicleComponent : MonoBehaviour, IHealth
{

    [SerializeField, Range(0, 100)] private int m_health = 100;
    [SerializeField, Range(0, 100)] private int m_maxhealth = 100;

    [SerializeField] Image m_healthImage;

    public int Health
    {
        get { return m_health; }
        set { m_health = value; }
    }

    public int MaxHealth
    {
        get { return m_maxhealth; }
        set { m_maxhealth = value; }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_healthImage.fillAmount = (float)m_health / 100.0f;

        if (m_health <= 0.0f)
        {
            transform.SetParent(null);
            if (TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.SetActive(true);
            }
            else
            {
                gameObject.AddComponent<Rigidbody>();
            }
            if (TryGetComponent<Collider>(out Collider collider))
                 collider.isTrigger = false;
        }

    }




    public void ReceiveDamage(int damage)
    {
        m_health -= damage;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Sphere"))
            ReceiveDamage(20);

        //Debug.Log("Collides with object" + other.gameObject.name);
    }

}