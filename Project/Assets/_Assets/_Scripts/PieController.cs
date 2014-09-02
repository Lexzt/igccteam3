using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Pie
{
    public int m_iValue;    // Value is < 360
    public Material m_Material;  // Color of the Pie

    public Pie(int iValue,Material mMaterial)
    {
        m_iValue = iValue;
        m_Material = mMaterial;
    }
}


public class PieController : MonoBehaviour 
{
    // Main Amt of Pie shapes
    public List<Pie> ListOfPieShapes;
    public List<float> ListOfStartingPercentages;
    public List<Material> ListOfMaterial;

	// Speed Limit of Rotation
	public float	rotSpeedLimit = 50;
	public float	rotDecleaseRait = 0.99f;	// set 0 ~ 1
    // Current Amt of Pie values
    private float m_fCurrentAmtOfPie    = 0;
    private int m_iMaxAmtOfPie          = 360;

    // Amt of Pies
    public int m_iAmtOfPies;

    // Prefab for drawing
    public GameObject PrefabObject;
    public GameObject BarObject;

    // Value of One slide
    private int ValueOfSlice = 2;

    // Pie Object
    private GameObject ParentObj;
    private GameObject BarParentObj;
	
	public float rotSpeed = 0;	// rotation speed

    // Debug - How Many times spinned
    public int m_iSpinNo = 0;

    public int m_iLimit = 6;

    // Game Manager Instance
    private GameManager GameManagerInstance;

