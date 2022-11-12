using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCoroutine : MonoBehaviour
{
    void Start()
    {
        var r = RotateAsync(100);
        while(r.MoveNext())
        {
            RotateAsync(1);
        }
    }

    private IEnumerator RotateAsync(int num)
    {
        for (int i = 0; i < num; i++)
        {
            transform.Rotate(i, 0, 0);
            yield return null;
        }
    }
}
