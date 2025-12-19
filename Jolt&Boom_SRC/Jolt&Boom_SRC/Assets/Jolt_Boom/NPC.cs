using UnityEngine;

public class NPC : MonoBehaviour
{

    [SerializeField] private Color32 m_defaultColour = Color.grey;
    [SerializeField] private float m_selectionCoolDown = 0.1f;

    private bool m_resetCol = false;
    public bool m_testTrigger = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if(m_testTrigger)
        {
            TriggerSelect(20.0f, Color.red);
            m_testTrigger = false;
        }

        if(m_selectionCoolDown > 0.0f)
            m_selectionCoolDown--;


        if(m_selectionCoolDown <= 0.0f && m_resetCol)
        {
            MeshRenderer mr = GetComponent<MeshRenderer>();
            if (mr != null && mr.materials.Length > 0)
            {
                Material mat = mr.sharedMaterials[0];
                mat.color = m_defaultColour;
                //renderer.sharedMaterial
            }

            m_resetCol = false;
        }
    }


    public void TriggerSelect(float cool_down, Color highlight_col)
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr != null && mr.materials.Length > 0)
        {
            Material mat = mr.sharedMaterials[0];
            mat.color = highlight_col;
            //renderer.sharedMaterial
            m_selectionCoolDown = cool_down;
            m_resetCol = true;
        }

    }


    private void OnDrawGizmos()
    {
        //MeshRenderer mr = GetComponent<MeshRenderer>();
        //if(mr != null && mr.materials.Length > 0 )
        //{
        //    Material mat = mr.sharedMaterials[0];
        //    mat.color = m_defaultColour;
        //    //renderer.sharedMaterial
        //}
    }


}
