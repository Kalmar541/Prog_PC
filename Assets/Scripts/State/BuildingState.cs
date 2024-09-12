using UnityEngine;

public class BuildingState : State
{
    private PlayerMoveController _playerMoveController;
    private BuildingObject _buildingObject;
    private bool _isCanPut;
    private LayerMask _castLayer;
    private float _distanceTake = 100f;
    private float _distancePut = 50f;

    public BuildingState(PlayerMoveController player,LayerMask castLayer)
    {
        _playerMoveController = player;
        _castLayer = castLayer;
    }

    public override void Enter()
    {
        base.Enter();
        TryTakeObject();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void Exit()
    {
        base.Exit();
    }

    private Vector3 _input;
    private Vector3 _directionMove;
    public override void Update()
    {
        base.Update();
        _input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        _directionMove = _playerMoveController.transform.right * _input.x + _playerMoveController.transform.forward * _input.z;
        _playerMoveController.CharacterController.Move(_directionMove * _playerMoveController.SpeedMove * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            if (_buildingObject == null)
            {
                TryTakeObject();
            }
            else
            {
                PutObject();
            }
        }

        if (_buildingObject != null)
        {
            TryPutObject();

            if (Input.mouseScrollDelta.y != 0)
            {            
                Vector3 rotation = _buildingObject.gameObject.transform.rotation.eulerAngles;             
                rotation.y += 45f * Input.mouseScrollDelta.y;
                Vector3 rotationScroll = Quaternion.AngleAxis(rotation.y, _buildingObject.gameObject.transform.up).eulerAngles;
                rotationScroll.x = rotation.x;
                rotationScroll.z = rotation.z;
                _buildingObject.gameObject.transform.rotation = Quaternion.Euler(rotationScroll);
            }
        }
    }

    private void TryTakeObject()
    {
        if (Physics.Raycast(GameManager.Instatince.ActiveCamera.transform.position, GameManager.Instatince.ActiveCamera.transform.forward, out RaycastHit hitInfo, _distanceTake))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out BuildingObject buildingObject))
            {
                buildingObject.Take();
                _buildingObject = buildingObject;              
            }
        }
    }

    private void TryPutObject()
    {
        if (Physics.Raycast(GameManager.Instatince.ActiveCamera.transform.position, GameManager.Instatince.ActiveCamera.transform.forward, out RaycastHit hitInfo, _distancePut, _castLayer, QueryTriggerInteraction.Ignore))
        {
            //На поверхности
            _buildingObject.transform.position = hitInfo.point;

            Vector3 currentRotation = _buildingObject.transform.rotation.eulerAngles;
            Vector3 normalRotation = Quaternion.FromToRotation(_playerMoveController.transform.up, hitInfo.normal).eulerAngles;
            normalRotation.y = currentRotation.y;
            _buildingObject.transform.rotation = Quaternion.Euler(normalRotation);

            //Окрасим объект в руках
            if (_buildingObject.CheckCollision())
            {
                _buildingObject.SetMaterialRed();
                _isCanPut = false;
            }
            else
            {
                if (((_buildingObject.LocatedLayer) & (1 << hitInfo.collider.gameObject.layer)) > 0)
                {
                    _buildingObject.SetMaterialGreen();
                    _isCanPut = true;
                }
                else
                {
                    _buildingObject.SetMaterialRed();
                    _isCanPut = false;
                }
            }
        }
        else
        {
            //В воздухе
            _buildingObject.transform.position = GameManager.Instatince.ActiveCamera.transform.position + GameManager.Instatince.ActiveCamera.transform.forward * 5f;
            _buildingObject.SetMaterialTransparent();
            _isCanPut = false;
        }
    }

    private void PutObject()
    {
        if (_isCanPut)
        {
            _buildingObject.Put();
            _buildingObject = null;
            GameManager.Instatince.StateMachine.ChangeState(_playerMoveController.MoveState);
        }
    }
}
