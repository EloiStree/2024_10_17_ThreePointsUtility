using UnityEngine;
namespace Eloi.ThreePoints
{
    /// <summary>
    /// I am a class that save the guardian triangle shape with the relocation of the transformt target.
    /// If the triangle is found some days later. It reload the relocation and apply it.
    /// </summary>
    public class ThreePointsMono_GuardianSaveAndReloadTransform : MonoBehaviour{

        public ThreePointsMono_Transform3 m_currentGuardianTriangle;
        public ThreePointsTriangleDefault m_currentValueOfGuardian;
        public Transform m_transformToReload;

        public void SaveCurrentLocationAsFile() {
        
        }
        public void LoadTriangleToRelocationFilesOnDevices() {
        
        }
    }
}