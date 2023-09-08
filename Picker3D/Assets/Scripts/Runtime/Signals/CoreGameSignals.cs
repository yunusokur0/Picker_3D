using System;
using UnityEngine.Events;

public class CoreGameSignals : MonoSingleton<CoreGameSignals>
{
    public UnityAction<GameStates> onChangeGameState = delegate { };
    public UnityAction<byte> onLevelInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onLevelSuccessful = delegate { };
    public UnityAction onLevelFailed = delegate { };
    public UnityAction onNextLevel = delegate { };
    public UnityAction onRestartLevel = delegate { };
    public UnityAction onPlay = delegate { };
    public UnityAction onReset = delegate { };
    public Func<byte> onGetLevelValue = delegate { return 0; };

    public UnityAction<byte> onStageAreaSuccessful = delegate { };
    public UnityAction onStageAreaEntered = delegate { };
    public UnityAction onFinishAreaEntered = delegate { };
    public UnityAction onMiniGameAreaEntered = delegate { };
    public UnityAction onMultiplierAreaEntered = delegate { };
}

//public class CoreGameSignals : MonoSingleton<CoreGameSignals>
//{
//    //level seviyesi açar ve cagirldiginde byte turunde parametre alýr
//    public UnityAction<byte> onLevelInitialize = delegate { };
//    //aktive leveli siler
//    public UnityAction onClearActiveLevel = delegate { };
//    //seviye tamamlandýgýnda cagirilir
//    public UnityAction onLevelSuccessful = delegate { };
//    //seviye kaybedilice cagirilir
//    public UnityAction onLevelFailed = delegate { };
//    //button ile bir sonraki level
//    public UnityAction onNextLevel = delegate { };
//    //button ile level resetler
//    public UnityAction onRestartLevel = delegate { };
//    //*************************************
//    public UnityAction onPlay = delegate { };
//    //beli seyler restleniyor, camerapoz gibi
//    public UnityAction onReset = delegate { };
//    //Func:byte türünden bir deðeri döndüren bir metodu temsil eder, 
//    public Func<byte> onGetLevelValue = delegate { return 0; };
//    //*************************************
//    public UnityAction<byte> onStageAreaSuccessful = delegate { };
//    //asansorde durmak
//    public UnityAction onStageAreaEntered = delegate { };
//    //fnish'e carpinca
//    public UnityAction onFinishAreaEntered = delegate { };

//    public UnityAction onMiniGameAreaEntered = delegate { };
//    public UnityAction onMultiplierAreaEntered = delegate { };
//}