using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private StatsScript StatsSystem;
    public GUISkin skin;

	void Start () 
    {
        StatsSystem = GetComponent<StatsScript>();
	}
	
	void Update () 
    {
	
	}

    void OnGUI()
    {
        GUI.skin = skin;
        GUI.Label(new Rect(30, 30, 200, 100), "Player HP: " + StatsSystem.m_fHealth.ToString());
        GUI.Label(new Rect(30, 46, 200, 100), "Player Atk: " + StatsSystem.m_fAttack.ToString());
        GUI.Label(new Rect(30, 62, 200, 100), "Player Eva: " + StatsSystem.m_iEvasion.ToString());
    }

    public void DecreaseHealth(int tHealth)
    {
        StatsSystem.m_fHealth -= tHealth;
        if (StatsSystem.m_fHealth < 0)
            StatsSystem.m_fHealth = 0;
    }
}
