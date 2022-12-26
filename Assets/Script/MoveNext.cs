using UnityEngine;
using System.Collections;

public class MoveNext : MonoBehaviour
{
    // IEnumerator型の変数を宣言
    private IEnumerator _Enumerator;

    float _time;
    void Start()
    {
        _Enumerator = CustomProcess();
    }

    IEnumerator CustomProcess()
    {
        // 1
        Debug.Log("wait");
        yield return null; // resultにtrueを返す

        // 2
        Debug.Log("Complete");
        yield break; // resultにfalseを返す
    }

    void Update()
    {
        _time += Time.deltaTime;
        // 3秒まってからMoveNext();を呼ぶ
        if (_time > 3f)
        {
            if (_Enumerator != null)
            {
                bool result = _Enumerator.MoveNext(); // このタイミングで1の処理が走る
                if (result)
                {
                    Debug.Log("Next"); // 2に移動
                }
                else // returnされなかったためresultがfalseになる
                {
                    // コルーチン終了
                    Debug.Log("End.");
                    _Enumerator = null;
                }
            }
        }
    }
}