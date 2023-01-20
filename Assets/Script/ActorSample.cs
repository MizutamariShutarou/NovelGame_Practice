using System.Collections;
using System.Threading;
using UnityEngine;

public class ActorSample : MonoBehaviour
{
    [SerializeField]
    private Actor _actor = default;

    private void Start()
    {
        StartCoroutine(RunAsync());
    }

    private IEnumerator RunAsync()
    {
        while (true)
        {
            var cts = new CancellationTokenSource();
            StartCoroutine(CancelIfClicked(cts));
            yield return _actor.FadeOut(2, cts.Token); // 2秒かけてフェードアウト

            yield return WaitClick(); // クリックを待つ
            yield return null; // 直前の GetMouseButtonDown が連続しないように1フレーム待つ

            cts = new CancellationTokenSource();
            StartCoroutine(CancelIfClicked(cts));
            yield return _actor.FadeIn(2, cts.Token); // ２秒かけてフェードイン

            yield return WaitClick(); // クリックを待つ
            yield return null;
        }
    }

    private IEnumerator CancelIfClicked(CancellationTokenSource cts)
    {
        while (!IsSkipRequested()) { yield return null; }
        cts.Cancel();
    }

    private IEnumerator WaitClick()
    {
        while (!IsSkipRequested()) { yield return null; }
    }

    private static bool IsSkipRequested()
    {
        return Input.GetMouseButtonDown(0);
    }
}