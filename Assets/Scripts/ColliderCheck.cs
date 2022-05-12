using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColliderCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _layers;

    private Collider2D _collider;

    public bool IsTouchingLayer { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsTouchingLayer = _collider.IsTouchingLayers(_layers);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsTouchingLayer = _collider.IsTouchingLayers(_layers);
    }
}
