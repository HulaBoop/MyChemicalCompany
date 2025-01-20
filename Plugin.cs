using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using MyChemicalCompany.Patches;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyChemicalCompany
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class MyChemicalCompanyBase : BaseUnityPlugin
    {
        private const string modGUID = "HulaBoop.MyChemicalCompany";
        private const string modName = "My Chemical Company";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static MyChemicalCompanyBase Instance;

        internal ManualLogSource manualLogSource;

        internal static List<AudioClip> audioClipList;

        internal static AssetBundle Bundle;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            manualLogSource = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            manualLogSource.LogInfo("It's not a phase, mom.");

            harmony.PatchAll(typeof(StartOfRoundPatch));

            manualLogSource = Logger;

            audioClipList = new List<AudioClip>();

            string FolderLocation = Instance.Info.Location;
            FolderLocation = FolderLocation.TrimEnd("MyChemicalCompany.dll".ToCharArray());
            Bundle = AssetBundle.LoadFromFile(FolderLocation + "mychemicalromance");

            if (Bundle != null)
            {
                manualLogSource.LogInfo("Successfully loaded asset bundle");
                audioClipList = Bundle.LoadAllAssets<AudioClip>().ToList();
            }
            else
            {
                manualLogSource.LogError("Failed to load asset bundle");
            }
        }
    }
}