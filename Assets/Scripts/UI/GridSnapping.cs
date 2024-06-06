using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SnapToGridInEditor : MonoBehaviour
{
    [SerializeField] private Vector2 m_GridSize = new(0.5f, 0.5f);

    private void Update()
    {
        var pos = transform.position;
        transform.position = new Vector2
            (
            Mathf.RoundToInt(pos.x / m_GridSize.x) * m_GridSize.x,
            Mathf.RoundToInt(pos.y / m_GridSize.y) * m_GridSize.y
            );
    }
}