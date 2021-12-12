using Audio;
using Enemies.Base;

namespace Enemies.Crawler
{
    public class CrawlerState : EnemyState<Crawler>
    {
        protected AudioManager audioManager;
    
        protected static new T Create<T>(Crawler target) where T : CrawlerState
        {
            var state = EnemyState<Crawler>.Create<T>(target);
            state.audioManager = target.GetComponent<AudioManager>();
            return state;
        }
    }
}