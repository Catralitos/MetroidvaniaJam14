using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CrawlerFalling : CrawlerState
{
    public static CrawlerFalling Create(Crawler target)
    {
        return CrawlerState.Create<CrawlerFalling>(target);
    }

    public override void StateStart()
    {
        base.StateStart();
        target.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        target.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        target.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
        if (target.lyingOnBack)
        {
            SetState(CrawlerStruggling.Create(target));
        }
    }
}
