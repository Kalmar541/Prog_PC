using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instatince;

    [Header("Установить в инспекторе")]
    [Header("Цвета окрашивания строительных объектов")]
    [SerializeField] private Color _greenColor;
    public Color GreenColor { get => _greenColor;}
    [SerializeField] private Color _redColor;
    public Color RedColor { get => _redColor; }
    [SerializeField] private Color _transparentColor;
    public Color TransparentColor { get => _transparentColor; }

    public Camera ActiveCamera { get; set; }

    public StateMachine StateMachine { get; private set; }
    
    void Awake()
    {
        if (Instatince == null)
        {
            Instatince = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); 
    }

    public void Initialize( State startState)
    {
        StateMachine = new();
        StateMachine.Initialize(startState);
        ActiveCamera = Camera.main;
    }
}
