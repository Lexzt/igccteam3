using UnityEngine;
using System.Collections;

public class PieInternController : MonoBehaviour {

    private PieController PieControl;

    public float rotSpeed = 1;
    public float rotDecleaseRait = 0.99f;

    public bool m_bStopRotation;

    public bool m_bTriggerAnimation = false;

    public bool m_bStartWait = false;
    public bool m_bWait = false;

    public bool m_bTriggerEffect = false;

	void Start () 
    {
        PieControl = GameObject.Find("Pie Controller").GetComponent<PieController>();
	}
	
	void Update () 
    {
        transform.Rotate(Vector3.down, rotSpeed);
        if (m_bStopRotation)
            rotSpeed *= rotDecleaseRait;

        if (Mathf.Abs(rotSpeed) < 0.4f)
        {
            rotSpeed = 0;
            m_bStopRotation = false;
            m_bTriggerAnimation = true;

            StartCoroutine(HelperScript.WaitIntern(gameObject, 1));

            if (m_bWait == true && m_bStartWait == true)
            {
                GameObject.Find("Transition").GetComponent<TransitionScript>().m_bTriggerAnimation = true;
                m_bTriggerAnimation = false;
                m_bWait = false;
                m_bStartWait = false;
            }
        }

        if (m_bTriggerAnimation)
        {
            if (m_bTriggerEffect == false)
            {
                GameObject.Find("Pointer").GetComponent<Pointer>().CheckInternRay();
                m_bTriggerEffect = true;
            }
        }

        if (Mathf.Abs(rotSpeed) > 0.4f && m_bWait == true)
        {
            m_bWait = false;
        }
	}

    public void ResetValues()
    {
        Debug.Log("Reset Intern Control");
        rotSpeed = 1;
        m_bTriggerEffect = false;
        m_bWait = false;
        m_bStartWait = false;
    }
}