	void Awake () 
    {
        Init();
        GameManagerInstance = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    void Update() 
    {
		ParentObj.transform.Rotate(Vector3.back, rotSpeed);
        //rotSpeed *= rotDecleaseRait; // decrease Speed over time
		if (Mathf.Abs(rotSpeed) < 0.001f) {
			rotSpeed = 0;
		}
        if (Input.GetKeyDown(KeyCode.B) && m_iSpinNo < m_iLimit)
        {
            float AtkPercentage = (2.0f / 100.0f) * 360.0f;
            float CritAtkPercentage = (2.0f / 100.0f) * 360.0f;
            
            if ((int)AtkPercentage % 2 == 1)
            {
                AtkPercentage -= 1;
            }

            if ((int)CritAtkPercentage % 2 == 1)
            {
                CritAtkPercentage -= 1;
            }
            
            SubValue(ListOfPieShapes[1].m_Material, (int)(AtkPercentage));     // Normal Attack
            AddValue(ListOfPieShapes[2].m_Material, (int)(CritAtkPercentage));     // Critical Attack

            Material tMaterial = null;
            foreach (Transform child in ParentObj.transform)
            {
                tMaterial = HelperScript.GetMaterial((int)child.localEulerAngles.z, ListOfPieShapes);

                if (tMaterial != child.renderer.material)
                {
                    child.renderer.material = tMaterial;
                    child.GetComponent<PieScript>().m_ePieType = HelperScript.GetTileType((int)child.transform.localEulerAngles.z, ListOfPieShapes);
                }
            }

            if (GameManagerInstance.m_eTurn == eTurn.ePLAYER)
            {
                BarParentObj.transform.GetChild((m_iLimit - 1) - m_iSpinNo++).FindChild("Bar").renderer.material.color = Color.cyan;
            }
            else if (GameManagerInstance.m_eTurn == eTurn.eENEMY)
            {
                BarParentObj.transform.GetChild((m_iLimit - 1) - m_iSpinNo++).FindChild("Bar").renderer.material.color = new Color(1.0f, 0.1f, 0.1f);
            }
        }

        if(Input.GetKey(KeyCode.N))
        {
            ResetPerc();
        }
	}

#region AddSubSet
    // Add Value to Specific Pie
    void AddValue(Material tMaterial, int iAmt)
    {
        foreach (Pie tPie in ListOfPieShapes)
        {
            if (tPie.m_Material == tMaterial)
            {
                if (tPie.m_iValue > 0)
                {
                    tPie.m_iValue += iAmt;
                    if (tPie.m_iValue < 0)
                    {
                        tPie.m_iValue = 0;
                    }
                }
                else
                {
                    tPie.m_iValue = 0;
                }
            }
        }
    }

    // Minus Value to Specific Pie
    void SubValue(Material tMaterial, int iAmt)
    {
        foreach (Pie tPie in ListOfPieShapes)
        {
            if (tPie.m_Material == tMaterial)
            {
                if (tPie.m_iValue > 0)
                {
                    tPie.m_iValue -= iAmt;
                    if (tPie.m_iValue < 0)
                    {
                        tPie.m_iValue = 0;
                    }
                }
                else
                {
                    tPie.m_iValue = 0;
                }
            }
        }
    }

    // Set Value to Specific Pie
    void SetValue(Material tMaterial, int iAmt)
    {
        foreach (Pie tPie in ListOfPieShapes)
        {
            if (tPie.m_Material == tMaterial)
            {
                tPie.m_iValue = iAmt;
            }
        }
    }
#endregion
	/// <summary>
	/// Control Speed Functions.
	/// </summary>
	public void IncreaseSpeed (float speed, bool dirRight = true)
	{
		rotSpeed += speed;
		float dir = (rotSpeed >= 0) ? 1 : -1;

		if(Mathf.Abs(rotSpeed) > rotSpeedLimit)
		{
			rotSpeed = rotSpeedLimit;
			rotSpeed *= dir;
		}

        //Debug.Log(rotSpeed);
        Swiped();
	}
	public void StopRotate()
	{
		rotSpeed = 0;

        //ResetPerc();
	}

	/// call On GameStart.
	public void StartRoulette(float startSpeed)
	{
		rotSpeed = startSpeed;
	}

    // On Swiped
    void Swiped()
    {
        if (m_iSpinNo < m_iLimit)
        {
            float AtkPercentage = (2.0f / 100.0f) * 360.0f;
            float CritAtkPercentage = (2.0f / 100.0f) * 360.0f;

            if ((int)AtkPercentage % 2 == 1)
            {
                AtkPercentage -= 1;
            }

            if ((int)CritAtkPercentage % 2 == 1)
            {
                CritAtkPercentage -= 1;
            }

            SubValue(ListOfPieShapes[1].m_Material, (int)(AtkPercentage));          // Normal Attack
            AddValue(ListOfPieShapes[2].m_Material, (int)(CritAtkPercentage));      // Critical Attack

            Material tMaterial = null;
            foreach (Transform child in ParentObj.transform)
            {
                tMaterial = HelperScript.GetMaterial((int)child.localEulerAngles.z, ListOfPieShapes);

                if (child.tag == "Pie")
                {
                    if (tMaterial != child.renderer.material)
                    {
                        child.renderer.material = tMaterial;
                        child.GetComponent<PieScript>().m_ePieType = HelperScript.GetTileType((int)child.transform.localEulerAngles.z, ListOfPieShapes);
                    }
                }
            }

            if (GameManagerInstance.m_eTurn == eTurn.ePLAYER)
            {
                BarParentObj.transform.GetChild((m_iLimit - 1) - m_iSpinNo++).FindChild("Bar").renderer.material.color = Color.cyan;
            }
            else if (GameManagerInstance.m_eTurn == eTurn.eENEMY)
            {
                BarParentObj.transform.GetChild((m_iLimit - 1) - m_iSpinNo++).FindChild("Bar").renderer.material.color = new Color(1.0f, 0.35f, 0.35f);
            }
        }
    }

    // Reset Pie
    public void ResetPerc()
    {
        float FailPercentage    =   (ListOfStartingPercentages[0] / 100.0f) * 360.0f;
        float AtkPercentage     =   (ListOfStartingPercentages[1] / 100.0f) * 360.0f;
        float CritAtkPercentage =   (ListOfStartingPercentages[2] / 100.0f) * 360.0f;
        
        SetValue(ListOfPieShapes[0].m_Material, (int)(FailPercentage));     // Miss Attack
        SetValue(ListOfPieShapes[1].m_Material, (int)(AtkPercentage));      // Normal Attack
        SetValue(ListOfPieShapes[2].m_Material, (int)(CritAtkPercentage));  // Critical Attack

        Material tMaterial = null;
        foreach (Transform child in ParentObj.transform)
        {
            tMaterial = HelperScript.GetMaterial((int)child.localEulerAngles.z, ListOfPieShapes);

            if (tMaterial != child.renderer.material)
            {
                child.renderer.material = tMaterial;
                child.GetComponent<PieScript>().m_ePieType = HelperScript.GetTileType((int)child.transform.localEulerAngles.z, ListOfPieShapes);
            }
        }

        m_iSpinNo = 0;
        foreach (Transform child in BarParentObj.transform)
        {
            child.FindChild("Bar").renderer.material.color = Color.white;
            //child.renderer.material.color = Color.white;
        }

        StartRoulette(1);
    }
    public void ResetPercByEnemy()
    {
        float PercentageDifference = (ListOfStartingPercentages[0] * ((float)GameObject.Find("EnemyManager").GetComponent<EnemyManager>().CurrentEnemyObj.GetComponent<StatsScript>().m_iEvasion / 100.0f));
        float Difference = ListOfStartingPercentages[0] - PercentageDifference;

        List<float> PassableList = new List<float>();
        PassableList.Add(PercentageDifference);                                     // Miss
        PassableList.Add(ListOfStartingPercentages[1] + Difference);     // Normal
        PassableList.Add(ListOfStartingPercentages[2]);                  // Crit

        float FailPercentage = (PassableList[0] / 100.0f) * 360.0f;
        float AtkPercentage = (PassableList[1] / 100.0f) * 360.0f;
        float CritAtkPercentage = (PassableList[2] / 100.0f) * 360.0f;

        SetValue(ListOfPieShapes[0].m_Material, (int)(FailPercentage));     // Miss Attack
        SetValue(ListOfPieShapes[1].m_Material, (int)(AtkPercentage));      // Normal Attack
        SetValue(ListOfPieShapes[2].m_Material, (int)(CritAtkPercentage));  // Critical Attack

        Material tMaterial = null;
        foreach (Transform child in ParentObj.transform)
        {
            tMaterial = HelperScript.GetMaterial((int)child.localEulerAngles.z, ListOfPieShapes);

            if (tMaterial != child.renderer.material)
            {
                child.renderer.material = tMaterial;
                child.GetComponent<PieScript>().m_ePieType = HelperScript.GetTileType((int)child.transform.localEulerAngles.z, ListOfPieShapes);
            }
        }

        m_iSpinNo = 0;
        foreach (Transform child in BarParentObj.transform)
        {
            child.FindChild("Bar").renderer.material.color = Color.white;
            //child.renderer.material.color = Color.white;
        }

        StartRoulette(1);
    }

    // Initalize Variables
    public void Init()
    {
        for (int i = 0; i < m_iAmtOfPies; i++)
        {
            float Value;
            if (i + 1 != m_iAmtOfPies)
            {
                //Value = Random.Range(ValueOfSlice, m_iMaxAmtOfPie - m_iCurrentAmtOfPie);
                Value = (ListOfStartingPercentages[i] / 100.0f) * m_iMaxAmtOfPie;

                // If Number is odd
                if (Value % 2 == 1)
                {
                    Value -= 1;
                }
                m_fCurrentAmtOfPie += Value;
            }
            else
            {
                Value = m_iMaxAmtOfPie - (int)m_fCurrentAmtOfPie;
            }
            ListOfPieShapes.Add(new Pie((int)Value, ListOfMaterial[i]));
        }

        ParentObj = new GameObject("Parent");
        for (int i = 0; i < 360 / ValueOfSlice; i++)
        {
            GameObject Inst = Instantiate(PrefabObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Inst.transform.Rotate(new Vector3(0, 0, (int)(i * ValueOfSlice)));
            Inst.transform.parent = ParentObj.transform;

            Inst.renderer.material = HelperScript.GetMaterial((int)(i * ValueOfSlice), ListOfPieShapes);

            PieScript ScriptInst = Inst.AddComponent<PieScript>();
            ScriptInst.m_ePieType = HelperScript.GetTileType((int)(i * ValueOfSlice), ListOfPieShapes);
        }

        BarParentObj = new GameObject("BarParent");
        for (int i = 0; i < m_iLimit; i++)
        {
            GameObject Inst = Instantiate(BarObject, new Vector3(0, 0, BarObject.transform.position.z), Quaternion.identity) as GameObject;
            Inst.transform.Rotate(new Vector3(0, 0, (int)(i * (360 / m_iLimit))));
            Inst.transform.parent = BarParentObj.transform;
        }

        // StartGame;
        StartRoulette(1);
    }
    public void Init(List<float> ListOfStartingPerc)
    {
        ListOfPieShapes.Clear();
        
        for (int i = 0; i < ListOfStartingPerc.Count; i++)
        {
            float Value;
            Value = (ListOfStartingPerc[i] / 100.0f) * m_iMaxAmtOfPie;

            // If Number is odd
            if ((int)Value % 2 == 1)
            {
                Value += 1;
            }

            ListOfPieShapes.Add(new Pie((int)Value, ListOfMaterial[i]));
        }

        Material tMaterial = null;
        foreach (Transform child in ParentObj.transform)
        {
            tMaterial = HelperScript.GetMaterial((int)child.localEulerAngles.z, ListOfPieShapes);

            if (child.tag == "Pie")
            {
                if (tMaterial != child.renderer.material)
                {
                    child.renderer.material = tMaterial;
                    child.GetComponent<PieScript>().m_ePieType = HelperScript.GetTileType((int)child.transform.localEulerAngles.z, ListOfPieShapes);
                }
            }
        }

        m_iSpinNo = 0;
        foreach (Transform child in BarParentObj.transform)
        {
            child.FindChild("Bar").renderer.material.color = Color.white;
            //child.renderer.material.color = Color.white;
        }

        // StartGame;
        StartRoulette(1);
    }
}
