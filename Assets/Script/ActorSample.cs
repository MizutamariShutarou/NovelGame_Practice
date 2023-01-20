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
            yield return _actor.FadeOut(2, cts.Token); // 2�b�����ăt�F�[�h�A�E�g

            yield return WaitClick(); // �N���b�N��҂�
            yield return null; // ���O�� GetMouseButtonDown ���A�����Ȃ��悤��1�t���[���҂�

            cts = new CancellationTokenSource();
            StartCoroutine(CancelIfClicked(cts));
            yield return _actor.FadeIn(2, cts.Token); // �Q�b�����ăt�F�[�h�C��

            yield return WaitClick(); // �N���b�N��҂�
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