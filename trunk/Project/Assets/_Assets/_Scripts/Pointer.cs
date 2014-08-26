using UnityEngine;
using System.Collections;

public class Pointer : MonoBehaviour {

    private Transform PointerPosition;

	void Start () 
    {
        PointerPosition = transform.FindChild("PointerPosition");
	}
	
	void Update () 
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(PointerPosition.position, Vector3.forward, out hit, Mathf.Infinity))
            {
                //Debug.Log(hit.transform.renderer.material.name);
                if (hit.transform.tag == "Pie")
                {
                    Debug.Log(hit.transform.GetComponent<PieScript>().m_ePieType);
                }
                //if (hit.transform.renderer.material.color == Color.red)
                //{
                //    Debug.Log("Red");
                //}
                //else if (hit.transform.renderer.material.color == Color.blue)
                //{
                //    Debug.Log("Blue");
                //}
                //else if (hit.transform.renderer.material.color == Color.green)
                //{
                //    Debug.Log("green");
                //}
            }
        }
	}
}
