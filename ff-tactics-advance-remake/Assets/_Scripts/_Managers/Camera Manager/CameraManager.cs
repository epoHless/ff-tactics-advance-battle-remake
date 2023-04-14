using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = FinalFantasy.InputSystem;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera TopDownCamera;
    [SerializeField] private CinemachineFreeLook StatusCamera;
    
    public CinemachineVirtualCameraBase ActiveCamera { get; private set; }


    private void Update()
    {
        StatusCamera.m_XAxis.Value += InputSystem.CameraVector.x;
    }

    public void ToggleCamera(ECameraType _type)
    {
        if (ActiveCamera) ActiveCamera.m_Priority = 10;
        
        ActiveCamera = _type == ECameraType.TOPDOWN ? TopDownCamera : StatusCamera;
        
        ActiveCamera.m_Priority = 20;
    }
}

public enum ECameraType
{
    TOPDOWN,
    STATUS
}