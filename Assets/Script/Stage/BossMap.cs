using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMap : Map
{
    private StageBGMAudio _bgmSource = null;

    private void Awake()
    {
        _bgmSource = transform.root.GetComponent<StageBGMAudio>();
    }

    public override void Init()
    {
        base.Init();

        _bgmSource.BossBGMPlay();
    }
}
