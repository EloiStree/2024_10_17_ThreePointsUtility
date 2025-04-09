using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Eloi.ThreePoints {
    public class ThreePointsMono_TetraRayListRegisterFileSave : MonoBehaviour {

        public ThreePointsMono_TetraRayListRegister m_register;
        public string m_pathRelativeFolder = "tetraray_register";
        public string GetPathFile() { return 
                Path.Combine(
                    Application.persistentDataPath,
                    m_pathRelativeFolder, 
                    m_register.m_register.m_dataBaseGuid+".txt"
                ) ;
        }
        public string GetPathFolder()
        {
            return
                Path.Combine(
                    Application.persistentDataPath,
                    m_pathRelativeFolder
                );
        }
        public bool m_useEnable;
        public void OnEnable()
        {
            if (m_useEnable)
            {
                Load();
            }
        }
        public void OnDisable()
        {
            if (m_useEnable)
            {
                Save();
            }
        }
        [ContextMenu("Save")]
        public void Save()
        {
            if (m_register == null)
                return;
            //SHOULD BE STORE IN MM CSV TO COMPRESS A BIT THE ALL BUT JSON IS GOOD ENOUGH FOR NOW
            string dirPath = GetPathFolder();
           string filePath = GetPathFile();
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            SaveTetraRayList list = new SaveTetraRayList();
            list.m_listTetraRay = m_register.m_register.m_listTetraRay;
            File.WriteAllText(filePath,JsonUtility.ToJson(list));

            Debug.Log($"Save TetraRay to {list.m_listTetraRay.Count}: "+filePath);
        }
        [ContextMenu("Delete Save")]
        public void DeleteSaveFile()
        {
            string filePath = GetPathFile();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log($"Delete TetraRay: " + filePath);
            }
        }



        [ContextMenu("Load")]
        public void Load()
        {
            if (m_register == null)
                return;

            //SHOULD BE STORE IN MM CSV TO COMPRESS A BIT THE ALL BUT JSON IS GOOD ENOUGH FOR NOW
            string filePath = GetPathFile();
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath); 
                SaveTetraRayList list = JsonUtility.FromJson<SaveTetraRayList>(text);
                m_register.m_register.m_listTetraRay.AddRange(list.m_listTetraRay);
                Debug.Log($"Load TetraRay from {list.m_listTetraRay.Count}: " + filePath);
            }
        }

      

        [System.Serializable]
        public class SaveTetraRayList { 
        
            public List<STRUCT_TetraRayWithWorld> m_listTetraRay = new List<STRUCT_TetraRayWithWorld>();
        }
    }
}

