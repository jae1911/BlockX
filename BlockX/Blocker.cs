using HarmonyLib;
using ResoniteModLoader;

namespace BlockX
{
    public class Blocker : ResoniteMod
    {
        public override string Name => "BlockX";
        public override string Author => "Jae \"J4\" Lo Presti";
        public override string Version => "0.1.0";

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<string> blockListUrl = new ModConfigurationKey<string>("blockListUrl", "The default URL to fetch a blocklist from.", () => "https://i.j4.lc/resonite/bl.txt");

        private static ModConfiguration Config;
        
        public override void OnEngineInit()
        {
            Config = GetConfiguration();

            Harmony harmony = new Harmony("lc.j4.blockx");
            harmony.PatchAll();
            
            Msg("Insert buckazoid.");
        }
    }
}