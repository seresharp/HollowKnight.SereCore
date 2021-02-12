using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using static SereCore.LogHelper;

namespace SereCore
{
    [PublicAPI]
    public static class ResourceHelper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Dictionary<string, Sprite> GetSprites(string prefix = null)
        {
            Assembly callingAssembly = new StackFrame(1, false).GetMethod()?.DeclaringType?.Assembly;
            if (callingAssembly == typeof(SereCore).Assembly)
            {
                callingAssembly = new StackFrame(2, false).GetMethod()?.DeclaringType?.Assembly;
            }

            Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

            if (callingAssembly == null)
            {
                return sprites;
            }

            foreach (string resource in callingAssembly.GetManifestResourceNames()
                .Where(name => name.ToLower().EndsWith(".png")))
            {
                try
                {
                    using (Stream stream = callingAssembly.GetManifestResourceStream(resource))
                    {
                        if (stream == null)
                        {
                            continue;
                        }

                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);

                        // Create texture from bytes
                        Texture2D tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                        tex.LoadImage(buffer, true);
                        tex.filterMode = FilterMode.Point;

                        string resName = Path.GetFileNameWithoutExtension(resource);
                        if (!string.IsNullOrEmpty(prefix))
                        {
                            resName = resName.Replace(prefix, "");
                        }

                        // Create sprite from texture
                        sprites.Add(resName,
                            Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f)));
                    }
                }
                catch (Exception e)
                {
                    LogError(e);
                }
            }

            return sprites;
        }
    }
}
