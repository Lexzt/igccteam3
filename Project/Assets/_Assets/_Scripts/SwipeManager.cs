using UnityEngine;
using System.Collections;

public class SwipeManager : MonoBehaviour
{

	/// 変数定義

	public float 		accelSpeedValue = 10.0f;
	public float		tapRangeDetection = 0.03f;

	private Vector2 	inputPosition;
	private Vector2 	inputPositionBuffer;
	private PieController pCtrler;
    private PieInternController pInternCtrler;
	private bool 	isClick = false;
	private Vector2 objectPos;

    // -------------------------- Keith ------------------------ //
    private Vector2 m_vFirstPosition;
    private Vector2 m_vSwipeDirection;
    private bool m_bSwiped = false;
    private bool m_bStopRotate = false;

    private Pointer Point;

	void Start()
	{
		isClick = false;
		pCtrler = GetComponent<PieController>();
        pInternCtrler = GameObject.Find("Pie Inner Circle").GetComponent<PieInternController>();
        Point = GameObject.Find("Pointer").GetComponent<Pointer>();
	}
	// Update is called once per frame
	void Update () {
        #region TouchBegin
        if (Input.GetMouseButtonDown(0))
        {
            m_vFirstPosition = Input.mousePosition;
        }
        #endregion
        #region TouchMove
        if ((Input.GetMouseButtonUp(0) || Input.GetMouseButton(0)) && m_bStopRotate == false)
        {
            m_vSwipeDirection = (Vector2)Input.mousePosition - m_vFirstPosition;
            eDirection Direction = HelperScript.GetDirection(m_vSwipeDirection.normalized);

            if (Direction == eDirection.eUP)
            {
                //Debug.Log("Up!");
            }
            else if (Direction == eDirection.eDOWN)
            {
                //Debug.Log("Down!");
                m_bSwiped = true;
            }
        }
        #endregion
        #region TouchEnd
        if (Input.GetMouseButtonUp(0) && m_bSwiped == true)
        {
            if (m_vFirstPosition == (Vector2)Input.mousePosition)
                m_bStopRotate = true;

            if (m_bStopRotate == true)
            {
                if (GameObject.Find("EnemyManager").GetComponent<EnemyManager>().m_bCurrentlyHasEnemy &&
                    GameObject.Find("PlayerManager").GetComponent<PlayerManager>().m_bCurrentlyHasPlayer)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, 100) == false)
                    {
                        pCtrler.StopRotate();
                        Point.CheckRay();
                        pInternCtrler.m_bStopRotation = true;
                    }
                }
            }
            else
            {
                if (pCtrler.m_iSpinNo < pCtrler.m_iLimit)
                {
                    pCtrler.IncreaseSpeed(accelSpeedValue);
                    pInternCtrler.rotSpeed += accelSpeedValue;
                }
            }
            m_bStopRotate = false;
        }
        #endregion

        // Specifically for touch
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                Touch tTouch = Input.GetTouch(0);

                if (tTouch.phase == TouchPhase.Began)
                {
                    Debug.Log("Down");

                    m_vFirstPosition = Input.mousePosition;
                }
                else if (tTouch.phase == TouchPhase.Moved)
                {
                    //Debug.Log("Moved");

                    m_vSwipeDirection = (Vector2)Input.mousePosition - m_vFirstPosition;
                    eDirection Direction = HelperScript.GetDirection(m_vSwipeDirection.normalized);

                    Debug.Log(Direction);

                    if (Direction == eDirection.eUP)
                    {
                        //Debug.Log("Up!");
                    }
                    else if (Direction == eDirection.eDOWN)
                    {
                        //Debug.Log("Down!");
                        m_bSwiped = true;
                    }
                }
                else if (tTouch.phase == TouchPhase.Ended)
                {
                    Debug.Log("Up");

                    pCtrler.IncreaseSpeed(accelSpeedValue);
                }
            }
        }
	}

    //void OnMouseDown()
    //{
    //    onClickObject();
    //}
    //void OnMouseUp()
    //{
    //    onClickEnd();
    //}


    //private void onClickObject(){
    //    GameObject selectGameObject;// クリックされたゲームオブジェクトへの参照.

    //    // レイを取得.
    //    Ray clkRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit rh;
    //    //当たり判定
    //    if (Physics.Raycast(clkRay, out rh, 500)){
    //        selectGameObject = rh.collider.gameObject;// クリックされたゲームオブジェクトを格納.
    //        if(selectGameObject.name == "Cylinder_1_Part(Clone)")
    //        {
    //            isClick = true;
    //        }
    //        //クリックした際のポジションを取得
    //        inputPosition = selectGameObject.transform.position;
    //        inputPositionBuffer = Input.mousePosition;
    //        objectPos = Camera.main.WorldToScreenPoint(pCtrler.gameObject.transform.position);
    //    }
    //    else{
    //        selectGameObject = null;// 何もクリックされていない場合はnullを代入
    //    }
    //}
	/**
	 * @クリックされたときの処理
	*/
    //private void onClickEnd () {

    //    if (!isClick) return;

    //    // ドラッグ距離を判定
    //    //クリックした際のポジションを取得
    //    inputPosition = inputPositionBuffer;
    //    inputPositionBuffer = Input.mousePosition;
    //    // 2つのベクトルの角度を比較して右回転か左回転かを判断する
    //    float angle1 = Mathf.Atan2(inputPositionBuffer.x-objectPos.x, inputPositionBuffer.y	-objectPos.y);
    //    float angle2 = Mathf.Atan2(inputPosition.x		-objectPos.x, inputPosition.y		-objectPos.y);
    //    float subAngle = angle1 - angle2;// プラスなら右回転
    //    float rotScale = 0;	// value of Swipe
    //    rotScale = Vector2.Angle(inputPositionBuffer-objectPos, inputPosition-objectPos);
    //    if(subAngle < 0){
    //        rotScale *= -1.0f;
    //    }
    //    //Calc Swipe
    //    Vector3 subVec =  inputPositionBuffer - inputPosition;

    //    if(Mathf.Abs(rotScale) < tapRangeDetection)
    //    {
    //        pCtrler.StopRotate();
    //    }else{
    //        //print (subVec);
    //        if( subVec.y > 0)
    //        {
    //            pCtrler.IncreaseSpeed (accelSpeedValue);
    //        }
    //    }
    //    isClick = false;
    //}
}