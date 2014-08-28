using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private StatsScript StatsSystem;

	void Start () 
    {
        StatsSystem = GetComponent<StatsScript>();
	}
	
	void Update () 
    {
	
	}

    void OnGUI()
    {
        GUI.Label(new Rect(30, 30, 100, 100), "Player HP: " + StatsSystem.m_fHealth.ToString());
        GUI.Label(new Rect(30, 46, 100, 100), "Player Atk: " + StatsSystem.m_fAttack.ToString());
        GUI.Label(new Rect(30, 62, 100, 100), "Player Eva: " + StatsSystem.m_iEvasion.ToString());
    }
}
