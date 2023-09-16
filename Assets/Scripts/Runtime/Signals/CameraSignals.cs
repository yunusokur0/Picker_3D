using UnityEngine.Events;

public class CameraSignals : MonoSingleton<CameraSignals>
{
    public UnityAction onSetCameraTarget = delegate { };
}
