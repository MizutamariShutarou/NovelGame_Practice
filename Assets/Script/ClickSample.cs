using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 結果を受け取る側のためのインターフェース
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAwaiter<T>
{
    /// <summary>
    /// 処理が終了したかどうか
    /// </summary>
    bool IsCompleted { get; }

    /// <summary>
    /// 処理の結果
    /// </summary>
    T Result { get; }
}

class Awaiter<T> : IAwaiter<T>
{
    public bool IsCompleted { get; private set; }

    public T Result { get; private set; }

    public void SetResult(T result)
    {
        IsCompleted = true;
        this.Result = result;
    }
}

public class ChoiceText : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        var selection = new string[]
        {
            "はい",
            "いいえ",
            "わからない",
        };
        Debug.Log("選択肢を表示して入力を待ちます。");
        yield return WaitForSelection(selection, out var awaiter);
        Debug.Log($"選択肢結果は {selection[awaiter.Result]} でした。");
    }

    public IEnumerator WaitForSelection(string[] messages, out IAwaiter<int> awaiter)
    {
        var result = new Awaiter<int>();
        var e = WaitForSelection(messages, result);
        awaiter = result;
        return e;
    }

    private IEnumerator WaitForSelection(string[] messages, Awaiter<int> awaiter)
    {
        // 選択肢を表示して、押されるの待つ処理
        yield return null;
    }
}

//public class ClickSample : MonoBehaviour
//{
//    [SerializeField]
//    private Actor _actor = default;

//    private void Start()
//    {
//        StartCoroutine(RunAsync());
//        //StartCoroutine(Main());
//    }
//    private IEnumerator RunAsync()
//    {
//        while (true)
//        {
//            Debug.Log("マウスのボタン入力を待ちます");

//            yield return WaitForMouseButtonDown(out var awaiter);

//            // Awaiter を待つ
//            //while (!awaiter.IsCompleted) { yield return null; }

//            // Awaiter の終了後は、必ず結果が保証されている
//            Debug.Log($"マウスの{awaiter.Result}ボタンが押されました");
//            yield return null;
//        }
//    }

//    private IEnumerator WaitForMouseButtonDown(out IAwaiter<int> awaiter)
//    {
//        var awaiterImpl = new Awaiter<int>();
//        //StartCoroutine(WaitForMouseButtonDown(awaiter));
//        var e = WaitForMouseButtonDown(awaiterImpl);
//        awaiter = awaiterImpl;
//        return e;
//    }
//    //private IEnumerator WaitForMouseButtonDown(out Awaiter<int> awaiter) => WaitForMouseButtonDown(awaiter = new());



//    private IEnumerator WaitForMouseButtonDown(Awaiter<int> awaiter)
//    {
//        // どのマウスボタンが押されたのか、結果を返したい。
//        while (true)
//        {
//            for (var i = 0; i < 3; i++)
//            {
//                if (Input.GetMouseButtonDown(i))
//                {
//                    awaiter.SetResult(i);
//                    yield break;
//                }
//            }

//            yield return null;
//        }
//    }

    //IEnumerator Main()
    //{
    //    while (true)
    //    {
    //        yield return WaiteForGetMouseButtonDown(value => { Debug.Log(value); });
    //    }
    //}


    //IEnumerator WaiteForGetMouseButtonDown(Action<int> action)
    //{
    //    while (true)
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            action?.Invoke(0);
    //            yield break;
    //        }
    //        else if (Input.GetMouseButtonDown(1))
    //        {
    //            action?.Invoke(1);
    //            yield break;
    //        }
    //        else if (Input.GetMouseButtonDown(2))
    //        {
    //            action?.Invoke(2);
    //            yield break;
    //        }

    //        yield return null;
    //    }
    //}
//}