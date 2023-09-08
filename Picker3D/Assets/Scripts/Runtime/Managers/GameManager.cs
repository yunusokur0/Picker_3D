using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameStates states;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    //[Button("Change State")]
    private void OnChangeGameState(GameStates state)
    {
        states = state;
    }
}
