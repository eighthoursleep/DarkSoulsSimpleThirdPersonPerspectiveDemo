using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : SingletonMono<CameraMgr>
{
    public Transform cameraView;
    public Transform target;
    public float distance;
    public float rotateVelocity;
    public float zoomInStep;
    public float zoomInValue;

    public bool canZoom;

    private GameObject cameraArm;

    public Vector3 CameraForward
    {
        get
        {
            return transform.forward;
        }
    }
    
    private new void Awake()
    {
        base.Awake();
        transform.rotation = target.rotation;
        cameraArm = new GameObject();
        cameraArm.name = "CameraArm";
        cameraArm.transform.SetParent(transform);
        cameraArm.transform.rotation = transform.rotation;
    }

    void FixedUpdate()
    {
        FlowTarget();
        RotateX(InputMgr.Instance.mouseX);
        RotateY(InputMgr.Instance.mouseY);
        if (canZoom)
        {
            ZoomIn(InputMgr.Instance.mouseScroll);
        }
        UpdateCameraPos(Time.fixedTime);
    }

    void RotateX(float value)
    {
        if (value >= 0.01 || value <= -0.01)
        {
            transform.Rotate( Vector3.up, value * rotateVelocity);
        }
    }
    
    void RotateY(float value)
    {
        if (value >= 0.01 || value <= -0.01)
        {
            float angle = value * rotateVelocity;
            Quaternion currentRot = cameraArm.transform.localRotation;
            Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.right) * currentRot;
            if (targetRot.eulerAngles.x >= 5.0f && targetRot.eulerAngles.x <= 80.0f)
            {
                cameraArm.transform.localRotation = targetRot;
            }
        }
    }

    void ZoomIn(float value)
    {
        if (value != 0.0f)
        {
            zoomInValue += value * zoomInStep;
            zoomInValue = zoomInValue < 3 ? 3: zoomInValue;
            zoomInValue = zoomInValue > 10 ? 10: zoomInValue;
        }
    }

    void FlowTarget()
    {
        transform.position = target.position;
    }

    void UpdateCameraPos(float delta)
    {
        cameraView.position = cameraArm.transform.position;
        cameraView.forward = cameraArm.transform.forward;
        cameraView.position += (-cameraArm.transform.forward.normalized * (distance + zoomInValue));
    }
}
