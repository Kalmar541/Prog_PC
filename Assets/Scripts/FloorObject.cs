using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Напольный обьект
/*
 * При создании обьекта расположите его центр(Pivot) внизу обьекта, этой точкой он будет устанавливаться.
 * BoxCollider задайте так что бы он охватывал модель, его размер и центр будет учитаваться при позиционировании.
 * Scale этого обьекта должен быть 1. Дочерние объекты могут быть любых рамеров, лижбы они входили в BoxCollider.
 */
public class FloorObject : BuildingObject
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private LayerMask _layerCollision;
    private GameManager _gm;
    private Color _defaultColor;

    void Start()
    {
        _gm = GameManager.Instatince;
        _defaultColor = _meshRenderer.material.color;
    }

    public override void Put()
    {
        SetModeOpaque();
        SetMaterialDefault();
        _boxCollider.isTrigger = false;
    }

    public override void Take()
    {
        SetModeTransparent();
        _boxCollider.isTrigger = true;
    }

    public override Vector3 GetSize()
    {
        return _boxCollider.size;
    }

    private void SetModeTransparent()
    {       
        ChangeModeMaterial.ChangeRenderMode(_meshRenderer.material, ChangeModeMaterial.BlendMode.Transparent);
    }

    private void SetModeOpaque()
    {
        ChangeModeMaterial.ChangeRenderMode(_meshRenderer.material, ChangeModeMaterial.BlendMode.Opaque);
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
        if (transform.rotation.eulerAngles.x == 0
            && transform.rotation.eulerAngles.z == 0)
        {
            return (Physics.CheckBox(transform.position + _boxCollider.center, _boxCollider.size * 0.49f, transform.rotation, _layerCollision, QueryTriggerInteraction.Ignore));          
        }
        else
        {
            return true;
        }
    }
}
