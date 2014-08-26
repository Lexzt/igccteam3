using UnityEngine;
using System.Collections;

public class TestButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update(){
	
	}
	void OnGUI () {
		if ( GUI.Button( new Rect(100, 100, 100, 20), "Stop" ) )
		{
			Debug.Log("PressedButton");
			GameObject obj = GameObject.Find("Roulette");
			if(obj != null){
				obj.GetComponent<TestRotate>().rotSpeed = 0;
			}
		}
	}
}
