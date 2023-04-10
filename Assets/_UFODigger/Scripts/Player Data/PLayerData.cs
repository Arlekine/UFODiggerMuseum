using System;
using UnityEngine;
using UnityEngine.Events;
using Utils;

[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObjects/Player", order = 3)]
public class PLayerData : ScriptableObject
{
    public UnityEvent OnGemsCountChange;
    public UnityEvent OnGoldCountChange;

    public int GemCount;
    public int GoldCount;

    public int ExcavationTurns;
    public UpgradesSO DiggingTurns;
    
    private const string _goldCountKey = "Player Gold Count";
    private const string _gemsCountKey = "Player Gems Count";
    
    private const string _gameExitTime = "ExTime";
    private const string _robotExpireTime = "RobotExpire";
    public TimeSpan OfflineTime;
    public DateTime RobotExpireTime;
    public bool IsRobotActive => RobotExpireTime > DateTime.Now;
    public bool IsRobotWasActiveBeforeQuitting;
    
    private const string _abandonedGoldKey = "AdabGold";
    public int AbandonedGold = 0;
    
    public int StartPlayerGold = 1000;
    public int StartPlayerGems = 0;

    public bool IsTutorialComplete;
    private string _tutorKey = "tutor";
    
    public bool IsBusTutorialComplete;
    private const string _busTutorKey = "busTutor";

    public void AddGems(int count)
    {
        GemCount += count;
        OnGemsCountChange.Invoke();
        SaveData();
    }

    public void AddGold(int count)
    {
        GoldCount += count;
        OnGoldCountChange.Invoke();
        SaveData();
    }

    public void LoadData()
    {
        GoldCount = SaveLoadSystem.CheckKey(_goldCountKey) ? SaveLoadSystem.LoadInt(_goldCountKey) : StartPlayerGold;
        
        AbandonedGold = SaveLoadSystem.CheckKey(_abandonedGoldKey) ? SaveLoadSystem.LoadInt(_abandonedGoldKey) : 0;

        GemCount = SaveLoadSystem.CheckKey(_gemsCountKey) ? SaveLoadSystem.LoadInt(_gemsCountKey) : StartPlayerGems;

        if (SaveLoadSystem.CheckKey(_robotExpireTime))
        {
            var temp = Convert.ToInt64(SaveLoadSystem.LoadString(_robotExpireTime));
            RobotExpireTime = DateTime.FromBinary(temp);
        }

        if (SaveLoadSystem.CheckKey(_gameExitTime))
        {
            var currentDate = DateTime.Now;
            var temp = Convert.ToInt64(SaveLoadSystem.LoadString(_gameExitTime));
            var oldDate = DateTime.FromBinary(temp);
            OfflineTime = currentDate.Subtract(oldDate);

            if (RobotExpireTime > oldDate)
                IsRobotWasActiveBeforeQuitting = true;
        }
        else
        {
            OfflineTime = TimeSpan.Zero;
        }

        IsTutorialComplete = SaveLoadSystem.CheckKey(_tutorKey) && SaveLoadSystem.LoadBool(_tutorKey);
        IsBusTutorialComplete = SaveLoadSystem.CheckKey(_busTutorKey) && SaveLoadSystem.LoadBool(_busTutorKey);
        
        OnGemsCountChange.Invoke();
        OnGoldCountChange.Invoke();
    }

    public void SaveData()
    {
        SaveLoadSystem.Save(_goldCountKey,GoldCount);
        SaveLoadSystem.Save(_gemsCountKey,GemCount);
        SaveLoadSystem.Save(_tutorKey,IsTutorialComplete);
        SaveLoadSystem.Save(_busTutorKey,IsBusTutorialComplete);
       
        SaveLoadSystem.Save(_gameExitTime,DateTime.Now.ToBinary().ToString());
        SaveLoadSystem.Save(_robotExpireTime, RobotExpireTime.ToBinary().ToString());
        
        SaveLoadSystem.Save(_abandonedGoldKey,AbandonedGold);
    }
}


