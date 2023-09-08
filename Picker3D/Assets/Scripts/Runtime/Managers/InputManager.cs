using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    #region Self Variables

    #region Private Variables

     private InputData _data;
     private bool _isAvailableForTouch, _isFirstTimeTouchTaken, _isTouching;

    private float _currentVelocity;
    private float3 _moveVector;
    private Vector2? _mousePosition;

    #endregion

    #endregion

    private void Awake()
    {
        _data = GetInputData();
    }

    private InputData GetInputData()
    {
        return Resources.Load<CD_Input>("Data/CD_Input").Data;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onReset += OnReset;
        InputSignals.Instance.onEnableInput += OnEnableInput;
        InputSignals.Instance.onDisableInput += OnDisableInput;
    }

    private void OnDisableInput()
    {
        _isAvailableForTouch = false;
    }

    private void OnEnableInput()
    {
        _isAvailableForTouch = true;
    }

    private void OnReset()
    {
        _isAvailableForTouch = false;
        _isTouching = false;
    }

    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onReset -= OnReset;
        InputSignals.Instance.onEnableInput -= OnEnableInput;
        InputSignals.Instance.onDisableInput -= OnDisableInput;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void Update()
    {
        if (!_isAvailableForTouch) return;

        if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
        {
            _isTouching = false;
            InputSignals.Instance.onInputReleased?.Invoke();
        }

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
        {
            _isTouching = true;
            InputSignals.Instance.onInputTaken?.Invoke();
            if (!_isFirstTimeTouchTaken)
            {
                _isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
            }

            _mousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
        {
            if (_isTouching)
            {
                if (_mousePosition != null)
                {
                    Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;
                    if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
                        _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                    else if (mouseDeltaPos.x < -_data.HorizontalInputSpeed)
                        _moveVector.x = -_data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
                    else
                        _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                            _data.ClampSpeed);

                    _moveVector.x = mouseDeltaPos.x;

                    _mousePosition = Input.mousePosition;

                    InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                    {
                        HorizontalValue = _moveVector.x,
                        ClampValues = _data.ClampValues
                    });
                }
            }
        }
    }

    private bool IsPointerOverUIElement()
    {
        var eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}











    //private InputData _data;
    ////dokunmaya uygun,ilk dokunus alindimi,dokunmus durumda
    //private bool _isAvailableForTouch, _isFirstTimeTouchTaken, _isTouching;

    //private float _currentVelocity;
    //private float3 _moveVector;
    //private Vector2? _mousePosition;


    //private void Awake()
    //{
    //    _data = GetInputData();
    //}

    //private InputData GetInputData()
    //{
    //    return Resources.Load<CD_Input>("Data/CD_Input").Data;
    //}

    //private void OnEnable()
    //{
    //    SubscribeEvents();
      
    //}

    //private void SubscribeEvents()
    //{
    //    CoreGameSignals.Instance.onReset += OnReset;
    //    InputSignals.Instance.onEnableInput += OnEnableInput;
    //    InputSignals.Instance.onDisableInput += OnDisableInput;
    //}

    //private void OnEnableInput ()
    //{
    //    _isAvailableForTouch = true;
    //} 
    //private void OnDisableInput()
    //{
    //    _isAvailableForTouch = false;
    //}
    //private void OnReset()
    //{
    //    _isAvailableForTouch = false;
    //    _isFirstTimeTouchTaken = false;
    //    _isTouching = false;

    //}

    //private void UnSubscribeEvents()
    //{
    //    CoreGameSignals.Instance.onReset -= OnReset;
    //    InputSignals.Instance.onEnableInput -= OnEnableInput;
    //    InputSignals.Instance.onDisableInput -= OnDisableInput;
    //}

    //private void OnDisable()
    //{
    //    UnSubscribeEvents();
  
    //}

    //private void Update()
    //{
    //    //dokunmaya uygun degil ise geri donder
    //    if (!_isAvailableForTouch) return;

    //    //solClick birakildiginda
    //    if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
    //    {
    //        _isTouching = false;
    //        InputSignals.Instance.onInputReleased?.Invoke();
    //    }
    //    //sol kilik yapt�g�n an
    //    if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
    //    {
    //        _isTouching = true;
    //        //isReadyToMove true oluyor
    //        InputSignals.Instance.onInputTaken?.Invoke();
    //        if (!_isFirstTimeTouchTaken)
    //        {
    //            _isFirstTimeTouchTaken = true;
    //            InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
    //        }
    //        //fare pozisyonunu alir
    //        _mousePosition = Input.mousePosition;
    //    }
    //    // sol fare d��mesine bas�l� oldu�u s�rece
    //    if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
    //    {
    //        if (_isTouching)
    //        {
    //            if (_mousePosition != null)
    //            {
    //                //o anki fare pozisyonunu al�r. Fare pozisyonu, oyun ekran�n�n sol �st k��esinden ba�layarak sa�a ve a�a��ya do�ru artan bir koordinat sistemi i�inde ifade edilen piksel cinsinden bir konumdu.
    //                // o anki fare pozisyonu ile _mousePosition i�indeki �nceki fare pozisyonu aras�ndaki fark� hesaplar.
    //                // Sonu�, bir vekt�r olarak d�ner ve bu vekt�r, fare pozisyonundaki de�i�ikli�i temsil eder.
    //                Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;
    //                if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
    //                    _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
    //                else if (mouseDeltaPos.x < -_data.HorizontalInputSpeed)
    //                    _moveVector.x = -_data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
    //                else
    //                    _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity, _data.ClampSpeed);


    //                _moveVector.x = mouseDeltaPos.x;

    //                _mousePosition = Input.mousePosition;
    //                //Debug.Log("Hiz: " + _moveVector.x);

    //                InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
    //                {
    //                    HorizontalValue = _moveVector.x,
    //                    ClampValues = _data.ClampValues
    //                });
    //            }
    //        }
    //    }
    //}

    //private bool IsPointerOverUIElement()
    //{
    //    var eventData = new PointerEventData(EventSystem.current)
    //    {
    //        position = Input.mousePosition
    //    };
    //    var results = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(eventData, results);
    //    return results.Count > 0;
    //}