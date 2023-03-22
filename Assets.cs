using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UnstableCards
{
    internal class Assets
    {
        private static readonly AssetBundle Bundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("assets", typeof(UnstableCards).Assembly);

        // Card Art
        public static GameObject FirefliesArt = Bundle.LoadAsset<GameObject>("C_Fireflies");
        public static GameObject BoomstickArt = Bundle.LoadAsset<GameObject>("C_Boomstick");
        public static GameObject GoldenAppleArt = Bundle.LoadAsset<GameObject>("C_GoldenApple");
        public static GameObject EnchantedGoldenAppleArt = Bundle.LoadAsset<GameObject>("C_EnchantedGoldenApple");
        public static GameObject HungerArt = Bundle.LoadAsset<GameObject>("C_Hunger");
        public static GameObject NerforNothinArt = Bundle.LoadAsset<GameObject>("C_NerforNothin");
        public static GameObject DrumMagArt = Bundle.LoadAsset<GameObject>("C_DrumMag");
        public static GameObject AmmoContainerArt = Bundle.LoadAsset<GameObject>("C_AmmoContainer");
        public static GameObject DetonatorArt = Bundle.LoadAsset<GameObject>("C_Detonator");
        public static GameObject WeakBonesArt = Bundle.LoadAsset<GameObject>("C_WeakBones");
        public static GameObject WeakheartArt = Bundle.LoadAsset<GameObject>("C_Weakheart");
        public static GameObject WallStreetCrashArt = Bundle.LoadAsset<GameObject>("C_WallStreetCrash");
        public static GameObject StoneStatueArt = Bundle.LoadAsset<GameObject>("C_StoneStatue");
        public static GameObject UraniumPayloadArt = Bundle.LoadAsset<GameObject>("C_UraniumPayload");
        public static GameObject PointClickAdventureGameArt = Bundle.LoadAsset<GameObject>("C_PointClickAdventureGame");
        public static GameObject WeaponServicingArt = Bundle.LoadAsset<GameObject>("C_WeaponServicing");
        public static GameObject DollarStoreFirearmArt = Bundle.LoadAsset<GameObject>("C_DollarStoreFirearm");
        public static GameObject RunItBackArt = Bundle.LoadAsset<GameObject>("C_RunItBack");
        public static GameObject DenseBulletsArt = Bundle.LoadAsset<GameObject>("C_DenseBullets");
        public static GameObject OneInTheChamberArt = Bundle.LoadAsset<GameObject>("C_OneInTheChamber");
        public static GameObject TwoInTheChamberArt = Bundle.LoadAsset<GameObject>("C_TwoInTheChamber");
        public static GameObject ShieldOfTheDamnedArt = Bundle.LoadAsset<GameObject>("C_ShieldOfTheDamned");
        public static GameObject HeartOfTheDamnedArt = Bundle.LoadAsset<GameObject>("C_HeartOfTheDamned");
        public static GameObject SkullOfTheDamnedArt = Bundle.LoadAsset<GameObject>("C_SkullOfTheDamned");
        public static GameObject SoulOfTheDamnedArt = Bundle.LoadAsset<GameObject>("C_SoulOfTheDamned");
        public static GameObject TheCatArt = Bundle.LoadAsset<GameObject>("C_TheCat");
        public static GameObject AngelWeaverArt = Bundle.LoadAsset<GameObject>("C_AngelWeaver");
        public static GameObject AmateurArt = Bundle.LoadAsset<GameObject>("C_Amateur");
        public static GameObject ClumsyArt = Bundle.LoadAsset<GameObject>("C_Clumsy");
        public static GameObject IllegalGunPartsArt = Bundle.LoadAsset<GameObject>("C_IllegalGunParts");
        public static GameObject RustBucketArt = Bundle.LoadAsset<GameObject>("C_RustBucket");
        public static GameObject NinjitsuMasterArt = Bundle.LoadAsset<GameObject>("C_NinjitsuMaster");
        public static GameObject ProfessionalArt = Bundle.LoadAsset<GameObject>("C_Professional");
        public static GameObject BostonBoyArt = Bundle.LoadAsset<GameObject>("C_BostonBoy");
        public static GameObject RocketJumperArt = Bundle.LoadAsset<GameObject>("C_RocketJumper");
        public static GameObject WhiskeyBottleArt = Bundle.LoadAsset<GameObject>("C_WhiskeyBottle");
        public static GameObject TotemOfTheDamnedArt = Bundle.LoadAsset<GameObject>("C_TotemOfTheDamned");
        public static GameObject TotemOfTheBalancedArt = Bundle.LoadAsset<GameObject>("C_TotemOfTheBalanced");
        public static GameObject TotemOfTheForgottenArt = Bundle.LoadAsset<GameObject>("C_TotemOfTheForgotten");
        public static GameObject HeavyWeaponsGuyArt = Bundle.LoadAsset<GameObject>("C_HeavyWeaponsGuy");
        public static GameObject StabbinLicenseArt = Bundle.LoadAsset<GameObject>("C_StabbinLicense");
        public static GameObject TheSoulConsumerArt = Bundle.LoadAsset<GameObject>("C_TheSoulConsumer");
        public static GameObject TurtleArt = Bundle.LoadAsset<GameObject>("C_Turtle");
        public static GameObject BulletConjurerArt = Bundle.LoadAsset<GameObject>("C_BulletConjurer");
        public static GameObject BulletsOfTheDamnedArt = Bundle.LoadAsset<GameObject>("C_BulletsOfTheDamned");
        public static GameObject WavyBulletsArt = Bundle.LoadAsset<GameObject>("C_WavyBullets");
        public static GameObject BubbleArt = Bundle.LoadAsset<GameObject>("C_Bubble");
        public static GameObject OilFilter = Bundle.LoadAsset<GameObject>("C_OilFilter");
        public static GameObject TrainBullets = Bundle.LoadAsset<GameObject>("C_TrainBullets");

        // Audio
        public static AudioClip totemOfTheDamnedAudio = Bundle.LoadAsset<AudioClip>("A_TotemOfTheDamned");
        public static AudioClip totemOfTheBalancedAudio = Bundle.LoadAsset<AudioClip>("A_TotemOfTheBalanced");
        public static AudioClip totemOfTheForgottenAudio = Bundle.LoadAsset<AudioClip>("A_TotemOfTheForgotten");
        public static AudioClip BostonBoyAudio = Bundle.LoadAsset<AudioClip>("A_BostonBoy");
        public static AudioClip BoomstickAudio = Bundle.LoadAsset<AudioClip>("A_Boomstick");
        public static AudioClip GoldenAppleAudio = Bundle.LoadAsset<AudioClip>("A_GoldenApple");
        public static AudioClip RustBucketAudio = Bundle.LoadAsset<AudioClip>("A_RustBucket");
    }
}
