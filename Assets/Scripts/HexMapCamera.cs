using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMapCamera : MonoBehaviour
{
    Transform swivel, stick;

    private float zoom = 1f;

    public float stickMinZoom;
    public float stickMaxZoom;

    public float swivelMinZoom;
    public float swivelMaxZoom;

    public float moveSpeedMinZoom;
    public float moveSpeedMaxZoom;

    public float rotationSpeed;
    private float rotationAngle;

    public HexGrid grid;


    private void Awake()
    {
        swivel = transform.GetChild(0);
        stick = swivel.GetChild(0);
    }

    private void Update()
    {
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
        if(zoomDelta != 0f)
        {
            AdjustZoom(zoomDelta);
        }

        float rotationDelta = Input.GetAxis("Rotation");
        if(rotationDelta != 0)
        {
            AdjustRotation(rotationDelta);
        }

        float xDelta = Input.GetAxis("Horizontal");
        float zDelta = Input.GetAxis("Vertical");
        if(xDelta != 0 || zDelta != 0)
        {
            AdjustPosition(xDelta, zDelta);
        }
    }

    private void AdjustRotation(float rotationDelta)
    {
        rotationAngle += rotationDelta * rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);

        if(rotationAngle <= 0f)
        {
            rotationAngle += 360f;
        }else if(rotationAngle >= 360f)
        {
            rotationAngle -= 360f;
        }
        
        //throw new NotImplementedException();
    }

    private void AdjustPosition(float xDelta, float zDelta)
    {
        Vector3 direction = transform.localRotation * new Vector3(xDelta, 0, zDelta).normalized;
        float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));
        float distance = Mathf.Lerp(moveSpeedMinZoom, moveSpeedMaxZoom, zoom) * damping * Time.deltaTime;
        //print(SystemInfo.graphicsDeviceName);
        Vector3 position = transform.localPosition;
        position += direction * distance;
        transform.localPosition = Clamp(position);
        
        //throw new NotImplementedException();
    }

    private Vector3 Clamp(Vector3 position)
    {
        float xMax = (grid.chunkCountX * HexMetrics.chunkSizeX - 0.5f) * (2f * HexMetrics.innerRadius);
        position.x = Mathf.Clamp(position.x, 0, xMax);
        float zMax = (grid.chunkCountZ * HexMetrics.chunkSizeZ - 1f) * (1.5f * HexMetrics.outerRadius);
        position.z = Mathf.Clamp(position.z, 0, zMax);
        return position;
        //throw new NotImplementedException();
    }

    private void AdjustZoom(float delta)
    {
        zoom = Mathf.Clamp01(zoom + delta);
        float distance = Mathf.Lerp(stickMinZoom, stickMaxZoom, zoom);
        stick.localPosition = new Vector3(0, 0, distance);

        float angle = Mathf.Lerp(swivelMinZoom, swivelMaxZoom, zoom);
        swivel.localRotation = Quaternion.Euler(angle, 0f, 0f);
        //throw new NotImplementedException();
    }
}
