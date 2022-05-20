using BepInEx;
using UnityEngine;

namespace LHA
{
    [BepInPlugin("com.kobrakon.LHA", "LHA", "1.0.0")]
    public class LHA : BaseUnityPlugin
    {
        public static GameObject Hook;
        private void Awake()
        {
            Logger.LogInfo($"LHA: Loading");
            Hook = new GameObject("LHA");
            Hook.AddComponent<LHAController>();
            DontDestroyOnLoad(Hook);
            
            LHAController.LoadAudio("\\BepInEx\\plugins\\LHA\\alert.wav");
        }
    }
}
