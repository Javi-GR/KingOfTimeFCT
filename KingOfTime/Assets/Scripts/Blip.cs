using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blip : MonoBehaviour
{
    public Transform target;

    public bool keepInBounds = true;
    public bool lockScale = false;
    MinimapBlipScript map;

    RectTransform myRectTransform;
    void Start()
    {
        map = GetComponentInParent<MinimapBlipScript>();
        myRectTransform = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        Vector2 newPosition = map.TransformPosition(target.position);
        if(keepInBounds)
            newPosition = map.MoveInsideMinimap(newPosition);
        if(!lockScale)
            myRectTransform.localScale  = new Vector3(map.zoomLevel, map.zoomLevel, 1);
        myRectTransform.localPosition = newPosition;
    }
    public void DestroyedEnemy()
    {
        Destroy(gameObject);
    }
}
