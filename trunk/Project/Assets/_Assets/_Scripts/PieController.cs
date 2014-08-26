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

    // Value of One slide
    private int ValueOfSlice = 2;

    // Pie Object
    private GameObject ParentObj;

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
        m_iAmtOfPies = Random.Range(3, 5);

        for (int i = 0; i < m_iAmtOfPies; i++)
        {
            int Value;
            if (i + 1 != m_iAmtOfPies)
            {
                Value = Random.Range(ValueOfSlice, m_iMaxAmtOfPie - m_iCurrentAmtOfPie);

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

        ParentObj = new GameObject("Parent");

        for (int i = 0; i < 360 / ValueOfSlice; i++)
        {
            GameObject Inst = Instantiate(PrefabObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Inst.transform.Rotate(new Vector3(0, 0, (int)(i * ValueOfSlice)));
            Inst.transform.parent = ParentObj.transform;

            Inst.renderer.material.color = GetColor((int)(i * ValueOfSlice), ListOfPieShapes);
        }
	}

    Color GetColor(int iAmt, List<Pie> ListOfPie)
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
                return ListOfPie[i].m_cColor;
            }
        }

        return Color.white;
    }

    void Update() 
    {
        ParentObj.transform.Rotate(Vector3.back, 30 * 2 * Time.deltaTime);
	}
}
