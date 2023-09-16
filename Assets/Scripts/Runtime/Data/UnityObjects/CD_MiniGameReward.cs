using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CD_MiniGameReward", menuName = "Picker3D/CD_MiniGameReward", order = 0)]
public class CD_MiniGameReward : ScriptableObject
{
    public List<MiniGameRewardData> Rewards = new List<MiniGameRewardData>();
}
