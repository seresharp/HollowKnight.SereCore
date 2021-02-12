using System;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using static SereCore.LogHelper;
using Object = UnityEngine.Object;

namespace SereCore
{
    [PublicAPI]
    public static class TextureExtensions
    {
        public static void SaveToFile(this Texture2D self, string path)
        {
            if (self == null || path == null)
            {
                return;
            }

            try
            {
                RenderTexture tmp = RenderTexture.GetTemporary(self.width, self.height, 0, RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);
                Graphics.Blit(self, tmp);
                RenderTexture prev = RenderTexture.active;
                RenderTexture.active = tmp;
                Texture2D newTex = new Texture2D(self.width, self.height);
                newTex.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
                newTex.Apply();
                RenderTexture.active = prev;
                RenderTexture.ReleaseTemporary(tmp);

                File.WriteAllBytes(path, newTex.EncodeToPNG());

                Object.Destroy(newTex);
            }
            catch (Exception e)
            {
                LogError(e);
            }
        }
    }
}