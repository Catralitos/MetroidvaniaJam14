using Audio;
using Enemies.Base;

namespace Enemies.Crawler
{
    public class CrawlerState : EnemyState<Crawler>
    {
    
        protected static new T Create<T>(Crawler target) where T : CrawlerState
        {
            var state = EnemyState<Crawler>.Create<T>(target);
            return state;
        }
    }
}