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
        _text.SetText("dashable");
    }

    public override void DoExitInteraction()
    {
        _text.SetText("");
    }

    public override void DoStayInteraction()
    {
    }
}
