using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

    public string Text;
    public GUISkin Skin;

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}

    void OnGUI()
    {
        GUI.skin = Skin;
        Debug.Log(GUI.skin.name);
        //GUILayout.Button(new Rect(Screen.width * 0.8f, Screen.height * 0.1f, 1000, 200), Text);
        GUILayout.Label(Text); 
    }
}
