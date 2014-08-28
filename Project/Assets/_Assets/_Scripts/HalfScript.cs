using UnityEngine;
using System.Collections;

public class HalfScript : MonoBehaviour {

    public bool m_bEezing = false;
    public bool m_bChangeOnce = false;

    // Trigger Wait
    public bool m_bWait = false;
    public bool m_bStartWait = false;

    public Vector3 m_vOriginalPosition;

	void Start () 
    {
        m_vOriginalPosition = transform.position;
	}
	
	void Update () 
    {
	
	}

    public void MoveToOrigin()
    {
        StartCoroutine(HelperScript.Eez(gameObject, transform.position, m_vOriginalPosition, 0.5f));
    }

    public void MoveToCenter()
    {
        Vector3 EndPos = new Vector3(transform.position.x,0,transform.position.z);
        StartCoroutine(HelperScript.Eez(gameObject, transform.position, EndPos, 0.5f));
    }

    public void WaitForSeconds(float tTimeInSeconds)
    {
        StartCoroutine(HelperScript.Wait(gameObject,tTimeInSeconds));
    }
}
