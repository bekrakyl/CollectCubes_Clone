using System;
using UnityEngine;

public static class ActionManager 
{
    public static Func<PoolType, Vector3, Transform, GameObject> GetItemFromPool { get; set; }
    public static Action<GameObject, PoolType> ReturnItemToPool { get; set; }
    public static Action<int> CubeCollected { get; set; }

    public static Action<int> TimeChallengeEnd { get; set; }

}
