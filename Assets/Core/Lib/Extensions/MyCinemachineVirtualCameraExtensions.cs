using Cinemachine;
using UnityEngine;

namespace Lib
{
    public static class MyCinemachineVirtualCameraExtensions
    {
        public static void ForceSetPosition(this CinemachineFramingTransposer transpose, Vector3 position)
        {
            transpose.VirtualCamera.PreviousStateIsValid = false;
            var virtualCamera = transpose.VirtualCamera;
            virtualCamera.ForceCameraPosition(
                position + transpose.m_TrackedObjectOffset + Vector3.back * transpose.m_CameraDistance,
                virtualCamera.transform.rotation);
        }

        public static void ForceSetPosition(this CinemachineTransposer transpose, Vector3 position)
        {
            transpose.VirtualCamera.PreviousStateIsValid = false;
            var virtualCamera = transpose.VirtualCamera;
            virtualCamera.ForceCameraPosition(
                position + transpose.m_FollowOffset,
                virtualCamera.transform.rotation);
        }
    }
}