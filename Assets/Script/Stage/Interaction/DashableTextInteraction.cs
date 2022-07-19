using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DashableTextInteraction : Interaction
{
    private TextMeshPro _text = null;

    private void Awake()
    {
        _text = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    public override void DoEnterInteraction()
    {
        _text.SetText("대시 가능합니다");
    }

    public override void DoExitInteraction()
    {
        _text.SetText("");
    }

    public override void DoStayInteraction()
    {
    }
}
