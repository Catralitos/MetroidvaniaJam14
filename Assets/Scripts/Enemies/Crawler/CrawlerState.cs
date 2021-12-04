using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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