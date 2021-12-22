using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : SingletonMono<PlayerMgr>
{
    public float velocity = 10.0f;

    public GameObject player;
    private Transform playerTransform;
    public Vector3 targetDirection = new Vector3();

    private new void Awake()
    {
        base.Awake();
        player = GameObject.Find("Player");
        playerTransform = player.transform;
    }

    private void Update()
    {
        Tick(Time.deltaTime);
    }

    private void Tick(float deltaTime)
    {
        UpdateTargetDirection(InputMgr.Instance.horizontal, InputMgr.Instance.vertical);
        UpdateRotation();
        UpdatePosition(deltaTime);
    }
    
    private void UpdatePosition(float deltaTime)
    {
        if (InputMgr.Instance.horizontal != 0.0f || InputMgr.Instance.vertical != 0.0f)
        {
            float inputVertical = targetDirection.magnitude;
            playerTransform.position += playerTransform.forward.normalized * (inputVertical * velocity * deltaTime);
            //playerTransform.position += playerTransform.right.normalized * (horizontal * velocity * deltaTime);
        }
    }
    private void UpdateRotation()
    {
        if (InputMgr.Instance.horizontal != 0.0f || InputMgr.Instance.vertical != 0.0f)
        {
            Quaternion targetRot = Quaternion.LookRotation(targetDirection,playerTransform.up);
            Quaternion currentRot = playerTransform.localRotation;
            Quaternion rot = Quaternion.Slerp(targetRot,currentRot,0.1f);
            playerTransform.localRotation = rot;
        }
    }

    private void UpdateTargetDirection(float horizontal, float vertical)
    {
        Vector3 camForward = CameraMgr.Instance.transform.forward;
        Vector3 camTarget = playerTransform.position + (camForward * 10.0f);
        Debug.DrawLine(playerTransform.position,camTarget, Color.red);

        Vector3 playerTarget = playerTransform.position + (playerTransform.forward * 10.0f);
        Debug.DrawLine(playerTransform.position,playerTarget, Color.yellow);

        Vector3 v = CameraMgr.Instance.transform.forward * vertical;
        Vector3 h = CameraMgr.Instance.transform.right * horizontal;
        Vector3 inputDir = (v + h).normalized;
        Vector3 inputTarget = playerTransform.position + (inputDir * 10.0f);
        Debug.DrawLine(playerTransform.position,inputTarget, Color.blue);
        if (inputDir == Vector3.zero)
        {
            inputDir = camForward;
        }
        targetDirection = inputDir;
    } 
}