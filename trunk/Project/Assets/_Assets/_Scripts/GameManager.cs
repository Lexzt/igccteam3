﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    // Start game as Player
    public eTurn m_eTurn = eTurn.ePLAYER;

    // List Of object ot enable, on start
    public List<GameObject> ListOfObjToEnable;

    private EnemyManager EnemyManagerInstance;
    private PlayerManager PlayerManagerInstance;
    private bool EnableObjects = false;

    private GUIText GameEnd;
    private bool m_bGameEnd = false;
    private TransitionScript TransitionInstance;

	void Start () 
    {
        EnemyManagerInstance = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        PlayerManagerInstance = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        GameEnd = GameObject.Find("GameEnd").GetComponent<GUIText>();
	}
	
	void Update () 
    {
        if (EnableObjects == false)
        {
            if (EnemyManagerInstance.m_bCurrentlyHasEnemy && PlayerManagerInstance.m_bCurrentlyHasPlayer)
            {
                foreach (GameObject Obj in ListOfObjToEnable)
                {
                    Obj.SetActive(true);
                }

                EnemyManagerInstance.InitOnPerc();
                EnableObjects = true;
            }
        }

        if (EnemyManagerInstance.m_bCurrentlyHasEnemy && PlayerManagerInstance.m_bCurrentlyHasPlayer)
        {
            if (TransitionInstance == null)
            {
                TransitionInstance = GameObject.Find("Transition").GetComponent<TransitionScript>();
            }

            if (EnemyManagerInstance.CurrentEnemyObj.GetComponent<StatsScript>().m_fHealth <= 0)
            {
                GameEnd.gameObject.SetActive(true);
                GameEnd.text = "Player Wins";
                m_bGameEnd = true;
                TransitionInstance.GameEnd();
            }
            else if (PlayerManagerInstance.CurrentPlayerObj.GetComponent<StatsScript>().m_fHealth <= 0)
            {
                GameEnd.gameObject.SetActive(true);
                GameEnd.text = "Enemy Wins";
                m_bGameEnd = true;
                TransitionInstance.GameEnd();
            }
        }
	}

    public void SwapTurns()
    {
        Debug.Log("Swapping Turns");

        if (m_eTurn == eTurn.ePLAYER)
        {
            m_eTurn = eTurn.eENEMY;
            //GameObject.Find("PlayerManager").GetComponent<PlayerManager>().InitOnPerc();
        }
        else if (m_eTurn == eTurn.eENEMY)
        {
            m_eTurn = eTurn.ePLAYER;
            //GameObject.Find("EnemyManager").GetComponent<EnemyManager>().InitOnPerc();
        }
    }

    void OnGUI()
    {
        if (m_eTurn == eTurn.eENEMY)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, 25, 150, 50), "Current Turn: Enemy");
        }
        else
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, 25, 150, 50), "Current Turn: Player");
        }

        if (m_bGameEnd)
        {
            if (GUI.Button(new Rect((Screen.width * 0.9f) - 50, (Screen.height * 0.8f) - 50, 100, 100), "Restart Game"))
            {
                Application.LoadLevel("Start");
            }
        }
    }
}