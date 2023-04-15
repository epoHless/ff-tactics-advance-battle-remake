using Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera TopDownCamera;
    [SerializeField] private CinemachineFreeLook StatusCamera;
    
    public CinemachineVirtualCameraBase ActiveCamera { get; private set; }

    public void ToggleCamera(ECameraType _type)
    {
        if (ActiveCamera) ActiveCamera.m_Priority = 10;
        ActiveCamera = _type == ECameraType.TOPDOWN ? TopDownCamera : StatusCamera;

        ActiveCamera.m_Priority = 20;
    }

    public void SetFollowObject(Transform _transform)
    {
        ActiveCamera.Follow = _transform;
        ActiveCamera.LookAt = _transform;
    }
}

public enum ECameraType
{
    TOPDOWN,
    STATUS
}