using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    /// <summary>
    /// Fade前の処理デリゲート
    /// </summary>
    public delegate void OnBeforeFade();
    OnBeforeFade _onBefore;

    /// <summary>
    /// Fade後の処理デリゲート
    /// </summary>
    public delegate void OnAfterFade();
    OnAfterFade _onAfter;

    Image _fadeOutImage;
    Image _fadeInImage;
    float _fadeTime;

    /// <summary>
    /// Fade中はtrue、Fade前はfalse
    /// </summary>
    bool _isFade;

    public bool IsFade
    {
        get
        {
            return _isFade;
        }
        set
        {
            _isFade = value;
        }
    }

    /// <summary>
    /// Fade前に行うイベントを登録
    /// </summary>
    /// <param name="del"></param>
    public void SetupDelegate(OnBeforeFade del)
    {
        _onBefore = del;
    }
    /// <summary>
    /// Fade後に行うイベントを登録
    /// </summary>
    /// <param name="del"></param>
    public void SetupDelegate(OnAfterFade del)
    {
        _onAfter = del;
    }

    /// <summary>
    /// 普通のFade
    /// </summary>
    /// <param name="fadeOutImage"></param>
    public void OnFade(Image fadeOutImage, float time)
    {
        if (_isFade) return;

        _fadeOutImage = fadeOutImage;
        _fadeTime = time;
        _isFade = true;

        StartCoroutine(Fade());
    }
    /// <summary>
    /// クロスフェード
    /// </summary>
    /// <param name="fadeOutImage"></param>
    /// <param name="fadeInImage"></param>
    /// <param name="time"></param>
    public void OnFade(Image fadeOutImage, Image fadeInImage, float time)
    {
        if (_isFade) return;

        _fadeOutImage = fadeOutImage;
        _fadeInImage = fadeInImage;
        _fadeTime = time;
        _isFade = true;

        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        float interval = _fadeTime / 255f;
        float alpha = 1f / 255f;
        float value = 0;

        Debug.Log($"Fade開始");

        _onBefore?.Invoke();

        yield return new WaitUntil(() => _onBefore == null);

        ImageColorFade(_fadeOutImage, 1);

        if (_fadeInImage)
            ImageColorFade(_fadeInImage, 0);

        while (true)
        {
            yield return new WaitForSeconds(interval);

            value += alpha;

            ImageColorFade(_fadeOutImage, 1 - value);

            if (_fadeInImage)
                ImageColorFade(_fadeInImage, value);

            if (Skip())
            {
                break;
            }

            if (_fadeOutImage.color.a <= 0)
            {
                break;
            }
        }

        Debug.Log($"Fade終了");

        ImageColorFade(_fadeOutImage, 0);

        if (_fadeInImage)
            ImageColorFade(_fadeInImage, 1);

        _onAfter?.Invoke();

        _isFade = false;
    }

    void ImageColorFade(Image image, float alpha)
    {
        var c = image.color;
        c.a = alpha;
        image.color = c;
    }

    /// <summary>
    /// スキップ
    /// </summary>
    public bool Skip()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            return true;
        }

        return false;
    }

}