using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    // Start game as Player
    public eTurn m_eTurn = eTurn.ePLAYER;

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
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
    }
}
