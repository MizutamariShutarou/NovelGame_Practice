using UnityEngine;
using System.Collections;

public class MoveNext : MonoBehaviour
{
    private IEnumerator _Enumerator;

    void Start()
    {
        _Enumerator = CustomProcess();
    }

    IEnumerator CustomProcess()
    {
        Debug.Log("wait");
        yield return null;

        Debug.Log("Complete!!!");
        yield break;
    }

    void Update()
    {
        if (_Enumerator != null)
        {
            // 次のフレームへ進める
            bool result = _Enumerator.MoveNext();
            if (result)
            {
                Debug.Log("Next.");
            }
            else
            {
                // コルーチン終了。
                Debug.Log("End.");
                _Enumerator = null;
            }
        }
    }
}