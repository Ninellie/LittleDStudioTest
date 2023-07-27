using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _indicator;

    [SerializeField]
    private Color _enabledColor;

    [SerializeField]
    private Color _disabledColor;

    private readonly List<GameObject> _pressingObjects = new(2);
    private ISunroofController _sunroof;

    private void Awake()
    {
        _sunroof = GetComponentInChildren<ISunroofController>();
    }

    private void Start()
    {
        _indicator.color = _disabledColor;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _indicator.color = _enabledColor;

        if (_pressingObjects.Contains(collider.gameObject))
            return;

        _pressingObjects.Add(collider.gameObject);

        _sunroof.Open();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        _pressingObjects.Remove(collider.gameObject);

        if (_pressingObjects.Count != 0) return;

        _sunroof.Close();
        _indicator.color = _disabledColor;
    }
}