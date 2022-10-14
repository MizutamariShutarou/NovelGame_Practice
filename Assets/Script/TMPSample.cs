using TMPro;
using UnityEngine;

public class TMPSample : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textUi = default;

    private void Start()
    {
        _textUi.text = "‚±‚ñ‚Î‚ñ‚Í";
    }
}