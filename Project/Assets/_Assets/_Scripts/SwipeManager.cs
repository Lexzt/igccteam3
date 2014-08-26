using UnityEngine;
using System.Collections;

public class SwipeManager : MonoBehaviour
{

	/// 変数定義
	public float rotSpeed = 0.01f;
	public Vector2 inputPosition;
	public Vector2 inputPositionBuffer;
	public float flickDistance = 0.0f;

	private PieController pCtrler;
	private bool isClick = false;
	private Vector2 objectPos;
	void Start()
	{
		isClick = false;
		pCtrler = GetComponent<PieController>();
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			onClickObject();
		}
		
		if(Input.GetMouseButtonUp(0)){
			onClickEnd();
		}
	}
	
	private void onClickObject(){
		GameObject selectGameObject;// クリックされたゲームオブジェクトへの参照.

		// レイを取得.
		Ray clkRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit rh;
		//当たり判定
		if (Physics.Raycast(clkRay, out rh, 500)){
			selectGameObject = rh.collider.gameObject;// クリックされたゲームオブジェクトを格納.
			print (selectGameObject);
			if(selectGameObject.name == "Cylinder_1_Part(Clone)")
			{
				isClick = true;
			}
			//クリックした際のポジションを取得
			inputPosition = selectGameObject.transform.position;
			inputPositionBuffer = Input.mousePosition;
			objectPos = Camera.main.WorldToScreenPoint(selectGameObject.transform.position);
		}
		else{
			selectGameObject = null;// 何もクリックされていない場合はnullを代入
		}
	}
	/**
	 * @クリックされたときの処理
	*/
	private void onClickEnd () {

		if (!isClick) return;

		// ドラッグ距離を判定
		//クリックした際のポジションを取得
		inputPosition = inputPositionBuffer;
		inputPositionBuffer = Input.mousePosition;
		// 2つのベクトルの角度を比較して右回転か左回転かを判断する
		float angle1 = Mathf.Atan2(inputPositionBuffer.x-objectPos.x,inputPositionBuffer.y-objectPos.y);
		float angle2 = Mathf.Atan2(inputPosition.x-objectPos.x,inputPosition.y-objectPos.y);
		float subAngle = angle1 - angle2;// プラスなら右回転
		float rotScale = 0;
		rotScale = Vector2.Angle(inputPositionBuffer-objectPos, inputPosition-objectPos);
		if(subAngle < 0){
			rotScale *= -1.0f;
		}
		// その分だけ回転速度をアップする
		print (rotScale);
		if(Mathf.Abs(rotScale) < 0.03f)
		{
			pCtrler.StopRotate();
		}else{
			rotScale *= 0.01f;
			pCtrler.IncreaseSpeed (rotScale);
		}
		isClick = false;
	}
}