using System;
using System.Collections.Generic;
using System.Text;
using EFT;
using EFT.HealthSystem;
using SosunSolution.Utils;

namespace SosunSolution.Features
{
    internal class Movement
    {
        public void Hook()
        {
            if (!SDK.RaidStarted)
                return;

            if (SDK.LocalPlayer == null)
                return;

            if(Vars.sn_infinity_stamina)
                InfinityStamina(SDK.LocalPlayer);
        }


        public void InfinityStamina(Player player)
        {
            BasePhysicalClass physical = player.Physical;
            if (physical == null)
            {
                return;
            }

            if (physical.Stamina != null)
            {
                physical.Stamina.Current = physical.StaminaCapacity;
            }

            if (physical.HandsStamina != null)
            {
                physical.HandsStamina.Current = physical.HandsCapacity;
            }

            if (physical.Oxygen != null)
            {
                physical.Oxygen.Current = physical.OxygenCapacity;
            }
        }

    }
}
