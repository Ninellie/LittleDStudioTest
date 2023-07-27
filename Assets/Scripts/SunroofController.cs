using System;
using DG.Tweening;
using UnityEngine;

public class SunroofController : MonoBehaviour, ISunroofController
{
    [SerializeField]
    [Min(0)]
    private float _openingDuration;

    [SerializeField]
    [Min(0)]
    private float _closingDuration;

    [SerializeField]
    private Vector2 _closedPosition;

    [SerializeField]
    private Vector2 _openedPosition;

    [SerializeField]
    private SunroofState _initialState = SunroofState.closed;

    private void Start()
    {
        transform.position = _initialState switch
        {
            SunroofState.closed => _closedPosition,
            SunroofState.opened => _openedPosition,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void Close()
    {
        transform.DOKill(false);
        transform.DOMove(_closedPosition, _closingDuration);
    }

    public void Open()
    {
        transform.DOKill(false);
        transform.DOMove(_openedPosition, _openingDuration);
    }
}