using System;
using System.IO;
using System.Threading.Tasks;
using FrooxEngine;
using FrooxEngine.Store;
using HarmonyLib;
using ResoniteModLoader;
using SkyFrost.Base;

namespace BlockX
{
    public class Blocker : ResoniteMod
    {
        public override string Name => "BlockX";
        public override string Author => "Jae \"J4\" Lo Presti";
        public override string Version => "0.1.0";

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<string> blockListUrl = new ModConfigurationKey<string>("blockListUrl", "The default URL to fetch a blocklist from.", () => "https://raw.githubusercontent.com/jae1911/BlockX-Lists/refs/heads/beep/lists/default.txt");
        
        private static ModConfiguration Config;

        private static readonly ListUtil LUtil = new ListUtil();
        
        public override void OnEngineInit()
        {
            if (ModLoader.IsHeadless)
            {
                Warn("Here be dragons, proceed with caution, using this mod on headlesses might lead to more issues.");
            }
            
            Config = GetConfiguration();

            Harmony harmony = new Harmony("lc.j4.blockx");
            harmony.PatchAll();
            
            Msg("Insert buckazoid.");

            LUtil.RefreshList(Config?.GetValue(blockListUrl));
        }

        [HarmonyPatch(typeof(EngineAssetGatherer), "Gather")]
        class EngineAssetGatherer_Gather_Patch
        {
            public static bool Prefix(Uri url, float priority, DB_Endpoint? overrideEndpoint, ref ValueTask<GatherResult> __result)
            {
                if (LUtil.CheckIfBlocked(url.AbsoluteUri))
                {
                    string tempFile = Path.GetTempFileName();
                    __result = new ValueTask<GatherResult>(new GatherResult(tempFile));
                    
                    Msg($"Prevented {url} from loading");
                    return false;
                }

                return true;
            }
        }

        [HarmonyPatch(typeof(LocalDB), "TryFetchAssetRecordAsync")]
        class LocalDB_TryFetchAssetRecordAsync_Patch
        {
            public static bool Prefix(Uri assetURL, ref Task<AssetRecord> __result)
            {
                if (LUtil.CheckIfBlocked(assetURL.AbsoluteUri))
                {
                    __result = Task.FromResult(new AssetRecord());
                    
                    Msg($"Prevented {assetURL} from loading");
                    return false;
                }
                
                return true;
            }
        }
    }
}