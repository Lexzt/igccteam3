﻿using UnityEngine;
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
	public float	 rotSpeedLimit = 50;
    // Current Amt of Pie values
    private float m_fCurrentAmtOfPie    = 0;
    private int m_iMaxAmtOfPie          = 360;

    // Amt of Pies
    public int m_iAmtOfPies;

    // Private Color array for randoming Colors

    public GameObject PrefabObject;

    // Value of One slide
    private int ValueOfSlice = 2;

    // Pie Object
    private GameObject ParentObj;
	
	private float rotSpeed = 0;	// rotation speed

	void Start () 
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
                Value = m_iMaxAmtOfPie - m_fCurrentAmtOfPie;
            }

            ListOfPieShapes.Add(new Pie((int)Value, ListOfMaterial[i]));
        }

        ParentObj = new GameObject("Parent");

        for (int i = 0; i < 360 / ValueOfSlice; i++)
        {
            GameObject Inst = Instantiate(PrefabObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Inst.transform.Rotate(new Vector3(0, 0, (int)(i * ValueOfSlice)));
            Inst.transform.parent = ParentObj.transform;

            Inst.renderer.material = GetMaterial((int)(i * ValueOfSlice), ListOfPieShapes);

            PieScript ScriptInst = Inst.AddComponent<PieScript>();
            ScriptInst.m_ePieType = HelperScript.GetTileType(Inst.renderer.material.name);
        }
	}

    Material GetMaterial(int iAmt, List<Pie> ListOfPie)
    {
        for (int i = 0; i < ListOfPie.Count; i++)
        {
            int iBeforeHand = 0;
            for (int j = 0; j <= i; j++)
            {
                iBeforeHand += ListOfPie[j].m_iValue;
            }

            if (iAmt < iBeforeHand)
            {
                return ListOfPie[i].m_Material;
            }
        }
        return null;
    }

    void Update() 
    {
		ParentObj.transform.Rotate(Vector3.back, rotSpeed);
        //ParentObj.transform.Rotate(Vector3.back, 30 * 5 * Time.deltaTime);

        if (Input.GetKey(KeyCode.B))
        {
            AddValue(ListOfPieShapes[0].m_Material, 4);     // Miss Attack
            SubValue(ListOfPieShapes[1].m_Material, 6);     // Normal Attack
            AddValue(ListOfPieShapes[2].m_Material, 2);     // Critical Attack

            Material tMaterial = null;

            foreach (Transform child in ParentObj.transform)
            {
                //Debug.Log(child.localEulerAngles.z);
                tMaterial = GetMaterial((int)child.localEulerAngles.z, ListOfPieShapes);

                if (tMaterial != child.renderer.material)
                {
                    child.renderer.material = tMaterial;
                    child.GetComponent<PieScript>().m_ePieType = HelperScript.GetTileType(child.renderer.material.name);
                }
            }
        }
	}

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
                }
            }
        }
    }

//==============================
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

        Swiped();
	}
	public void StopRotate()
	{
		rotSpeed = 0;
	}
//==============================

    public void Swiped()
    {
        AddValue(ListOfPieShapes[0].m_Material, 4);     // Miss Attack
        SubValue(ListOfPieShapes[1].m_Material, 6);     // Normal Attack
        AddValue(ListOfPieShapes[2].m_Material, 2);     // Critical Attack

        Material tMaterial = null;

        foreach (Transform child in ParentObj.transform)
        {
            //Debug.Log(child.localEulerAngles.z);
            tMaterial = GetMaterial((int)child.localEulerAngles.z, ListOfPieShapes);

            if (tMaterial != child.renderer.material)
            {
                child.renderer.material = tMaterial;
                child.GetComponent<PieScript>().m_ePieType = HelperScript.GetTileType(child.renderer.material.name);
            }
        }
    }

}
