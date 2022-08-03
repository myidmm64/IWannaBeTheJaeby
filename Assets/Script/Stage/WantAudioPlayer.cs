using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WantAudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip _clip = null;
    [SerializeField]
    private bool _isChangeNormalBGM = false;
    [SerializeField]
    private StageBGMAudio _stageBGMAudio = null;

    public void ClipStart()
    {
        _stageBGMAudio.BGMPlay(_clip);
        if(_isChangeNormalBGM)
        {
            _stageBGMAudio.NormalBGM = _clip;
        }
    }
}
