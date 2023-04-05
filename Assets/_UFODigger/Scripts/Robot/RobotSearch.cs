using System.Linq;
using UnityEngine;

public class RobotSearch :  IState
{
    private RobotCleaner _robot;
    private Stand[] _stands = new Stand[4];
    
    public RobotSearch(RobotCleaner robot, Stand[] stands) {

        _robot = robot;
        _stands = stands;
    }
    
    public void OnEnter()
    {
        var visibleStand = from stand in _stands where stand.IsStandBuild == true select stand;
        var enumerable = visibleStand as Stand[] ?? visibleStand.ToArray();
        var randomStand = enumerable[Random.Range(0, enumerable.Count())];
        _robot.SetTargetStand(randomStand);
    }

    public void Tick()
    {
       
    }

    public void OnExit()
    {
        
    }
}
