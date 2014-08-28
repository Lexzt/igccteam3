using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

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
        GUI.Label(new Rect(Screen.width - 140, 30, 100, 100), "Enemy HP: " + StatsSystem.m_fHealth.ToString());
        GUI.Label(new Rect(Screen.width - 140, 46, 100, 100), "Enemy Atk: " + StatsSystem.m_fAttack.ToString());
        GUI.Label(new Rect(Screen.width - 140, 62, 100, 100), "Enemy Eva: " + StatsSystem.m_iEvasion.ToString());
    }

    public void DecreaseHealth(int tHealth)
    {
        StatsSystem.m_fHealth -= tHealth;
        if (StatsSystem.m_fHealth < 0)
            StatsSystem.m_fHealth = 0;
    }
}
