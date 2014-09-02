using UnityEngine;
using System.Collections;

public class Pointer : MonoBehaviour {

    private Transform PointerPosition;
    private Transform InnerPointerPosition;
    private EnemyManager EnemManager;
    private PieController PieControl;

    public int m_iBaseDamage = 10;
    public int m_iCritMultiplier = 2;
    public int m_iConstantIncrement = 4;

    // Debug
    private Vector3 NewDirection;

    public int m_iCurrentDamage = 0;

    private GameManager GameManagerInstance;

    private PlayerManager PlayerManagerInstance;
    private EnemyManager EnemyManagerInstance;

    private bool m_bSingleSecondSpin = false;

    // Feedback
    private GameObject GUIFeedback = null;

	void Start () 
    {
        PointerPosition = transform.FindChild("PointerPosition");
        InnerPointerPosition = transform.FindChild("PointerInternPosition");
        EnemManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        PieControl = GameObject.Find("Pie Controller").GetComponent<PieController>();

        GameManagerInstance = GameObject.Find("GameManager").GetComponent<GameManager>();
        PlayerManagerInstance = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        EnemyManagerInstance = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();

        GUIFeedback = GameObject.Find("Damage");

        NewDirection = new Vector3(0.0f, 0.2f, 1f);
	}
	
	void Update () 
    {
        Debug.DrawRay(PointerPosition.position, Vector3.forward);
        Debug.DrawRay(PointerPosition.position, NewDirection);

        Debug.DrawRay(InnerPointerPosition.position, Vector3.forward);
        Debug.DrawRay(InnerPointerPosition.position, NewDirection);
	}

