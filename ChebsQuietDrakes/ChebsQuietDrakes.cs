using BepInEx;
using Jotunn;
using Jotunn.Managers;
using Jotunn.Utils;

namespace ChebsQuietDrakes
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency(Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.NotEnforced, VersionStrictness.None)]
    internal class ChebsQuietDrakes : BaseUnityPlugin
    {
        public const string PluginGuid = "com.chebgonaz.chebsquietdrakes";
        public const string PluginName = "ChebsQuietDrakes";
        public const string PluginVersion = "1.2.0";

        private void Awake()
        {
            PrefabManager.OnVanillaPrefabsAvailable += DoOnVanillaPrefabsAvailable;
        }

        private void DoOnVanillaPrefabsAvailable()
        {
            PrefabManager.OnVanillaPrefabsAvailable -= DoOnVanillaPrefabsAvailable;

            var drakePrefab = PrefabManager.Instance.GetPrefab("Hatchling");
            if (drakePrefab == null)
            {
                Logger.LogError("Failed to get drake prefab");
                return;
            }

            if (!drakePrefab.TryGetComponent(out MonsterAI monsterAI))
            {
                Logger.LogError("Failed to get drake prefab's MonsterAI component");
                return;
            }
            
            foreach (var effect in monsterAI.m_alertedEffects.m_effectPrefabs)
            {
                effect.m_enabled = false;
            }
            
            foreach (var effect in monsterAI.m_idleSound.m_effectPrefabs)
            {
                effect.m_enabled = false;
            }
        }
    }
}