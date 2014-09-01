using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    // List Of Object for Enemies
    public List<GameObject> ListOfEnemyies;
    public bool m_bCurrentlyHasEnemy = false;

    // Storage for Curreny enemy object, for name
    [HideInInspector]
    public GameObject CurrentEnemyObj = null;

    // Pie Controller Object
    private PieController PieControl;
    private PieInternController PieInternControl;

	void Start () 
    {
        //PieInternControl = GameObject.Find("Pie Inner Circle").GetComponent<PieInternController>();
    }
	
	void Update () 
    {

	}

    void OnGUI()
    {
        if (m_bCurrentlyHasEnemy == false)
        {
            for (int i = 0; i < ListOfEnemyies.Count; i++)
            {
                if (GUI.Button(new Rect((Screen.width / (ListOfEnemyies.Count + 1)) * (i + 1) - 50, 50, 100, 50), ListOfEnemyies[i].name))
                {
                    // Create Enemy Object
                    GameObject EnemyObj = Instantiate(ListOfEnemyies[i], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    EnemyObj.SetActive(true);

                    // Set Current Obj
                    CurrentEnemyObj = EnemyObj;

                    // Currently have Enemy, So ignore
                    m_bCurrentlyHasEnemy = true;
                }

                GUI.Label(new Rect((Screen.width / (ListOfEnemyies.Count + 1)) * (i + 1) - 35, 110, 100, 50), "Health: " + ListOfEnemyies[i].GetComponent<StatsScript>().m_fHealth.ToString());
                GUI.Label(new Rect((Screen.width / (ListOfEnemyies.Count + 1)) * (i + 1) - 35, 130, 100, 50), "Attack: " + ListOfEnemyies[i].GetComponent<StatsScript>().m_fAttack.ToString());
                GUI.Label(new Rect((Screen.width / (ListOfEnemyies.Count + 1)) * (i + 1) - 35, 150, 100, 50), "Evasion: " + ListOfEnemyies[i].GetComponent<StatsScript>().m_iEvasion.ToString());
            }
        }
        else
        {
            GUI.Label(new Rect((Screen.width / 2) - 100, 50, 200, 50), "Current Enemy: " + CurrentEnemyObj.name);
        }
    }

    public void DecreaseHealth(int tDamage)
    {
        if (CurrentEnemyObj != null)
        {
            CurrentEnemyObj.GetComponent<Enemy>().DecreaseHealth(tDamage);
        }
    }

    public void ResetManager()
    {
        m_bCurrentlyHasEnemy = false;
        CurrentEnemyObj = null;
    }

    public void InitOnPerc()
    {
        PieControl = GameObject.Find("Pie Controller").GetComponent<PieController>();

        // Change the Pie chart now
        float PercentageDifference = (PieControl.ListOfStartingPercentages[0] * ((float)CurrentEnemyObj.GetComponent<StatsScript>().m_iEvasion / 100.0f));
        float Difference = PieControl.ListOfStartingPercentages[0] - PercentageDifference;

        List<float> PassableList = new List<float>();
        PassableList.Add(PercentageDifference);                                     // Miss
        PassableList.Add(PieControl.ListOfStartingPercentages[1] + Difference);     // Normal
        PassableList.Add(PieControl.ListOfStartingPercentages[2]);                  // Crit

        PieControl.Init(PassableList);
    }
}
