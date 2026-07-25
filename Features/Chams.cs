using System.Collections.Generic;
using EFT;
using UnityEngine;

namespace SosunSolution.Features
{
    internal class Chams
    {
        private static Shader _coloredShader;
        private static Shader ColoredShader
        {
            get
            {
                if (_coloredShader == null)
                    _coloredShader = Shader.Find("Hidden/Internal-Colored");
                return _coloredShader;
            }
        }

        private readonly Dictionary<Player, Material[]> _originalMaterials = new Dictionary<Player, Material[]>();
        private readonly Dictionary<Player, Renderer[]> _baseRenderers = new Dictionary<Player, Renderer[]>();
        private readonly Dictionary<Player, GameObject[]> _hiddenClones = new Dictionary<Player, GameObject[]>();

        public void Apply(Player player, Color visibleColor, Color hiddenColor)
        {
            if (player == null || _baseRenderers.ContainsKey(player))
                return;

            Renderer[] renderers = player.PlayerBody.GetComponentsInChildren<Renderer>(true);
            if (renderers.Length == 0)
                return;

            Material[] originals = new Material[renderers.Length];
            GameObject[] clones = new GameObject[renderers.Length];

            for (int i = 0; i < renderers.Length; i++)
            {
                Renderer r = renderers[i];
                originals[i] = r.material;

                Material visibleMat = new Material(ColoredShader);
                visibleMat.SetColor("_Color", visibleColor);
                visibleMat.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.LessEqual);
                visibleMat.SetInt("_ZWrite", 1);
                visibleMat.renderQueue = 3000;
                r.material = visibleMat;

                GameObject clone = new GameObject(r.gameObject.name + "_ChamsHidden");
                clone.transform.SetParent(r.transform, false);
                clone.transform.localPosition = Vector3.zero;
                clone.transform.localRotation = Quaternion.identity;
                clone.transform.localScale = Vector3.one;

                if (r is SkinnedMeshRenderer smr)
                {
                    SkinnedMeshRenderer clonedSmr = clone.AddComponent<SkinnedMeshRenderer>();
                    clonedSmr.bones = smr.bones;
                    clonedSmr.rootBone = smr.rootBone;
                    clonedSmr.sharedMesh = smr.sharedMesh;

                    Material hiddenMat = new Material(ColoredShader);
                    hiddenMat.SetColor("_Color", hiddenColor);
                    hiddenMat.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Greater);
                    hiddenMat.SetInt("_ZWrite", 0);
                    hiddenMat.renderQueue = 4000;
                    clonedSmr.material = hiddenMat;
                }
                else if (r is MeshRenderer mr)
                {
                    MeshFilter originalFilter = r.GetComponent<MeshFilter>();
                    if (originalFilter != null)
                    {
                        MeshFilter clonedFilter = clone.AddComponent<MeshFilter>();
                        clonedFilter.sharedMesh = originalFilter.sharedMesh;

                        MeshRenderer clonedMr = clone.AddComponent<MeshRenderer>();
                        Material hiddenMat = new Material(ColoredShader);
                        hiddenMat.SetColor("_Color", hiddenColor);
                        hiddenMat.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Greater);
                        hiddenMat.SetInt("_ZWrite", 0);
                        hiddenMat.renderQueue = 4000;
                        clonedMr.material = hiddenMat;
                    }
                }

                clones[i] = clone;
            }

            _originalMaterials[player] = originals;
            _baseRenderers[player] = renderers;
            _hiddenClones[player] = clones;
        }

        public void Remove(Player player)
        {
            if (player == null || !_baseRenderers.ContainsKey(player))
                return;

            Renderer[] renderers = _baseRenderers[player];
            Material[] originals = _originalMaterials[player];
            GameObject[] clones = _hiddenClones[player];

            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i] != null)
                    renderers[i].material = originals[i];

                if (clones[i] != null)
                    UnityEngine.Object.Destroy(clones[i]);
            }

            _baseRenderers.Remove(player);
            _originalMaterials.Remove(player);
            _hiddenClones.Remove(player);
        }

        public void RemoveAll()
        {
            foreach (var player in new List<Player>(_baseRenderers.Keys))
                Remove(player);
        }
    }
}