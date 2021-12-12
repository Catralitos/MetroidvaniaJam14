using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUpgrade : Upgrade
{
    protected override void SetUpgrade()
    {
        PlayerEntity.Instance.unlockedDash = true;
    }
}
