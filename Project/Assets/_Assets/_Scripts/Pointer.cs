using UnityEngine;
using System.Collections;

public class Pointer : MonoBehaviour {

    private Transform PointerPosition;
    private EnemyManager EnemManager;
    private PieController PieControl;

    public int m_iBaseDamage = 10;
    public int m_iCritMultiplier = 2;
    public int m_iConstantIncrement = 4;

    // Debug
    private Vector3 NewDirection;

	void Start () 
    {
        PointerPosition = transform.FindChild("PointerPosition");
        EnemManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        PieControl = GameObject.Find("Pie Controller").GetComponent<PieController>();

        NewDirection = new Vector3(0.0f, 0.2f, 1f);
	}
	
	void Update () 
    {
        Debug.DrawRay(PointerPosition.position, Vector3.forward);
        Debug.DrawRay(PointerPosition.position, NewDirection);
	}

    public void CheckRay()
    {
        Debug.Log("Checking Ray");
        RaycastHit hit;
        if (Physics.Raycast(PointerPosition.position, Vector3.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Pie")
            {
                Debug.Log(hit.transform.GetComponent<PieScript>().m_ePieType);

                if (hit.transform.GetComponent<PieScript>().m_ePieType == ePIETYPE.eATTACK)
                {
                    EnemManager.DecreaseHealth(m_iBaseDamage + PieControl.m_iSpinNo * m_iConstantIncrement);
                }
                else if (hit.transform.GetComponent<PieScript>().m_ePieType == ePIETYPE.eCRITATTACK)
                {
                    EnemManager.DecreaseHealth((m_iBaseDamage + PieControl.m_iSpinNo * m_iConstantIncrement) * 2);
                }
                else if (hit.transform.GetComponent<PieScript>().m_ePieType == ePIETYPE.eFAIL)
                {

                }
            }
        }
        else
        {
            // In between objects
            Debug.Log("Hit Nothing Once!");
            if (Physics.Raycast(PointerPosition.position, NewDirection, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "Pie")
                {
                    Debug.Log(hit.transform.GetComponent<PieScript>().m_ePieType);

                    if (hit.transform.GetComponent<PieScript>().m_ePieType == ePIETYPE.eATTACK)
                    {
                        EnemManager.DecreaseHealth(m_iBaseDamage + PieControl.m_iSpinNo * m_iConstantIncrement);
                    }
                    else if (hit.transform.GetComponent<PieScript>().m_ePieType == ePIETYPE.eCRITATTACK)
                    {
                        EnemManager.DecreaseHealth((m_iBaseDamage + PieControl.m_iSpinNo * m_iConstantIncrement) * 2);
                    }
                    else if (hit.transform.GetComponent<PieScript>().m_ePieType == ePIETYPE.eFAIL)
                    {

                    }
                }
            }
            else
            {
                Debug.Log("Hit Nothing Again!");
            }
        }
    }
}
