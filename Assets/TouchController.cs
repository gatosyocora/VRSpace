/* Touchコントローラー操作用プログラム */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public OVRInput.Controller controller;
    GameObject go;
    public GameObject menu;
    GameObject top, mid, und;
    bool menuflag = false;

    float downX, downY;

    private void Start()
    {
        go = GameObject.Find("Monitor Board"); // ディスプレイを表示しているオブジェクトを取得
    }


    // Update is called once per frame
    void Update()
    {
        // コントローラの各座標取得
        float x = OVRInput.GetLocalControllerPosition(controller).x;
        float y = OVRInput.GetLocalControllerPosition(controller).y;
        float z = OVRInput.GetLocalControllerPosition(controller).z;
        transform.localPosition = new Vector3(x, y, z - 20);
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);

        // ディスプレイオブジェクトの移動
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
            {
                downX = x;
                downY = y;
            }
            go.transform.position = new Vector3(x - downX, y, z - 10);
        }

        // 操作メニューを表示
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Instantiate(menu, new Vector3(x, y, z - 10), Quaternion.Euler(-90, 180, 0));
        }

        // マウスカーソルを移動
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            //Vector2 screenPos = new Vector2(x, y);
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)(x * 1500) + Screen.width / 2, Screen.height / 2 - (int)(y * 300));

            if (OVRInput.Get(OVRInput.RawButton.B))
            {

            }
        }
    }
}
