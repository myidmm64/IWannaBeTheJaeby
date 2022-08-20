using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewText : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> _texts = new List<TextMeshProUGUI>();
    [SerializeField]
    private TextMeshProUGUI _text = null;

    public void View()
    {
        if (_texts.Count > 0)
        {
            for (int i = 0; i < _texts.Count; i++)
            {
                _texts[i].enabled = true;
            }
        }

        if (_text != null)
        {
            _text.enabled = true;
        }
    }

    public void Exit()
    {
        if (_texts.Count > 0)
        {
            for (int i = 0; i < _texts.Count; i++)
            {
                _texts[i].enabled = false;
            }
        }

        if (_text != null)
        {
            _text.enabled = false;
        }
    }
}
