using UnityEngine;
using System.Collections;

public class GUIFade : MonoBehaviour {

    private GUIText Word;
    public float m_fAlpha = 0f;

	void Start () 
    {
        Word = GetComponent<GUIText>();
	}
	
	void Update () 
    {
        Word.material.color = new Color(Word.material.color.r,
            Word.material.color.g,
            Word.material.color.b,
             m_fAlpha);

        if (m_fAlpha > 0)
            m_fAlpha -= 0.008f;
	}

    public void ResetAlpha()
    {
        m_fAlpha = 1;
    }
}
