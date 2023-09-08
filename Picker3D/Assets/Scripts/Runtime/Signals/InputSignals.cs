using UnityEngine.Events;

//public class InputSignals : MonoBehaviour
//{
//public static InputSignals Instance;
////*****************************************
//public UnityAction onFirstTimeTouchTaken = delegate { };
////Alýnan Giriþte, hareket edebilirlik
//public UnityAction onInputTaken = delegate { };
////giris serbest birakildiginda
//public UnityAction onInputReleased = delegate { };
////giris etkinlestirirlir
//public UnityAction onEnableInput = delegate { };
////giris devradisi birakilir 
//public UnityAction onDisableInput = delegate { };

//public UnityAction<HorizontalInputParams> onInputDragged = delegate { };
public class InputSignals : MonoSingleton<InputSignals>
{
    public UnityAction onEnableInput = delegate { };
    public UnityAction onDisableInput = delegate { };
    public UnityAction onFirstTimeTouchTaken = delegate { };
    public UnityAction onInputTaken = delegate { };
    public UnityAction onInputReleased = delegate { };
    public UnityAction<HorizontalInputParams> onInputDragged = delegate { };
}
