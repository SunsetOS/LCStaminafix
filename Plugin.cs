using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using LCStaminaFix.Patches;

namespace LCStaminaFix
{

    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class LCStaminaFix : BaseUnityPlugin
    {

        public const string PLUGIN_GUID = "sunsetos.lcstaminafix";
        public const string PLUGIN_NAME = "lcstaminafix";
        public const string PLUGIN_VERSION = "1.0.1";


        // private static ConfigFile configFile;
        private readonly Harmony harmony = new Harmony(PLUGIN_GUID);

        private static LCStaminaFix Instance;

        internal ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(PLUGIN_GUID);
            mls.LogInfo(PLUGIN_GUID + " is here");

            harmony.PatchAll(typeof(StaminaFix));
            harmony.PatchAll(typeof(LCStaminaFix));
        }

        
    }
    

    
}
