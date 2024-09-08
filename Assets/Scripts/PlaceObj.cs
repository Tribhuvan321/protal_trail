using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObj : MonoBehaviour
{
    public GameObject obj;
    private GameObject spawnedObj;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>(); 

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }
    bool ToGetTouchPos(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        else
        {
            touchPosition = default;
            return false;
        }
    }

    void Update()
    {
        if(!ToGetTouchPos(out Vector2 touchPosition))
        {
            return;
        }
        else
        {
            if(_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
                if (spawnedObj == null)
                {
                    spawnedObj = Instantiate(obj, hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnedObj.transform.position = hitPose.position;
                }
            }
            
        }
    }
}
