using UnityEngine;
using Audio;

namespace Enemies.Crawler
{
    public class CrawlerFalling : CrawlerState
    {
        private AudioManager _audioManager;
        
        public static CrawlerFalling Create(Crawler target)
        {
            return CrawlerState.Create<CrawlerFalling>(target);
        }

        public override void StateStart()
        {
            base.StateStart();
            _audioManager = GetComponent<AudioManager>();
            target.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            target.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
            _audioManager.Stop("Crawling");
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
}
