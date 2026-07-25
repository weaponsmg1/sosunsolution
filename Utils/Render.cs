using EFT;
using EFT.HealthSystem;
using SosunSolution.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static SosunSolution.Vars;


namespace SosunSolution.Utils
{
    internal class Render
    {

        public static void DrawHollowRect(Rect rect, Color color, float thickness)
        {
            Color oldColor = GUI.color;
            GUI.color = color;

            GUI.DrawTexture(new Rect(rect.x, rect.y, rect.width, thickness), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(rect.x, rect.y + rect.height - thickness, rect.width, thickness), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(rect.x, rect.y, thickness, rect.height), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(rect.x + rect.width - thickness, rect.y, thickness, rect.height), Texture2D.whiteTexture);

            GUI.color = oldColor;
        }

        private static void DrawLineGL(Vector2 start, Vector2 end, Color color, float thickness)
        {
            if (Event.current.type == EventType.Repaint)
            {
                GL.PushMatrix();
                GL.LoadPixelMatrix();
                GL.Begin(GL.LINES);
                GL.Color(color);

                GL.Vertex3(start.x, start.y, 0);
                GL.Vertex3(end.x, end.y, 0);

                GL.End();
                GL.PopMatrix();
            }
        }

        public static Rect? DrawBox2D(Camera cam, Player player, Color color)
        {
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;
            bool anyVisible = false;

            foreach (Vector3 worldPos in SDK.BoxCorners(player))
            {
                Vector2 screenPos;
                if (SDK.WorldToScreen(cam, worldPos, out screenPos))
                {
                    minX = Mathf.Min(minX, screenPos.x);
                    minY = Mathf.Min(minY, screenPos.y);
                    maxX = Mathf.Max(maxX, screenPos.x);
                    maxY = Mathf.Max(maxY, screenPos.y);
                    anyVisible = true;
                }
            }

            if (!anyVisible)
                return null;

            float padding = 4f;
            Rect rect = new Rect(
                minX - padding,
                minY - padding,
                maxX - minX + padding * 2f,
                maxY - minY + padding * 2f
            );

            DrawHollowRect(rect, color, 1f);

            return rect;
        }
    }
}
