using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveNoteText;
    [SerializeField] private TextMeshProUGUI _waveNumberText;
    [SerializeField] private TextMeshProUGUI _countDownText;

    public void ChangeNoteText(string text)
    {
        _waveNoteText.SetText(text);
    }

    public void ChangeNumberText(string text)
    {
        _waveNumberText.SetText(text);
    }

    public void ChangeTimerText(string text)
    {
        _countDownText.SetText(text);
    }
}
