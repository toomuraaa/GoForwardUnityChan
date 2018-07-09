using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour {

    //アニメーションするためのコンポーネントを入れる
    Animator animator;

    //Unitychanを移動させるコンポーネントを入れる
    Rigidbody2D rigit2D;

    //地面の位置
    private float groundLevel = -3.0f;

    //ジャンプの速度の減衰
    private float dump = 0.8f;

    //ジャンプ速度
    float jumpVelocity = 20;

    //ゲームオーバーになる位置
    private float deadLine = -9;

	// Use this for initialization
	void Start () {
        //アニメータのコンポーネントを取得
        this.animator = GetComponent<Animator>();
        //Rigitbody2Dのコンポーネントを取得
        this.rigit2D = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
        //走るアニメーションを再生するために、Animetorのパラメータを調節
        this.animator.SetFloat("Horizontal", 1);

        //着地しているかどうか確認
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;
        this.animator.SetBool("isGround", isGround);

        GetComponent<AudioSource>().volume = (isGround) ? 1 : 0;

        //着地状態でクリックされた場合
        if(Input.GetMouseButtonDown(0) && isGround)
        {
            //上方向のちからを加える
            this.rigit2D.velocity = new Vector2(0, this.jumpVelocity);
        }

        //クリックをやめたら上方向への速度を減少する
        if(Input.GetMouseButton(0) == false)
        {
            if(this.rigit2D.velocity.y > 0)
            {
                this.rigit2D.velocity *= this.dump;
            }
        }
        //デッドラインを超えた場合ゲームオーバーにする
        if(transform.position.x < this.deadLine)
        {
            //UIControllerのGameOverを呼び出して画面上に「Gm\ameOver」と表示する
            GameObject.Find("Canvas").GetComponent<UIController>().GameOver();

            //Unityちゃんを破棄する
            Destroy(gameObject);
        }
	}
}
