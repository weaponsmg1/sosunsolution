using EFT;
using EFT.Animations;
using EFT.InventoryLogic;
using HarmonyLib;
using SosunSolution.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SosunSolution.Features
{
    public class Accuracy
    {
        public void Hook()
        {
            if (!SDK.RaidStarted)
                return;

            if (SDK.LocalPlayer == null)
                return;

            if(Vars.sn_instant_ads)
                InstantADS(SDK.LocalPlayer);
        }

        private void InstantADS(Player player)
        {
            ProceduralWeaponAnimation proceduralWeaponAnimation = player.ProceduralWeaponAnimation;
            if (proceduralWeaponAnimation == null || SDK.PwaAimingSpeedField == null)
            {
                return;
            }
            try
            {
                SDK.PwaAimingSpeedField.SetValue(proceduralWeaponAnimation, 20f);
            }
            catch { }
 
        }

    }
}
