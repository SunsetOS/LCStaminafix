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
        static void Postfix(ref float ___sprintMeter, ref bool ___isSprinting, ref bool ___isWalking, ref float ___sprintTime)
        {
            if (!___isSprinting && !___isWalking)
            {
                ___sprintMeter = Mathf.Clamp(___sprintMeter + 0.08f * Time.deltaTime - Time.deltaTime / (___sprintTime + 4f), 0f, 1f);
            }else if (!___isSprinting && ___isWalking)
            {
                ___sprintMeter = Mathf.Clamp(___sprintMeter + 0.07f * Time.deltaTime - Time.deltaTime / (___sprintTime + 9f), 0f, 1f);
            }
        }
    }
}
