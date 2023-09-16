using Unity.Mathematics;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private new Rigidbody rigidbody;

    #endregion

    #region Private Variables

     private PlayerMovementData _data;
     private bool _isReadyToMove, _isReadyToPlay,_miniGameSpeed;
     private float _xValue;
    private float _forwardSpeed;
    private float2 _clampValues;

    #endregion

    #endregion

    internal void SetData(PlayerMovementData data)
    {
        _data = data;
    }

    private void FixedUpdate()
    {
        _forwardSpeed = _data.ForwardSpeed;
        if(_miniGameSpeed)
            _forwardSpeed = _data.ForwardSpeed+15;

        if (!_isReadyToPlay)
        {
            StopPlayer();
            return;
        }
        Debug.Log("currentTotal: " + _forwardSpeed);
        if (_isReadyToMove)
        {
            MovePlayer();
        }
        else
        {
            StopPlayerHorizontally();
        }
    }

    private void StopPlayer()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    private void StopPlayerHorizontally()
    {
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _forwardSpeed);
        rigidbody.angularVelocity = Vector3.zero;
    }

    private void MovePlayer()
    {
        var velocity = rigidbody.velocity;
        velocity = new Vector3(_xValue * _data.SidewaySpeed, velocity.y, _forwardSpeed);
        rigidbody.velocity = velocity;
        var position1 = rigidbody.position;
        Vector3 position;
        position = new Vector3(Mathf.Clamp(position1.x, _clampValues.x, _clampValues.y),
            (position = rigidbody.position).y, position.z);
        rigidbody.position = position;
        
    }
    internal void IsReadyToPlay(bool condition)
    {
        _isReadyToPlay = condition;
    }

    internal void IsReadyToMove(bool condition)
    {
        _isReadyToMove = condition;
    }  
    internal void MiniGameSpeed(bool condition)
    {
        _miniGameSpeed = condition;
    }

    internal void UpdateInputParams(HorizontalInputParams inputParams)
    {
        _xValue = inputParams.HorizontalValue;
        _clampValues = inputParams.ClampValues;
    }

    internal void OnReset()
    {
        StopPlayer();
        _isReadyToMove = false;
        _isReadyToPlay = false;
    }
}
