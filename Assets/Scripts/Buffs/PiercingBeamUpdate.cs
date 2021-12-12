using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingBeamUpgrade : Upgrade
{
    protected override void SetUpgrade()
    {
        PlayerEntity.Instance.unlockedPiercingBeam = true;
    }
}
