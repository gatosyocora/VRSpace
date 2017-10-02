using UnityEngine;
using System.Runtime.InteropServices;

/**
 * コントローラ操作
 * RHandTrigger:    ディスプレイ掴む・ディスプレイ操作
 * ->   RThumbstickDown:    ディスプレイ縮小
 *      RThumbstickUp:      ディスプレイ拡大
 * RIndexTrigger:   マウスカーソル移動・マウス操作
 * ->   B:                  マウスクリック・長押し
 * 
 **/

public class TouchController: MonoBehaviour {

    [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    private const int MOUSEEVENTF_LEFTDOWN = 0x2;
    private const int MOUSEEVENTF_LEFTUP = 0x4;

    public OVRInput.Controller controller;
    GameObject go;

    Vector3 downControllerPos;
    Vector3 downDispPos;

    Vector3 downR;
    Vector3 downDispR;


    TextMesh tm;　// デバッグ用****************************

    private void Start()
    {
        // コントローラによって操作できるオブジェクトを変える
        if (controller == OVRInput.Controller.RTouch)
        {
            go = GameObject.Find("Monitor Board");
        }
        else if (controller == OVRInput.Controller.LTouch)
        {
            go = GameObject.Find("ImageObject");
        }

        // デバッグ表示用オブジェクトを取得******************************
        GameObject txt = GameObject.Find("DebugText");
        tm = (TextMesh)txt.GetComponent(typeof(TextMesh));
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 controllerPos = OVRInput.GetLocalControllerPosition(controller);
        Vector3 controllerRot = OVRInput.GetLocalControllerRotation(controller).eulerAngles;

        transform.localPosition = controllerPos;
        transform.eulerAngles = controllerRot;

        float dispWidth = go.GetComponent<Renderer>().bounds.size.x;
        float dispHeight = go.GetComponent<Renderer>().bounds.size.y;


        /* 右コントローラ */
        if (controller == OVRInput.Controller.RTouch)
        {
            if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))          // ディスプレイ操作
            {
                if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger)) // ボタンが押し込まれた瞬間の座標を取得
                {

                    // コントローラの座標
                    downControllerPos = controllerPos;

                    // 対象オブジェクトの座標
                    downDispPos = go.transform.position;

                    // コントローラの回転
                    downR = transform.eulerAngles;

                    // 対象オブジェクトの回転
                    downDispR = go.transform.eulerAngles;
                }

                go.transform.position = downDispPos + (controllerPos - downControllerPos);   // ディスプレイ移動
                go.transform.eulerAngles = downDispR + (transform.eulerAngles - downR); // ディスプレイ回転

                if (OVRInput.Get(OVRInput.RawButton.RThumbstickDown))  // ディスプレイ縮小
                {
                    go.transform.localScale -= new Vector3(0.002f, 0.002f * dispHeight / dispWidth, 0);
                }
                else if (OVRInput.Get(OVRInput.RawButton.RThumbstickUp)) // ディスプレイ拡大
                {
                    go.transform.localScale += new Vector3(0.002f, 0.002f * dispHeight / dispWidth, 0);
                }
            }
            else if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))      // カーソル操作
            {

                int screenWidth = 1919;// System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

                Vector3 dispPos = go.transform.position;

                int mouseX = (int)((dispPos.x - dispWidth/2) + (controllerPos.x - (dispPos.x - dispWidth / 2)) * screenWidth/dispWidth);    // 左端 + (差分) * 比
                int mouseY = (int)((dispPos.y + dispHeight/2) + ((dispPos.y + dispHeight / 2) - controllerPos.y) * screenHeight/dispHeight);  // 上端 + (差分) * 比

                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(mouseX, mouseY);    // カーソル移動

                if (OVRInput.GetDown(OVRInput.RawButton.B))     // クリック
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                }
                else if (OVRInput.GetUp(OVRInput.RawButton.B))
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }
            }

            //デバッグ用
            tm.text = "";
            tm.text = tm.text + "screenX: " + go.transform.position.x.ToString() + "\r\n";
            tm.text = tm.text + "screenY: " + go.transform.position.y.ToString() + "\r\n";
            tm.text = tm.text + "screenZ: " + go.transform.position.z.ToString() + "\r\n";
            tm.text = tm.text + "RhandX: " + controllerPos.x.ToString() + "\r\n";
            tm.text = tm.text + "RhandY: " + controllerPos.y.ToString() + "\r\n";
            tm.text = tm.text + "RhandZ: " + controllerPos.z.ToString() + "\r\n";
            tm.text = tm.text + "rotateX: " + transform.localRotation.x.ToString() + "\r\n";
            tm.text = tm.text + "rotateY: " + transform.localRotation.y.ToString() + "\r\n";
            tm.text = tm.text + "rotateZ: " + transform.localRotation.z.ToString() + "\r\n";


        }

        /* 左コントローラ */
        else if (controller == OVRInput.Controller.LTouch)
        {
            if (OVRInput.Get(OVRInput.RawButton.LHandTrigger))          // ディスプレイ操作
            {
                if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger)) // ボタンが押し込まれた瞬間の座標を取得
                {

                    // コントローラの座標
                    downControllerPos = controllerPos;

                    // 対象オブジェクトの座標
                    downDispPos = go.transform.position;

                    // コントローラの回転
                    downR = transform.eulerAngles;

                    // 対象オブジェクトの回転
                    downDispR = go.transform.eulerAngles;
                }

                go.transform.position = downDispPos + (controllerPos - downControllerPos);   // ディスプレイ移動
                go.transform.eulerAngles = downDispR + (transform.eulerAngles - downR); // ディスプレイ回転

                if (OVRInput.Get(OVRInput.RawButton.LThumbstickDown))  // ディスプレイ縮小
                {
                    go.transform.localScale -= new Vector3(0.002f, 0, 0.002f * dispHeight / dispWidth);
                }
                else if (OVRInput.Get(OVRInput.RawButton.LThumbstickUp)) // ディスプレイ拡大
                {
                    go.transform.localScale += new Vector3(0.002f, 0, 0.002f * dispHeight / dispWidth);
                }
            }
        }
    }
}
