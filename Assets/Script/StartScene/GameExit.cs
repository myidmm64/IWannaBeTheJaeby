using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : StartSceneText
{
    public override void Excute()
    {
        Debug.Log("게임 나가기");
        Application.Quit();
    }

    public override void Return()
    {
    }
}
