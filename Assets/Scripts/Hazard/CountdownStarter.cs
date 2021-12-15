using GameManagement;
using Hazard;

public class CountdownStarter : Openable
{
    public override void Open()
    {
        
        LevelManager.Instance.StartFinalCountdown();
    }
    
    public override void Close()
    {
        LevelManager.Instance.StopFinalCountown();
    }
}
