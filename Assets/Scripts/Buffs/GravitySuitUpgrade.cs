using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySuitUpgrade : Upgrade
{
    protected override void SetUpgrade()
    {
        PlayerEntity.Instance.unlockedGravitySuit = true;
    }
}
