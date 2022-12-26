using UnityEngine;
using System.Collections;

public class MoveNext : MonoBehaviour
{
    // IEnumerator�^�̕ϐ���錾
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
        yield return null; // result��true��Ԃ�

        // 2
        Debug.Log("Complete");
        yield break; // result��false��Ԃ�
    }

    void Update()
    {
        _time += Time.deltaTime;
        // 3�b�܂��Ă���MoveNext();���Ă�
        if (_time > 3f)
        {
            if (_Enumerator != null)
            {
                bool result = _Enumerator.MoveNext(); // ���̃^�C�~���O��1�̏���������
                if (result)
                {
                    Debug.Log("Next"); // 2�Ɉړ�
                }
                else // return����Ȃ���������result��false�ɂȂ�
                {
                    // �R���[�`���I��
                    Debug.Log("End.");
                    _Enumerator = null;
                }
            }
        }
    }
}