using BepInEx;
using BepInEx.Logging;
using SosunSolution.Features;
using UnityEngine;

namespace SosunSolution
{
    [BepInPlugin("SosunSolution.ssn", "SosynSolution", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private Movement g_Movement = new Movement();
        private Visuals g_Visuals = new Visuals();
        private Accuracy g_Accuracy = new Accuracy();
        private Chams g_Chams = new Chams();
        private Menu g_Menu = new Menu();
        public static ManualLogSource LogSource;

        private void Awake()
        {
            LogSource = Logger;
            LogSource.LogInfo("sosunsolution loaded!");
        }

        private void Update()
        {
            g_Movement.Hook();
            g_Accuracy.Hook();

            g_Visuals.UpdateChams(g_Chams);

            if (Input.GetKeyDown(KeyCode.Insert))
            {
                g_Menu.showMenu = !g_Menu.showMenu;
            }
        }

        private void OnGUI()
        {
            if (Event.current.type == EventType.Repaint)
            {
                g_Visuals.Draw(); 
            }

            if (g_Menu.showMenu)
            {
                g_Menu.windowRect = GUILayout.Window(1337, g_Menu.windowRect, g_Menu.DrawMenu, "SosunSolution");
            }
        }
    }
}