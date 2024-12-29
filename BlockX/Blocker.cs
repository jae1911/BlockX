using System;
using System.IO;
using System.Threading.Tasks;
using FrooxEngine;
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
        private static readonly ModConfigurationKey<string> blockListUrl = new ModConfigurationKey<string>("blockListUrl", "The default URL to fetch a blocklist from.", () => "https://i.j4.lc/resonite/bl.txt");

        private const string sigTest = "d8f42c9ee9af31a2671f6f00773d8e2bc7a808596d195725b61e0d8e4b349e48";
        
        private static ModConfiguration Config;

        private ListUtil _lUtil = new ListUtil();
        
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

            _lUtil.RefreshList(Config?.GetValue(blockListUrl));
        }

        [HarmonyPatch(typeof(EngineAssetGatherer), "Gather")]
        class EngineAssetGatherer_Gather_Patch
        {
            public static bool Prefix(Uri url, float priority, DB_Endpoint? overrideEndpoint, ref ValueTask<GatherResult> __result)
            {
                if (url.AbsoluteUri.Contains(sigTest))
                {
                    string tempFile = Path.GetTempFileName();
                    __result = new ValueTask<GatherResult>(new GatherResult(tempFile));
                    
                    Msg($"Prevented {url} from loading");
                    return false;
                }

                return true;
            }
        }
    }
}