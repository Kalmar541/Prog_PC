using UnityEngine;

public abstract class BuildingObject : MonoBehaviour
{
    [SerializeField] private LayerMask _locatedLayer;
    public LayerMask LocatedLayer { get=> _locatedLayer; }

    [SerializeField] private float _maxDistanceFromPlayer = 5f; // расстояния когда объект в воздухе
    public float MaxDistanceFromPlayer { get => _maxDistanceFromPlayer; }

    public abstract void Put();

    public abstract void Take();
     
    public abstract Vector3 GetSize();

    public abstract void SetMaterialGreen();
    public abstract void SetMaterialRed();
    public abstract void SetMaterialTransparent();
    public abstract void SetMaterialDefault();
    public abstract bool CheckCollision();
}
