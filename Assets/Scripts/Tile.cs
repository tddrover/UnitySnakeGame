using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColore, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;

    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColore;
    }
}
