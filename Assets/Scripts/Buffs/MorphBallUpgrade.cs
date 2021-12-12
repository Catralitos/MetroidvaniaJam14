using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphBallUpgrade : Upgrade
{
    protected override void SetUpgrade()
    {
        PlayerEntity.Instance.unlockedMorphBall = true;
    }
}   
