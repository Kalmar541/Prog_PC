using UnityEngine;

//Обьект любого расположения
/*
 * При создании обьекта расположите его центр(Pivot) внизу обьекта, этой точкой он будет устанавливаться.
 * SphereCollider задайте так что бы он охватывал модель, его размер и центр будет учитаваться при позиционировании.
 * Scale этого обьекта должен быть 1. Дочерние объекты могут быть любых рамеров, лижбы они входили в SphereCollider.
 */
public class SphereColliderObj : BuildingObject
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private SphereCollider _sphereCollider;

    private GameManager _gm;
    private Color _defaultColor;
    [SerializeField] private LayerMask _layerCollision;

    void Start()
    {
        _gm = GameManager.Instatince;
        _defaultColor = _meshRenderer.material.color;
    }

    public override void Put()
    {
        SetMaterialDefault();
        _sphereCollider.isTrigger = false;
    }

    public override void Take()
    {
         SetModeMaterialAsTransparent();
        _sphereCollider.isTrigger = true;
    }

    public override Vector3 GetSize()
    {
        return Vector3.one* _sphereCollider.radius;
    }

    private void SetModeMaterialAsTransparent()
    {
        ChangeModeMaterial.ChangeRenderMode(_meshRenderer.material, ChangeModeMaterial.BlendMode.Transparent);
    }

    public override void SetMaterialGreen()
    {
        _meshRenderer.material.color = _gm.GreenColor;
    }

    public override void SetMaterialRed()
    {
        _meshRenderer.material.color = _gm.RedColor;
    }

    public override void SetMaterialTransparent()
    {
        _meshRenderer.material.color = _gm.TransparentColor;
    }

    public override void SetMaterialDefault()
    {
        _meshRenderer.material.color = _defaultColor;
    }

    public override bool CheckCollision()
    {
        return  Physics.CheckSphere(transform.position + Quaternion.Euler(transform.rotation.eulerAngles) * _sphereCollider.center, _sphereCollider.radius * 0.99f, _layerCollision, QueryTriggerInteraction.Ignore);
    }
}
