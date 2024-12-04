using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using GameNetcodeStuff;
using UnityEngine;

namespace LCStaminaFix.Patches
{
    [HarmonyPatch(typeof(GameNetcodeStuff.PlayerControllerB))]

    internal class StaminaFix
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        // Vanilla SprintTime 11f
        static void StaminaRegan(ref float ___sprintMeter, ref float ___sprintTime, ref bool ___isSprinting, ref bool ___isWalking, ref float ___drunkness)
        {
            float drunkMeter = Mathf.Abs(StartOfRound.Instance.drunknessSpeedEffect.Evaluate(___drunkness) - 1.25f);

            if (!___isSprinting && !___isWalking) // Standing 12
            {
                ___sprintMeter = Mathf.Clamp(___sprintMeter + 1 / LCStaminaFix.StandConfig.Value * Time.deltaTime * drunkMeter - Time.deltaTime * drunkMeter / (___sprintTime + 4f), 0f, 1f);
            }
            else if (!___isSprinting && ___isWalking) // Walking 16
            {
                ___sprintMeter = Mathf.Clamp(___sprintMeter + 1 / LCStaminaFix.WalkingConfig.Value * Time.deltaTime * drunkMeter - Time.deltaTime * drunkMeter / (___sprintTime + 9f), 0f, 1f);
            }
        }
    }
}
