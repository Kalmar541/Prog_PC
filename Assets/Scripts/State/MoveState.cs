using UnityEngine;

public class MoveState : State
{
    private PlayerMoveController _playerMoveController;
    private float _distanceTake = 100f;
    private Camera _cameraPlayer;

    public MoveState (PlayerMoveController player)
    {
        _playerMoveController = player;
        _cameraPlayer = Camera.main;
    }
    public override void Enter()
    {
        base.Enter();
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
            if (Physics.Raycast(_cameraPlayer.transform.position, _cameraPlayer.transform.forward, out RaycastHit hit, _distanceTake))
            {
                if (hit.collider.gameObject.TryGetComponent(out BuildingObject buildingObject))
                {
                    GameManager.Instatince.StateMachine.ChangeState(_playerMoveController.BuildingState);                                   
                }
            }
        }
    }
}
