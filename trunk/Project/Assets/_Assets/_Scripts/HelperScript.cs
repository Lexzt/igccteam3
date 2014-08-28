using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ePIETYPE
{
    eNONE = -1,
    eFAIL,
    eATTACK,
    eCRITATTACK,
}

public enum eDirection
{
    eNONE = -1,
    eLEFT,
    eRIGHT,
    eUP,
    eDOWN,
};

public class HelperScript : MonoBehaviour 
{
    public static eDirection GetDirection(Vector2 DirectionNormalized)
    {
        // Return Value
        eDirection ReturnVal = eDirection.eNONE;

        // Abs The Check value for Direction compare
        Vector2 TempVec = new Vector2(Mathf.Abs(DirectionNormalized.x), Mathf.Abs(DirectionNormalized.y));
        if ((TempVec.x > TempVec.y) && DirectionNormalized.x > 0)
        {
            ReturnVal = eDirection.eRIGHT;
        }
        else if ((TempVec.y > TempVec.x) && DirectionNormalized.y > 0)
        {
            ReturnVal = eDirection.eUP;
        }
        else if ((TempVec.x > TempVec.y) && DirectionNormalized.x < 0)
        {
            ReturnVal = eDirection.eLEFT;
        }
        else if ((TempVec.y > TempVec.x) && DirectionNormalized.y < 0)
        {
            ReturnVal = eDirection.eDOWN;
        }

        return ReturnVal;
    }

    public static ePIETYPE GetTileType(int iAmt, List<Pie> ListOfPie)
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
                if (ListOfPie[i].m_Material.name == "Mat1")
                {
                    return ePIETYPE.eFAIL;
                }
                else if (ListOfPie[i].m_Material.name == "Mat2")
                {
                    return ePIETYPE.eATTACK;
                }
                else if (ListOfPie[i].m_Material.name == "Mat3")
                {
                    return ePIETYPE.eCRITATTACK;
                }
                else
                {
                    return ePIETYPE.eNONE;
                }
            }
        }
        return ePIETYPE.eNONE;
    }

    public static Material GetMaterial(int iAmt, List<Pie> ListOfPie)
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

    public static IEnumerator Eez(GameObject Obj, Vector3 startLoc, Vector3 endLoc, float EezTime)
    {
        float t = 0;

        Obj.GetComponent<HalfScript>().m_bEezing = true;

        while (t / EezTime < 1)
        {
            t += Time.deltaTime;
            Obj.transform.position = Vector3.Lerp(startLoc, endLoc, t / EezTime);
            yield return null;
        }

        Obj.GetComponent<HalfScript>().m_bEezing = false;
        Obj.GetComponent<HalfScript>().m_bChangeOnce = !Obj.GetComponent<HalfScript>().m_bChangeOnce;
    }

    public static IEnumerator Wait(GameObject Obj, float tTime)
    {
        Obj.GetComponent<HalfScript>().m_bStartWait = true;
        yield return new WaitForSeconds(tTime);
        Obj.GetComponent<HalfScript>().m_bWait = true;
    }
}
