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
            new LHAController().Start();
            Logger.LogInfo($"LHA Loading");
        }
    }
}
