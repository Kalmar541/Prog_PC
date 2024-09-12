using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Установить в инспекторе")]
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _player;
    [SerializeField] private float mouseSensX = 250f;
    [SerializeField] private float mouseSensY = 250f;

    private float _mouseX;
    private float _mouseY;
    private float _rotateX;

    void Update()
    {
        _mouseX = Input.GetAxis("Mouse X") * mouseSensX * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * mouseSensY * Time.deltaTime;

        _rotateX -= _mouseY;
        _camera.localRotation = Quaternion.Euler(_rotateX, 0f, 0f);
        _rotateX = Mathf.Clamp(_rotateX, -90f, 90f);

        _player.Rotate(Vector3.up * _mouseX);
    }
}
