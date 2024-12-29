using System;
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
        
        public override void OnEngineInit()
        {
            Config = GetConfiguration();

            Harmony harmony = new Harmony("lc.j4.blockx");
            harmony.PatchAll();
            
            Msg("Insert buckazoid.");
        }

        [HarmonyPatch(typeof(EngineAssetGatherer), "Gather")]
        class EngineAssetGatherer_Gather_Patch
        {
            public static bool Prefix(Uri url, float priority, DB_Endpoint? overrideEndpoint, ref ValueTask<GatherResult> __result)
            {
                if (url.ToString().Contains(sigTest))
                {
                    __result = new ValueTask<GatherResult>(new GatherResult((string)null));
                    
                    Msg($"Prevented {url} from loading");
                    return false;
                }

                return true;
            }
        }
    }
}