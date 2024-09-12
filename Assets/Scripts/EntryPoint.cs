using UnityEngine;

public class EntryPoint : MonoBehaviour
{  
    [Header("Установить в инспекторе")]
    [SerializeField] private PlayerMoveController _playerMoveController;
    [SerializeField] private GameManager _gameManager;

    private void Awake()
    {
        _playerMoveController.Initializate();
        _gameManager.Initialize(_playerMoveController.MoveState);
    }
}
