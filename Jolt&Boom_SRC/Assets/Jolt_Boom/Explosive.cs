using UnityEngine;
using UnityEngine.InputSystem.HID;

public interface IExplodable
{
    public void ReceiveDamage(int damage);
}


[RequireComponent(typeof(Rigidbody))]
public class Explosive : MonoBehaviour, IExplodable
{
    [SerializeField, Range(100, 1000)] private float explosionForce;
    [SerializeField, Range(0, 20)] private float explosionRadius;
    [SerializeField, Range(0, 20)] private int health, damage;
    [SerializeField, Range(0f, 5f)] private float timer = 1f;
    [SerializeField] private bool useTimer;
    [SerializeField] private bool alreadyExploded = false;
    [SerializeField] private bool drawExplosionRadius = false;
    //private List<Transform> enemiesToStablize = new List<Transform>();

    [SerializeField, Range(0.0f, 100.0f)] private float m_relSpeed4CollsionExplosion = 15.0f;

    private void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr != null && mr.materials.Length > 0)
        {
            Material mat = mr.sharedMaterials[0];
            mat.color = Color.green;
            //renderer.sharedMaterial
        }
    }

    private void Update()
    {
        if (useTimer)
            Invoke("TriggerExplosion", timer);
    }


    public void ReceiveDamage(int damage)
    {
        health -= damage;

        Debug.Log("Received Damage!!!!");
        if (health <= 0)
        {
            Invoke("TriggerExplosion", 0.5f);
            Debug.Log("About to explode!!!!");
        }
        //TriggerExplosion();

    }



    private void TriggerExplosion()
    {

        if (!alreadyExploded)
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                IExplodable canExplode = hit.GetComponent<IExplodable>();
               // ICharacter canReiceveDmg = hit.GetComponent<ICharacter>();
                if (canExplode != null && hit.transform != this /*&& !alreadyExploded*/)
                {
                    hit.SendMessage("ReceiveDamage", damage);
                }
                //else if (canReiceveDmg != null && hit.CompareTag("Enemies"))
                //{
                //    hit.SendMessage("ReceiveDamage", damage);
                //}


                if (rb != null)
                    rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, 1.5f);

                alreadyExploded = true;
            }



            MeshRenderer mr = GetComponent<MeshRenderer>();
            if (mr != null && mr.materials.Length > 0)
            {
                Material mat = mr.sharedMaterials[0];
                mat.color = Color.red;
                //renderer.sharedMaterial
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!alreadyExploded && collision.relativeVelocity.sqrMagnitude > (m_relSpeed4CollsionExplosion * m_relSpeed4CollsionExplosion))
        {
            int damage_points = 2;
            if (collision.transform.TryGetComponent<Explosive>(out Explosive explosive))
                damage_points *= 2;

            //SendMessage("ReceiveDamage", damage_points);
            ReceiveDamage(damage_points);
        }


        if (collision.transform.TryGetComponent<IHealth>(out IHealth health))
            collision.transform.SendMessage("ReceiveDamage", damage);
    }

    private void OnDrawGizmos()
    {
        if (drawExplosionRadius)
        {
            Gizmos.color = (!alreadyExploded) ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }

    }

}
