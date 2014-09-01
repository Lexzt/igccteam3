using UnityEngine;
using System.Collections;

public class TransitionScript : MonoBehaviour {

    public bool m_bIsInside = false;
    public bool m_bTriggerAnimation = false;

    public float m_fSecondsWait = 2.0f;

    private bool m_bEnemyDead = false;

	void Start () 
    {
	
	}
	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_bTriggerAnimation = true;
        }

        if (m_bTriggerAnimation == true)
        {
            if (m_bIsInside == false)
            {
                foreach (Transform child in transform)
                {
                    if (child.GetComponent<HalfScript>().m_bEezing == false)
                    {
                        child.GetComponent<HalfScript>().MoveToCenter();
                        m_bIsInside = true;
                    }
                }
            }
            else if (m_bIsInside == true)
            {
                foreach (Transform child in transform)
                {
                    if (child.GetComponent<HalfScript>().m_bStartWait == false)
                    {
                        child.GetComponent<HalfScript>().WaitForSeconds(m_fSecondsWait);
                    }
                    else if (child.GetComponent<HalfScript>().m_bStartWait == true)
                    {
                        if (child.GetComponent<HalfScript>().m_bWait == true)
                        {
                            if (child.GetComponent<HalfScript>().m_bEezing == false)
                            {
                                //GameObject.Find("Pie Controller").GetComponent<PieController>().ResetPercByEnemy();
                                //GameObject.Find("GameManager").GetComponent<GameManager>().SwapTurns();

                                if (GameObject.Find("GameManager").GetComponent<GameManager>().m_eTurn == eTurn.ePLAYER)
                                {
                                    GameObject.Find("EnemyManager").GetComponent<EnemyManager>().InitOnPerc();
                                }
                                else if (GameObject.Find("GameManager").GetComponent<GameManager>().m_eTurn == eTurn.eENEMY)
                                {
                                    GameObject.Find("PlayerManager").GetComponent<PlayerManager>().InitOnPerc();
                                }
                                GameObject.Find("Pie Inner Circle").GetComponent<PieInternController>().ResetValues();

                                child.GetComponent<HalfScript>().MoveToOrigin();
                                m_bIsInside = false;
                                m_bTriggerAnimation = false;

                                child.GetComponent<HalfScript>().m_bWait = false;
                                child.GetComponent<HalfScript>().m_bStartWait = false;
                            }
                        }
                    }
                }
            }
        }
	}
}
