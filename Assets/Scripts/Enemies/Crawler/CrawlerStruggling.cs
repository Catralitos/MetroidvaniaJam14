using UnityEngine;

public class CrawlerStruggling : CrawlerState
{
    public static CrawlerStruggling Create(Crawler target)
    {
        return CrawlerState.Create<CrawlerStruggling>(target);
    }

    public override void StateStart()
    {
        base.StateStart();
        target.rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}