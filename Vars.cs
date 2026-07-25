using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SosunSolution
{
    internal class Vars
    {
        // Accuracy
        public static bool sn_instant_ads = true;

        // Movement
        public static bool sn_infinity_stamina = true;

        // Visuals
        public static bool sn_visuals_player_chams = true;
        public static bool sn_visuals_player_box = false;
        public static float sn_visuals_distance = 250.0f;

        // Color
        public static int sn_clr_player_r = 255;
        public static int sn_clr_player_g = 0;
        public static int sn_clr_player_b = 0;

        public static Color sn_clr_player => new Color(Vars.sn_clr_player_r / 255f, Vars.sn_clr_player_g / 255f, Vars.sn_clr_player_b / 255f, 1f);
    }
}
