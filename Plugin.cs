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
        public const string PLUGIN_VERSION = "1.1.0";

        public static ConfigEntry<float> StandConfig;
        public static ConfigEntry<float> WalkingConfig;

        // private static ConfigFile configFile;
        private readonly Harmony harmony = new Harmony(PLUGIN_GUID);

        public static LCStaminaFix Instance;

        internal ManualLogSource log;

        void Awake()
        {

            if (Instance == null)
            {
                Instance = this;    
            }

            log = BepInEx.Logging.Logger.CreateLogSource(PLUGIN_GUID);
            log = Logger;
            log.LogInfo(PLUGIN_GUID + " has been loaded");

            StandConfig = Config.Bind("General",
                                      "StandingRegenTime",
                                      12f,
                                      "How long does it take for stamina to fully regen when you're standing.");

            WalkingConfig = Config.Bind("General",
                                        "WalkingRegenTime",
                                        16f,
                                        "How long does it take for stamina to fully regen when you're walking.");

            log.LogInfo($"Walking Regen Time set to {WalkingConfig.Value}");
            log.LogInfo($"Standing Regen Time set to {StandConfig.Value}");

            harmony.PatchAll(typeof(StaminaFix));
            harmony.PatchAll(typeof(LCStaminaFix));
        }

        
    }
    

    
}
