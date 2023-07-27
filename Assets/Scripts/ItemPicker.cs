using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    [SerializeField]
    private string _itemLayerName = "Item";

    [SerializeField]
    [Min(0)]
    private float _pickupRadius;

    [SerializeField]
    private GameObject _item;

    private Vector2 PickupAreaSize => new(_pickupRadius, _pickupRadius);
    private bool _isHolding = false;
    private LayerMask _itemLayer;

    private void Start()
    {
        _itemLayer = LayerMask.GetMask(_itemLayerName);
    }

    private void FixedUpdate()
    {
        if (_isHolding)
            return;

        _item = TryGetNearestItem();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, PickupAreaSize);
    }

    public void OnTakeItem()
    {
        if (_item == null)
            return;

        _isHolding = true;
        _item.SetActive(false);
    }

    public void OnDropItem()
    {
        if (_item == null)
            return;

        var pos = transform.position;
        _item.transform.position = pos;
        _item.SetActive(true);
        _isHolding = false;
    }

    private GameObject TryGetNearestItem()
    {
        var x = transform.position.x;
        var y = transform.position.y;
        var origin = new Vector2(x, y);
        var hit = Physics2D.OverlapBox(origin, PickupAreaSize, 0f, _itemLayer);
        return hit == null ? null : hit.gameObject;
    }

}