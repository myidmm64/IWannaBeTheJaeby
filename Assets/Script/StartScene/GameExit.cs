using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : StartSceneText
{
    public override void Enter()
    {
    }

    public override void Excute()
    {
        Debug.Log("���� ������");
        Application.Quit();
    }

    public override void Exit()
    {
    }

    public override void Return()
    {
    }
}
