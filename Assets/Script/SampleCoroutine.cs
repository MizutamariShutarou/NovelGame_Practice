using System.Collections;
using UnityEngine;

public class SampleCoroutine : MonoBehaviour
{
    bool _isNext;
    private void Start()
    {
        StartCoroutine(ExecuteAsync());
    }

    private IEnumerator ExecuteAsync()
    {
        Debug.Log("ExecuteAsync: Begin");

        while (true)
        {
            var t = 0F;

            // X軸の回転を2秒間
            yield return RotateAsync(Vector3.right, 2);

            // 1秒間待機する
            //yield return new WaitForSeconds(1);

            //yield return WaitForSecondOrCrick();

            yield return AllWait();

            // Y軸の回転を2秒間
            yield return RotateAsync(Vector3.up, 2);

            // 1秒間待機する
            yield return new WaitForSeconds(1);

            // Z軸の回転を2秒間
            yield return RotateAsync(Vector3.forward, 2);

            // 1秒間待機する
            yield return new WaitForSeconds(1);
        }

        // Debug.Log("ExecuteAsync: End");
    }

    private IEnumerator RotateAsync(Vector3 eulers, float duration)
    {
        Debug.Log($"RotateAsync: Begin eulers={eulers}, duration={duration}");
        var t = 0F;
        while (t < duration)
        {
            t += Time.deltaTime;
            transform.Rotate(eulers);
            yield return null;
        }
        Debug.Log("ExecuteAsync: End");
    }


    IEnumerator WaitForSecondOrCrick()
    {
        float time = 0;
        while (5 > time)
        {
            yield return null;
            time += Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Fire1");
                yield break;
            }
        }
    }

    IEnumerator WaitForSecond(int second)
    {
        yield return null;
        float time = 0;

        time += Time.deltaTime;
        if (time > second)
        {
            _isNext = true;
        }

    }

    IEnumerator WaitClick()
    {
        yield return null;
        if (Input.GetMouseButtonDown(0))
        {
            _isNext = true;
            yield break;
        }
    }

    IEnumerator AllWait()
    {
        while (!_isNext)
        {
            yield return null;
            WaitForSecond(2);
            WaitClick();
        }
    }
    private void Update()
    {
        Debug.Log(_isNext);
    }
}