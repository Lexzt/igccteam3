using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {

    private GUITexture StartButton;

	void Start () 
    {
        StartButton = GetComponent<GUITexture>();
	}
	
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (StartButton.HitTest(Input.mousePosition))
            {
                Application.LoadLevel("Base");
            }
        }
	}
}
