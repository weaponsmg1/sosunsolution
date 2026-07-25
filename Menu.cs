using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SosunSolution
{
    internal class Menu
    {
        public bool showMenu = false;

        public Rect windowRect = new Rect(20, 20, 300, 350);
        public Vector2 scrollPosition = Vector2.zero;

        public void DrawMenu(int windowID)
        {
            GUILayout.BeginVertical();

            GUILayout.Label("SosunSolution", new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            });

            GUILayout.Space(10);

            Vars.sn_infinity_stamina = GUILayout.Toggle(Vars.sn_infinity_stamina, "Infinite Stamina");

            GUILayout.Space(10);

            Vars.sn_instant_ads = GUILayout.Toggle(Vars.sn_instant_ads, "InstantADS");

            GUILayout.Space(10);

            Vars.sn_visuals_player_chams = GUILayout.Toggle(Vars.sn_visuals_player_chams, "Chams");

            GUILayout.Space(10);

            Vars.sn_visuals_player_box = GUILayout.Toggle(Vars.sn_visuals_player_box, "ESP Box");

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();

            GUILayout.Label("Distance:", GUILayout.Width(60));
            Vars.sn_visuals_distance = GUILayout.HorizontalSlider(Vars.sn_visuals_distance, 0, 500, GUILayout.Width(150));
            GUILayout.Label($"{Vars.sn_visuals_distance:F0}m", GUILayout.Width(40));

            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.Label("Player Color (RGB)", new GUIStyle(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            });

            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("R:", GUILayout.Width(20));
            Vars.sn_clr_player_r = (int)GUILayout.HorizontalSlider(Vars.sn_clr_player_r, 0, 255, GUILayout.Width(180));
            GUILayout.Label($"{Vars.sn_clr_player_r}", GUILayout.Width(30));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("G:", GUILayout.Width(20));
            Vars.sn_clr_player_g = (int)GUILayout.HorizontalSlider(Vars.sn_clr_player_g, 0, 255, GUILayout.Width(180));
            GUILayout.Label($"{Vars.sn_clr_player_g}", GUILayout.Width(30));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("B:", GUILayout.Width(20));
            Vars.sn_clr_player_b = (int)GUILayout.HorizontalSlider(Vars.sn_clr_player_b, 0, 255, GUILayout.Width(180));
            GUILayout.Label($"{Vars.sn_clr_player_b}", GUILayout.Width(30));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Close Menu"))
                showMenu = false;

            GUILayout.EndVertical();

            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }
    }
}