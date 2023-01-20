using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// ���ʂ��󂯎�鑤�̂��߂̃C���^�[�t�F�[�X
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAwaiter<T>
{
    /// <summary>
    /// �������I���������ǂ���
    /// </summary>
    bool IsCompleted { get; }

    /// <summary>
    /// �����̌���
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
            "�͂�",
            "������",
            "�킩��Ȃ�",
        };
        Debug.Log("�I������\�����ē��͂�҂��܂��B");
        yield return WaitForSelection(selection, out var awaiter);
        Debug.Log($"�I�������ʂ� {selection[awaiter.Result]} �ł����B");
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
        // �I������\�����āA�������̑҂���
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
//            Debug.Log("�}�E�X�̃{�^�����͂�҂��܂�");

//            yield return WaitForMouseButtonDown(out var awaiter);

//            // Awaiter ��҂�
//            //while (!awaiter.IsCompleted) { yield return null; }

//            // Awaiter �̏I����́A�K�����ʂ��ۏ؂���Ă���
//            Debug.Log($"�}�E�X��{awaiter.Result}�{�^����������܂���");
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
//        // �ǂ̃}�E�X�{�^���������ꂽ�̂��A���ʂ�Ԃ������B
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