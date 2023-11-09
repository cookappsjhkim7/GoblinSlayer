using UnityEngine;
using UnityEngine.UI;

public class UI_TouchButton : MonoBehaviour
{
    private Button _button;
    private Toggle _toggle;

    private void Start()
    {
        _button = GetComponent<Button>();
        _toggle = GetComponent<Toggle>();

        if (_button != null)
        {
            _button.onClick.AddListener(OnClickButton);
        }
        
        if (_toggle != null)
        {
            _toggle.onValueChanged.AddListener(OnValueChange);
        }
    }

    private void OnClickButton()
    {
        SoundManager.Instance.Play(Enum_Sound.Effect,"UI_Click");
    }

    private void OnValueChange(bool isOn)
    {
        if (isOn)
        {
            SoundManager.Instance.Play(Enum_Sound.Effect,"UI_Click");
        }
    }
}