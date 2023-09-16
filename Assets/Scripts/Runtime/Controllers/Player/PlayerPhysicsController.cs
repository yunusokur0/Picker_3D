using DG.Tweening;
using UnityEngine;

public class PlayerPhysicsController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private PlayerManager manager;
    [SerializeField] private new Collider collider;
    [SerializeField] private new Rigidbody rigidbody;

    #endregion

    #region Private Variables

    private readonly string _stageArea = "StageArea";
    private readonly string _finish = "FinishArea";
    private readonly string _miniGame = "MiniGameArea";
    private bool isColliding = false;
    private PlayerMovementData _data;
    #endregion

    #endregion
    internal void SetData(PlayerMovementData data)
    {
        _data = data;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_stageArea))
        {
            CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
            InputSignals.Instance.onDisableInput?.Invoke();
            DOVirtual.DelayedCall(3, () =>
            {
                var result = other.transform.parent.GetComponentInChildren<PoolController>()
                    .TakeResults(manager.StageValue);

                if (result)
                {
                    CoreGameSignals.Instance.onStageAreaSuccessful?.Invoke(manager.StageValue);
                    InputSignals.Instance.onEnableInput?.Invoke();
                    UISignals.Instance.onMiniGameBar?.Invoke();

                }
                else
                {
                    CoreGameSignals.Instance.onLevelFailed?.Invoke();
                }
            });
            return;

        }

        if (other.CompareTag(_finish))
        {
            CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
            InputSignals.Instance.onDisableInput?.Invoke();
            return;
        }

        if (other.CompareTag(_miniGame))
        {
            isColliding = true;
            UISignals.Instance.onMiniGameBarFinish?.Invoke();
        }
    }
    private void Update()
    {
        if (isColliding)
        {
            float newFillAmount = LevelPanelController.Instance.MiniGameBarImage.fillAmount - (0.45f * Time.deltaTime);
            newFillAmount = Mathf.Clamp01(newFillAmount); 
            LevelPanelController.Instance.MiniGameBarImage.fillAmount = newFillAmount;
            if (newFillAmount <= 0)
            {
                isColliding = false;
                CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
            }
                
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        var transform1 = manager.transform;
        var position1 = transform1.position;

        Gizmos.DrawSphere(new Vector3(position1.x, position1.y - 1f, position1.z + .9f), 1.7f);
    }

    public void OnReset()
    {
    }
}
