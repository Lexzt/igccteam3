using UnityEngine;
using System.Collections;

public class SwipeManager : MonoBehaviour
{

	/// 変数定義

	public float 			accelSpeedValue = 10.0f;
	public float			tapRangeDetection = 0.03f;
	/// private
	private Vector2 		inputPositionStart;
	private PieController 	pCtrler;
	private Vector2 		objectPos;
	private GameObject 		selectGameObject;// クリックされたゲームオブジェクトへの参照.
	void Start()
	{
		pCtrler = GetComponent<PieController>();
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			onClickObject();
		}
		if(Input.GetMouseButtonUp(0)){
			onReleased();
		}
	}

	private void onClickObject(){
		// temp firstPos
		inputPositionStart = Input.mousePosition;

		// レイを取得.
		Ray clkRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit rh;
		//当たり判定
		if (Physics.Raycast(clkRay, out rh, 500)){
			selectGameObject = rh.collider.gameObject;// クリックされたゲームオブジェクトを格納.
			objectPos = Camera.main.WorldToScreenPoint(pCtrler.gameObject.transform.position);
		}
		else{
			selectGameObject = null;// 何もクリックされていない場合はnullを代入
		}
	}
	/**
	 * @クリックされたときの処理
	*/
	private void onReleased () {

		Vector2 releasePosition = Input.mousePosition;
		//Calc Swipe
		Vector2 subVec =  releasePosition - inputPositionStart;

		if(Mathf.Abs(subVec.y) < tapRangeDetection)
		{
			pCtrler.StopRotate();
		}else{// if Swipe
			if(Screen.width/2 > inputPositionStart.x){
				// Swipe Up
				if( subVec.y > 0)	pCtrler.IncreaseSpeed (accelSpeedValue);
				else				pCtrler.IncreaseSpeed (-accelSpeedValue);
			}else{
				// Swipe Down
				if( subVec.y < 0)	pCtrler.IncreaseSpeed (accelSpeedValue);
				else				pCtrler.IncreaseSpeed (-accelSpeedValue);
			}
		}
	}
}