using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    private GameManager _gm;

    [Header("Установить в инспекторе")]
    [SerializeField] private CharacterController _characterController;
   //[SerializeField] private BuildingController _buildingController;
    public CharacterController CharacterController { get => _characterController;}
    public float SpeedMove = 15f;
    [SerializeField]  private LayerMask _castLayer;

    public MoveState MoveState { get; private set; }
    public BuildingState BuildingState { get; private set; }


    void Start()
    {
        _gm = GameManager.Instatince;        
    }

    public void Initializate()
    {
        MoveState = new(this);
        BuildingState = new(this, _castLayer);
    }

    void Update()
    {
        _gm.StateMachine.CurrentState.Update();  
    }
}
