using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    // List Of Objects for Players
    public List<GameObject> ListOfPlayers;
    public bool m_bCurrentlyHasPlayer = false;

    // Storage for Curreny enemy object, for name
    [HideInInspector]
    public GameObject CurrentPlayerObj = null;

    // Pie Controller Object
    private PieController PieControl;
    private PieInternController PieInternControl;

    public List<Texture> ListOfButtonTexture;
    public GUISkin skin;
    public GUISkin statsskin;
    private GUIStyle Style;

	void Awake () 
    {
        Style = new GUIStyle();
	}
	
	void Update () 
    {
	
	}

    void OnGUI()
    {
        GUI.skin = skin;
        if (m_bCurrentlyHasPlayer == false)
        {
            for (int i = 0; i < ListOfPlayers.Count; i++)
            {
                if (GUI.Button(new Rect((Screen.width / (ListOfPlayers.Count + 1)) * (i + 1) - 64, 300, 128, 128), ListOfButtonTexture[i], Style))
                {
                    // Create Enemy Object
                    GameObject PlayerObj = Instantiate(ListOfPlayers[i], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    PlayerObj.SetActive(true);

                    // Set Current Obj
                    CurrentPlayerObj = PlayerObj;

                    // Currently have Enemy, So ignore
                    m_bCurrentlyHasPlayer = true;

                    PlayerObj.GetComponent<Player>().skin = statsskin;
                }

                GUI.Label(new Rect((Screen.width / (ListOfPlayers.Count + 1)) * (i + 1) - 35, 410, 150, 50), "Health: " + ListOfPlayers[i].GetComponent<StatsScript>().m_fHealth.ToString());
                GUI.Label(new Rect((Screen.width / (ListOfPlayers.Count + 1)) * (i + 1) - 35, 430, 150, 50), "Attack: " + ListOfPlayers[i].GetComponent<StatsScript>().m_fAttack.ToString());
                GUI.Label(new Rect((Screen.width / (ListOfPlayers.Count + 1)) * (i + 1) - 35, 450, 150, 50), "Agility: " + ListOfPlayers[i].GetComponent<StatsScript>().m_iEvasion.ToString());
            }
        }
        //else
        //{
        //    GUI.Label(new Rect((Screen.width / 2) - 100, 65, 200, 50), "Current Player: " + CurrentPlayerObj.name);
        //}
    }

    public void DecreaseHealth(int tDamage)
    {
        if (CurrentPlayerObj != null)
        {
            CurrentPlayerObj.GetComponent<Player>().DecreaseHealth(tDamage);
        }
    }

    public void InitOnPerc()
    {
        // Change the Pie chart now
        PieControl = GameObject.Find("Pie Controller").GetComponent<PieController>();
        float PercentageDifference = (PieControl.ListOfStartingPercentages[0] * ((float)CurrentPlayerObj.GetComponent<StatsScript>().m_iEvasion / 100.0f));
        float Difference = PieControl.ListOfStartingPercentages[0] - PercentageDifference;

        List<float> PassableList = new List<float>();
        PassableList.Add(PercentageDifference);                                     // Miss
        PassableList.Add(PieControl.ListOfStartingPercentages[1] + Difference);     // Normal
        PassableList.Add(PieControl.ListOfStartingPercentages[2]);                  // Crit

        PieControl.Init(PassableList);
    }
}
