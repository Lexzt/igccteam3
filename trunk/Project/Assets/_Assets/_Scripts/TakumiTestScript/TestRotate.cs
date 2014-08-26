using UnityEngine;
using System.Collections;

public class TestRotate : MonoBehaviour {
	
	
	/// 関数定義
	//private void onClickUpdate();
	public const int lineNum = 5;
	public int[] Line = new int[lineNum];
	/// 変数定義
	public float rotSpeed = 0;
	public Vector2 inputPosition;
	public Vector2 inputPositionBuffer;
	public float flickDistance = 0.0f;
	
	private bool isClick = false;
	private Vector2 objectPos;
	// Use this for initialization
	void Start () {
		isClick = false;
		objectPos = new Vector2(0,0);
		for(int i=0;i<lineNum;++i)
		{
			Line[i] = i;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			onClickObject();
		}
		
		if( isClick ){// クリック中の場合
			onClickUpdate();
		}
		
		if(Input.GetMouseButtonUp(0)){
			isClick = false;
		}
		
		
		transform.Rotate(new Vector3(0,0,rotSpeed));
	}
	
	private void onClickObject(){
		GameObject selectGameObject;// クリックされたゲームオブジェクトへの参照.
		
		// レイを取得.
		Ray clkRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit rh;
		//当たり判定
		if (Physics.Raycast(clkRay, out rh, 500)){
			selectGameObject = rh.collider.gameObject;// クリックされたゲームオブジェクトを格納.
			if(selectGameObject == this.gameObject)
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
		rotSpeed += flickDistance/100;
	}
	/**
	 * @クリックされたときの処理
	*/
	private void onClickUpdate () {
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
		if(subAngle > 0){
			rotScale *= -1.0f;
		}
		// その分だけ回転速度をアップする
		rotSpeed += rotScale/100;
	}
}
