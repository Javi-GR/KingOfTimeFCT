using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapBlipScript : MonoBehaviour
{
    public Transform target;
    public float zoomLevel = 20f;


    public Vector2 TransformPosition(Vector3 position)
    {
        Vector3 offset = position - target.position;
        Vector2 newPosition = new Vector2(offset.x, offset.z);
        newPosition *= zoomLevel;
        return newPosition;
    }

    public Vector2 MoveInsideMinimap(Vector2 point)
    {
        Rect mapRect = GetComponent<RectTransform>().rect;
        point = Vector2.Max(point, mapRect.min);
        point = Vector2.Min(point, mapRect.max);
        return point;
    }
}
