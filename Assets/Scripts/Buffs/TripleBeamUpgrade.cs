using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBeamUpgrade : Upgrade
{
    protected override void SetUpgrade()
    {
        PlayerEntity.Instance.unlockedTripleBeam = true;
    }
}
