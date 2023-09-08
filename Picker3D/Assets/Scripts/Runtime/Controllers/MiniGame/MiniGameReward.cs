using UnityEngine;

public class MiniGameReward : MonoBehaviour
{
    private MiniGameRewardData _data;
    private readonly string _player = "Player";
    [SerializeField] private byte stageID;
    private void Awake()
    {
        _data = GetPoolData();
    }

    private MiniGameRewardData GetPoolData()
    {
        return Resources.Load<CD_MiniGameReward>("Data/CD_MiniGameReward").Rewards[stageID];

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_player))
        {
            if (rewardmanager.Instance.rewardText != null)
            {
                rewardmanager.Instance.rewardText.text = $"{_data.Reward}$";
            }
        }
    }
}