    public void CheckRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(PointerPosition.position, Vector3.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Pie")
            {
                //Debug.Log(hit.transform.GetComponent<PieScript>().m_ePieType);

                if (hit.transform.GetComponent<PieScript>().m_ePieType == ePIETYPE.eATTACK)
                {
                    m_iCurrentDamage += (m_iBaseDamage + PieControl.m_iSpinNo * m_iConstantIncrement);
                }
                else if (hit.transform.GetComponent<PieScript>().m_ePieType == ePIETYPE.eCRITATTACK)
                {
                    m_iCurrentDamage += (m_iBaseDamage + PieControl.m_iSpinNo * m_iConstantIncrement);
                    m_iCurrentDamage *= 2;
                }
                else if (hit.transform.GetComponent<PieScript>().m_ePieType == ePIETYPE.eFAIL)
                {
                    m_iCurrentDamage = 0;
                }

                //GUIFeedback.GetComponent<GUIScript>().Text = m_iCurrentDamage.ToString();
                GUIFeedback.GetComponent<GUIText>().text = m_iCurrentDamage.ToString();
                GUIFeedback.GetComponent<GUIFade>().ResetAlpha();
            }
        }
        else
        {
            // In between objects
            if (Physics.Raycast(PointerPosition.position, NewDirection, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "Pie")
                {
                    //Debug.Log(hit.transform.GetComponent<PieScript>().m_ePieType);

                    if (hit.transform.GetComponent<PieScript>().m_ePieType == ePIETYPE.eATTACK)
                    {
                        m_iCurrentDamage += m_iBaseDamage + PieControl.m_iSpinNo * m_iConstantIncrement;
                    }
                    else if (hit.transform.GetComponent<PieScript>().m_ePieType == ePIETYPE.eCRITATTACK)
                    {
                        m_iCurrentDamage += m_iBaseDamage + PieControl.m_iSpinNo * m_iConstantIncrement;
                        m_iCurrentDamage *= 2;
                    }
                    else if (hit.transform.GetComponent<PieScript>().m_ePieType == ePIETYPE.eFAIL)
                    {
                        m_iCurrentDamage = 0;
                    }
                    //GUIFeedback.GetComponent<GUIScript>().Text = m_iCurrentDamage.ToString();
                    GUIFeedback.GetComponent<GUIText>().text = m_iCurrentDamage.ToString();
                    GUIFeedback.GetComponent<GUIFade>().ResetAlpha();
                }
            }
            else
            {
                Debug.Log("Hit Nothing Again!");
            }
        }
    }

    public void CheckInternRay()
    {
        RaycastHit hit;
        // Check ray for Internal Object
        if (Physics.Raycast(InnerPointerPosition.position, Vector3.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "PieIntern")
            {
                //Debug.Log(hit.transform.GetComponent<PieInternScript>().m_ePieType);

                if (hit.transform.GetComponent<PieInternScript>().m_ePieType == eBONUSPIETYPE.eNORMALATK)
                {
                    if (GameManagerInstance.m_eTurn == eTurn.ePLAYER)
                    {
                        //Debug.Log("Deal " + m_iCurrentDamage + " Dmg to enemy");
                        EnemyManagerInstance.DecreaseHealth(m_iCurrentDamage);
                        GameManagerInstance.m_eTurn = eTurn.eENEMY;
                    }
                    else if (GameManagerInstance.m_eTurn == eTurn.eENEMY)
                    {
                        //Debug.Log("Deal " + m_iCurrentDamage + " Dmg to player");
                        PlayerManagerInstance.DecreaseHealth(m_iCurrentDamage);
                        GameManagerInstance.m_eTurn = eTurn.ePLAYER;
                    }
                    //GUIFeedback.GetComponent<GUIScript>().Text = m_iCurrentDamage.ToString();
                    GUIFeedback.GetComponent<GUIText>().text = m_iCurrentDamage.ToString();
                    GUIFeedback.GetComponent<GUIFade>().ResetAlpha();
                    m_iCurrentDamage = 0;
                }
                else if (hit.transform.GetComponent<PieInternScript>().m_ePieType == eBONUSPIETYPE.eDOUBLEATK)
                {
                    m_iCurrentDamage *= 2;
                    if (GameManagerInstance.m_eTurn == eTurn.ePLAYER)
                    {
                        //Debug.Log("Deal " + m_iCurrentDamage + " Dmg to enemy");
                        EnemyManagerInstance.DecreaseHealth(m_iCurrentDamage);
                        GameManagerInstance.m_eTurn = eTurn.eENEMY;
                    }
                    else if (GameManagerInstance.m_eTurn == eTurn.eENEMY)
                    {
                        //Debug.Log("Deal " + m_iCurrentDamage + " Dmg to player");
                        PlayerManagerInstance.DecreaseHealth(m_iCurrentDamage);
                        GameManagerInstance.m_eTurn = eTurn.ePLAYER;
                    }
                    //GUIFeedback.GetComponent<GUIScript>().Text = m_iCurrentDamage.ToString();
                    GUIFeedback.GetComponent<GUIText>().text = m_iCurrentDamage.ToString();
                    GUIFeedback.GetComponent<GUIFade>().ResetAlpha();
                    m_iCurrentDamage = 0;
                }
                else if (hit.transform.GetComponent<PieInternScript>().m_ePieType == eBONUSPIETYPE.eSECONDSPIN)
                {

                }
                //else if (hit.transform.GetComponent<PieInternScript>().m_ePieType == eBONUSPIETYPE.eSTUN)
                //{
                //    m_iCurrentDamage = 0;
                //}
            }
        }
        else
        {
            // In between objects
            Debug.Log("Hit Nothing Once!");
            if (Physics.Raycast(InnerPointerPosition.position, NewDirection, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "PieIntern")
                {
                    Debug.Log(hit.transform.GetComponent<PieInternScript>().m_ePieType);

                    if (hit.transform.GetComponent<PieInternScript>().m_ePieType == eBONUSPIETYPE.eNORMALATK)
                    {
                        if (GameManagerInstance.m_eTurn == eTurn.ePLAYER)
                        {
                            Debug.Log("Deal " + m_iCurrentDamage + " Dmg to enemy");
                            EnemyManagerInstance.DecreaseHealth(m_iCurrentDamage);
                            GameManagerInstance.m_eTurn = eTurn.eENEMY;
                        }
                        else if (GameManagerInstance.m_eTurn == eTurn.eENEMY)
                        {
                            Debug.Log("Deal " + m_iCurrentDamage + " Dmg to player");
                            PlayerManagerInstance.DecreaseHealth(m_iCurrentDamage);
                            GameManagerInstance.m_eTurn = eTurn.ePLAYER;
                        }
                        //GUIFeedback.GetComponent<GUIScript>().Text = m_iCurrentDamage.ToString();
                        GUIFeedback.GetComponent<GUIText>().text = m_iCurrentDamage.ToString();
                        GUIFeedback.GetComponent<GUIFade>().ResetAlpha();
                        m_iCurrentDamage = 0;
                    }
                    else if (hit.transform.GetComponent<PieInternScript>().m_ePieType == eBONUSPIETYPE.eDOUBLEATK)
                    {
                        m_iCurrentDamage *= 2;
                        if (GameManagerInstance.m_eTurn == eTurn.ePLAYER)
                        {
                            Debug.Log("Deal " + m_iCurrentDamage + " Dmg to enemy");
                            EnemyManagerInstance.DecreaseHealth(m_iCurrentDamage);
                            GameManagerInstance.m_eTurn = eTurn.eENEMY;
                        }
                        else if (GameManagerInstance.m_eTurn == eTurn.eENEMY)
                        {
                            Debug.Log("Deal " + m_iCurrentDamage + " Dmg to player");
                            PlayerManagerInstance.DecreaseHealth(m_iCurrentDamage);
                            GameManagerInstance.m_eTurn = eTurn.ePLAYER;
                        }
                        //GUIFeedback.GetComponent<GUIScript>().Text = m_iCurrentDamage.ToString();
                        GUIFeedback.GetComponent<GUIText>().text = m_iCurrentDamage.ToString();
                        GUIFeedback.GetComponent<GUIFade>().ResetAlpha();
                        m_iCurrentDamage = 0;
                    }
                    else if (hit.transform.GetComponent<PieInternScript>().m_ePieType == eBONUSPIETYPE.eSECONDSPIN)
                    {

                    }
                    //else if (hit.transform.GetComponent<PieInternScript>().m_ePieType == eBONUSPIETYPE.eSTUN)
                    //{
                    //    m_iCurrentDamage = 0;
                    //}
                }
            }
            else
            {
                Debug.Log("Hit Nothing Again!");
            }
        }
    }
}
