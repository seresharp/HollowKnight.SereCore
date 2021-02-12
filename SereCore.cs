using System;
using System.IO;
using Modding;

namespace SereCore
{
    public class SereCore : Mod
    {
        static SereCore()
        {
            string oldDll = Path.Combine(
                Path.GetDirectoryName(typeof(SereCore).Assembly.Location),
                "SeanprCore.dll"
            );

            if (File.Exists(oldDll))
            {
                try
                {
                    File.Delete(oldDll);
                }
                catch (UnauthorizedAccessException) { }
            }

            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                if (!args.Name.StartsWith("SeanprCore"))
                {
                    return null;
                }

                LogHelper.LogWarn("Caught assembly resolve failure for SeanprCore, redirecting to SereCore.");
                LogHelper.LogWarn("Please update your mod to reference SereCore instead.");

                return typeof(SereCore).Assembly;
            };
        }

        public override string GetVersion()
            => GetType().Assembly.GetName().Version.ToString();
    }
}
