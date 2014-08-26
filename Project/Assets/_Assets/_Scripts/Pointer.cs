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
        if (Physics.Raycast(PointerPosition.position, Vector3.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.renderer.material.color == Color.red)
            {
                Debug.Log("Red");
            }
            else if (hit.transform.renderer.material.color == Color.blue)
            {
                Debug.Log("Blue");
            }
            else if (hit.transform.renderer.material.color == Color.black)
            {
                Debug.Log("Black");
            }
            else if (hit.transform.renderer.material.color == Color.cyan)
            {
                Debug.Log("Cyan");
            }
            else if (hit.transform.renderer.material.color == Color.gray)
            {
                Debug.Log("Gray");
            }
            else if (hit.transform.renderer.material.color == Color.green)
            {
                Debug.Log("Green");
            }
            else if (hit.transform.renderer.material.color == Color.magenta)
            {
                Debug.Log("Magenta");
            }
            else if (hit.transform.renderer.material.color == Color.yellow)
            {
                Debug.Log("Yellow");
            }
        }
	}
}
