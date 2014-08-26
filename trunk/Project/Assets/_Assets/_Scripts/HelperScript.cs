using UnityEngine;
using System.Collections;

public enum ePIETYPE
{
    eNONE = -1,
    eFAIL,
    eATTACK,
    eCRITATTACK,
}

public class HelperScript : MonoBehaviour 
{
    public static ePIETYPE GetTileType(string MatName)
    {
        if (MatName == "Mat1 (Instance)")
        {
            return ePIETYPE.eFAIL;
        }
        else if (MatName == "Mat2 (Instance)")
        {
            return ePIETYPE.eATTACK;
        }
        else if (MatName == "Mat3 (Instance)")
        {
            return ePIETYPE.eCRITATTACK;
        }
        else
        {
            return ePIETYPE.eNONE;
        }
    }
}
