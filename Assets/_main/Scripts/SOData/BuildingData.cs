using System;
using Buildings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SOData
{
    [HideMonoScript]
    [CreateAssetMenu(fileName = "BuildingData", menuName = "Buildings/BuildingData", order = 0)]
    public class BuildingData : ScriptableObject
    {
        [EnumToggleButtons]
        public BuildingType type;
        
        public SpriteData spriteData;

        public string buildingName;

        [Serializable]
        public struct SpriteData
        {
            public Sprite level1;
            public Sprite level2;
            public Sprite level3;
        }
    }
}