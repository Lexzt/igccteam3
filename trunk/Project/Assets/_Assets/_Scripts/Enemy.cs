using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

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
        GUI.Label(new Rect(Screen.width - 160, 30, 200, 100), "Enemy HP: " + StatsSystem.m_fHealth.ToString());
        GUI.Label(new Rect(Screen.width - 160, 46, 200, 100), "Enemy Atk: " + StatsSystem.m_fAttack.ToString());
        GUI.Label(new Rect(Screen.width - 160, 62, 200, 100), "Enemy Eva: " + StatsSystem.m_iEvasion.ToString());
    }

    public void DecreaseHealth(int tHealth)
    {
        StatsSystem.m_fHealth -= tHealth;
        if (StatsSystem.m_fHealth < 0)
            StatsSystem.m_fHealth = 0;
    }
}
