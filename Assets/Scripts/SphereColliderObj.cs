using UnityEngine;

//������ ������ ������������
/*
 * ��� �������� ������� ����������� ��� �����(Pivot) ����� �������, ���� ������ �� ����� ���������������.
 * SphereCollider ������� ��� ��� �� �� ��������� ������, ��� ������ � ����� ����� ����������� ��� ����������������.
 * Scale ����� ������� ������ ���� 1. �������� ������� ����� ���� ����� �������, ����� ��� ������� � SphereCollider.
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
