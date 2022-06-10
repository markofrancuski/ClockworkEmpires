using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private delegate void ListenInput();

    [SerializeField] private float _panSpeed;
    [SerializeField] private float _panBorderThickness;
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _enableBorderCheck = false;
    [Space]
    [SerializeField] private Vector2 _positionLimit;
    [SerializeField] private Vector2 _scrollLimit;
    [SerializeField] private Vector2 _panSpeedLimit;
    [Space]
    [SerializeField] private Transform _target;

    [SerializeField] private Vector3 _originalPos;
    [SerializeField] private float _originalHeight;
    [SerializeField] private Quaternion _originalRotation;

    private ListenInput InputMethod;

    #region Unity Methods

    private void Start()
    {
        _originalPos = transform.position;
        _originalRotation = transform.rotation;
        _originalHeight = _target.transform.localPosition.y;

        Subscribe();
    }

    private void Update()
    {
        if (InputMethod != null)
        {
            InputMethod();
        }
    }

    private void OnDrawGizmos()
    {
        return;
        //Debug.DrawLine(transform.position, _target.position, Color.red, 0.5f);

        Vector3 targetPosition = _target.position;

        Vector3 scrollLimitMin = targetPosition;
        scrollLimitMin.y = _scrollLimit.x;

        Vector3 scrollLimitMax = targetPosition;
        scrollLimitMax.y = _scrollLimit.y;

        //Debug.DrawLine(scrollLimitMax, scrollLimitMin, Color.yellow, 1f);
    }

    #endregion Unity Methods

    #region Private Methods
    private void UpdateInput()
    {
        Vector3 position = transform.position;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            transform.position = _originalPos;
            transform.rotation = _originalRotation;
            Vector3 localPos = _target.localPosition;
            localPos.y = _originalHeight;
            _target.localPosition = localPos;
            return;
        }

        _panSpeed = _panSpeedLimit.x;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _panSpeed = _panSpeedLimit.y;
        }

        if (CheckForwardInput())
        {
            position.z += _panSpeed * Time.deltaTime;
        }

        if (CheckBackwardInput())
        {
            position.z -= _panSpeed * Time.deltaTime;
        }

        if (CheckRightInput())
        {
            position.x += _panSpeed * Time.deltaTime;
        }

        if (CheckLeftInput())
        {
            position.x -= _panSpeed * Time.deltaTime;
        }

        position.x = Mathf.Clamp(position.x, -_positionLimit.x, _positionLimit.x);
        position.z = Mathf.Clamp(position.z, -_positionLimit.y, _positionLimit.y);
        transform.position = position;

        //CheckRotation();

        CheckScroll();
    }

    private bool CheckForwardInput()
    {
        bool valid = Input.GetKey("w");

        if (_enableBorderCheck)
        {
            valid |= Input.mousePosition.y >= Screen.height - _panBorderThickness;
        }

        return valid;
    }
    private bool CheckBackwardInput()
    {
        bool valid = Input.GetKey("s");

        if (_enableBorderCheck)
        {
            valid |= Input.mousePosition.y <= _panBorderThickness;
        }

        return valid;
    }
    private bool CheckRightInput()
    {
        bool valid = Input.GetKey("d");

        if (_enableBorderCheck)
        {
            valid |= Input.mousePosition.x >= Screen.width - _panBorderThickness;
        }
        return valid;
    }
    private bool CheckLeftInput()
    {
        bool valid = Input.GetKey("a");
        if (_enableBorderCheck)
        {
            valid |= Input.mousePosition.x <= _panBorderThickness;
        }
        return valid;
    }
    private void CheckScroll()
    {
        Vector3 targetPos = _target.localPosition;
        float scroll = Input.mouseScrollDelta.y;

        targetPos.y += scroll * _scrollSpeed * 100f * Time.deltaTime;
        targetPos.y = Mathf.Clamp(targetPos.y, _scrollLimit.x, _scrollLimit.y);
        _target.localPosition = targetPos;
    }
    private void CheckRotation()
    {
        bool rotateLeft = Input.GetKey(KeyCode.Q);
        bool rotateRight = Input.GetKey(KeyCode.E);
        if (rotateLeft ^ rotateRight)
        {
            float rotationAmount = rotateLeft ? _rotationSpeed : -_rotationSpeed;
            Quaternion rotation = transform.rotation * Quaternion.Euler(Vector3.up *  rotationAmount );
            transform.rotation = rotation;
        }

    }

    private void Subscribe()
    {
        InputMethod = UpdateInput;
    }
    private void UnSubscribe()
    {
        InputMethod = null;
    }

    #endregion Private Methods
}
