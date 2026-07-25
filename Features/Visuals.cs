using EFT;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SosunSolution.Utils;

namespace SosunSolution.Features
{
    internal class Visuals
    {
        public void Draw()
        {
            if (!SDK.RaidStarted)
                return;

            if (SDK.FpsCamera == null)
                return;

            if (SDK.LocalPlayer == null)
                return;

            float maxDistance = Vars.sn_visuals_distance;
            Vector3 localPos = SDK.LocalPlayer.Position;

            foreach (Player player in SDK.AllAlivePlayers)
            {
                if (player == null || player == SDK.LocalPlayer)
                    continue;

                if (player.HealthController == null || !player.HealthController.IsAlive)
                    continue;

                if (Vector3.Distance(localPos, player.Position) > maxDistance)
                    continue;

                if (Vars.sn_visuals_player_box)
                    Render.DrawBox2D(SDK.FpsCamera, player, Color.red);
            }
        }

        public void UpdateChams(Chams chams)
        {
            if (!SDK.RaidStarted)
            {
                chams.RemoveAll();
                return;
            }

            if (SDK.LocalPlayer == null)
                return;

            foreach (Player player in SDK.AllAlivePlayers)
            {
                if (player == null || player == SDK.LocalPlayer)
                    continue;

                if (player.HealthController == null || !player.HealthController.IsAlive)
                    continue;

                if (Vars.sn_visuals_player_chams)
                    chams.Apply(player, Vars.sn_clr_player, Vars.sn_clr_player);
                else
                    chams.Remove(player);
            }
        }
    }
}