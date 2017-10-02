using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class FolderControl : MonoBehaviour {

	// Use this for initialization
	void Start () {

        // direct直下のファイルを取得
        string[] files = System.IO.Directory.GetFiles(
            @"C:\test", "*", System.IO.SearchOption.AllDirectories
        );

        // direct直下のディレクトリを取得
        string[] directries = System.IO.Directory.GetDirectories(@"C:\test", "*", System.IO.SearchOption.AllDirectories);

        // 表示用オブジェクトを取得
        GameObject txt = GameObject.Find("FolderText");
        TextMesh tm = (TextMesh)txt.GetComponent(typeof(TextMesh));

        // ディレクトリの状態を表示
        tm.text = "";
        foreach(string strBuff in files)
        {
            tm.text = tm.text + strBuff + "\r\n";
        }
        foreach (string strBuff in directries)
        {
            tm.text = tm.text + strBuff + "\r\n";
        }

    }
}
