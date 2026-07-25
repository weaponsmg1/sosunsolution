using Comfort.Common;
using EFT;
using EFT.Animations;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace SosunSolution.Utils
{
    internal class SDK
    {
        public static GameWorld World
        {
            get
            {
                if (Singleton<GameWorld>.Instantiated)
                {
                    return Singleton<GameWorld>.Instance;
                }
                return null;
            }
        }

        public static Player LocalPlayer
        {
            get
            {
                GameWorld world = SDK.World;
                if (!(world != null))
                {
                    return null;
                }
                return world.MainPlayer;
            }
        }

        public static bool InRaid
        {
            get
            {
                GameWorld world = SDK.World;
                return world != null && world.MainPlayer != null;
            }
        }

        public static bool RaidStarted
        {
            get
            {
                if (!Singleton<AbstractGame>.Instantiated)
                {
                    return false;
                }
                AbstractGame instance = Singleton<AbstractGame>.Instance;
                if (instance == null || instance.Status != GameStatus.Started)
                {
                    return false;
                }
                GameWorld world = SDK.World;
                return world != null && world.MainPlayer != null;
            }
        }

        public static IEnumerable<Player> AllAlivePlayers
        {
            get
            {
                GameWorld gameWorld = Singleton<GameWorld>.Instance;
                if (gameWorld == null)
                    yield break;

                foreach (Player player in gameWorld.RegisteredPlayers)
                {
                    if (player != null &&
                        player.HealthController != null &&
                        player.HealthController.IsAlive)
                    {
                        yield return player;
                    }
                }
            }
        }

        public static Camera FpsCamera
        {
            get
            {
                if (CameraClass.Exist && CameraClass.Instance.Camera != null)
                {
                    return CameraClass.Instance.Camera;
                }
                return Camera.main;
            }
        }

        public static bool WorldToScreen(Camera camera, Vector3 world, out Vector2 screen)
        {
            if (camera == null)
            {
                screen = default(Vector2);
                return false;
            }
            Vector3 vector = camera.WorldToScreenPoint(world);
            if (vector.z <= 0f)
            {
                screen = default(Vector2);
                return false;
            }
            screen = new Vector2(vector.x, (float)Screen.height - vector.y);
            return true;
        }
        public static IEnumerable<Vector3> BoxCorners(Player p)
        {
            Vector3 pos = p.Position;

            float height = 1.8f;
            float width = 0.6f;
            float depth = 0.6f;

            Vector3 center = pos + Vector3.up * (height / 2f);
            Vector3 half = new Vector3(width / 2f, height / 2f, depth / 2f);

            yield return center + new Vector3(-half.x, -half.y, -half.z);
            yield return center + new Vector3(half.x, -half.y, -half.z);
            yield return center + new Vector3(-half.x, half.y, -half.z);
            yield return center + new Vector3(half.x, half.y, -half.z);
            yield return center + new Vector3(-half.x, -half.y, half.z);
            yield return center + new Vector3(half.x, -half.y, half.z);
            yield return center + new Vector3(-half.x, half.y, half.z);
            yield return center + new Vector3(half.x, half.y, half.z);
        }

        public static readonly FieldInfo PwaAimingSpeedField = typeof(ProceduralWeaponAnimation).GetField("_aimingSpeed", BindingFlags.NonPublic | BindingFlags.Instance);

    }
}
