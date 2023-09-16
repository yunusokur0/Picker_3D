using UnityEngine.Events;

public class CoreUISignals : MonoSingleton<CoreUISignals>
{
    public UnityAction<UIPanelTypes, int> onOpenPanel = delegate { };
    public UnityAction<int> onClosePanel = delegate { };
    public UnityAction onCloseAllPanels = delegate { };
}
