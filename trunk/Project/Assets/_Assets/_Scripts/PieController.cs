using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Pie
{
    public int m_iValue;    // Value is < 360
    public Color m_cColor;  // Color of the Pie

    public Pie(int iValue,Color cColor)
    {
        m_iValue = iValue;
        m_cColor = cColor;
    }
}

public class PieController : MonoBehaviour 
{
    // Main Amt of Pie shapes
    public List<Pie> ListOfPieShapes;

    // Current Amt of Pie values
    private int m_iCurrentAmtOfPie  = 0;
    private int m_iMaxAmtOfPie      = 360;

    // Debug - Temp Amt of Pies
    private int m_iAmtOfPies;

    // Private Color array for randoming Colors
    private List<Color> ListOfColor;

    public GameObject PrefabObject;

	void Start () 
    {
        // Randoming Colors
        ListOfColor = new List<Color>();
        ListOfColor.Add(Color.red);
        ListOfColor.Add(Color.blue);
        ListOfColor.Add(Color.black);
        ListOfColor.Add(Color.cyan);
        ListOfColor.Add(Color.gray);
        ListOfColor.Add(Color.green);
        ListOfColor.Add(Color.magenta);
        ListOfColor.Add(Color.yellow);

        // Randoming amt of "Pies"
        m_iAmtOfPies = Random.Range(2, 5);

        for (int i = 0; i < m_iAmtOfPies; i++)
        {
            int Value;
            if (i + 1 != m_iAmtOfPies)
            {
                Value = Random.Range(0, m_iMaxAmtOfPie - m_iCurrentAmtOfPie);

                // If Number is odd
                if (Value % 2 == 1)
                {
                    Value -= 1;
                }

                m_iCurrentAmtOfPie += Value;
            }
            else
            {
                Value = m_iMaxAmtOfPie - m_iCurrentAmtOfPie;
            }

            int RandomColorNo = Random.Range(0,ListOfColor.Count);
            ListOfPieShapes.Add(new Pie(Value, ListOfColor[RandomColorNo]));
            ListOfColor.Remove(ListOfColor[RandomColorNo]);
        }

        GameObject ParentObj = new GameObject("Parent");

        for (int i = 0; i < 180; i++)
        {
            //Quaternion Angle = new Quaternion(0,0,Mathf.Deg2Rad * i * 2,0);
            GameObject Inst = Instantiate(PrefabObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Inst.transform.Rotate(new Vector3(0, 0, i * 2));
            Inst.transform.parent = ParentObj.transform;
        }

        foreach (Pie tPie in ListOfPieShapes)
        {
            for (int i = 0; i < tPie.m_iValue; i += 2)
            {

            }
        }
	}

    void Update() 
    {
	
	}
}
