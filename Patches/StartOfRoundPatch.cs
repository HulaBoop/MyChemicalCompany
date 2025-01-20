using HarmonyLib;

namespace MyChemicalCompany.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        static void OverrideAudio(StartOfRound __instance)
        {
            System.Random random = new System.Random();
            int audioClipNumber = random.Next(0, 7);

            __instance.shipIntroSpeechSFX = MyChemicalCompanyBase.audioClipList[audioClipNumber];
        }
    }
}