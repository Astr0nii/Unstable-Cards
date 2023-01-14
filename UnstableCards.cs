using BepInEx;
using UnityEngine;
using UnboundLib.Cards;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using RarityLib.Utils;
using System.Collections.Generic;
using UnstableCards.Cards.Wacky;
using UnstableCards.Cards.Debuffs;
using UnstableCards.Cards.God;
using UnstableCards.Cards.Buff;
using UnstableCards.Cards.Shrine;
using UnstableCards.Cards.Damned;
using UnboundLib;

namespace UnstableCards
{
    // Mods required to make this work.

    [BepInDependency("root.rarity.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("Root.Gun.bodyRecoil.Patch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.CrazyCoders.Rounds.RarityBundle", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.managers", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.classes.manager.reborn", BepInDependency.DependencyFlags.HardDependency)]

    // Declaring that our mod exists to BepIn
    [BepInPlugin(ModId, ModName, Version)]

    //The game our mod is associated with
    [BepInProcess("Rounds.exe")]

    public class UnstableCards : BaseUnityPlugin
    {
        public CardCategory damnedCategory { get; private set; } = CustomCardCategories.instance.CardCategory("Damned");
        private const string ModId = "com.Astr0ni.Rounds.UnstableCards";
        private const string ModName = "Unstable Cards";
        private const string Version = "1.8.0"; // Mod version (major.minor.patch)

        public const string ModInitials = "UC";

        public static Dictionary<string, GameObject> CardArt = new Dictionary<string, GameObject>();

        void Awake()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            RarityUtils.AddRarity("Damned", 0.000000000000000000001f, new Color32(15, 214, 81, 255), new Color32(0, 95, 60, 255));
        }

        void Start()
        {
            AudioSource audioSource = gameObject.GetOrAddComponent<AudioSource>();
            audioSource.PlayOneShot(Assets.startupSound, 0.75f);
            // Wacky Cards
            CustomCard.BuildCard<Boomstick>();
            CustomCard.BuildCard<PointClickAdventureGame>();
            CustomCard.BuildCard<UraniumPayload>();
            CustomCard.BuildCard<TackShooter>();
            CustomCard.BuildCard<StabbinLicense>();
            CustomCard.BuildCard<Detonator>();
            CustomCard.BuildCard<StoneStatue>();
            CustomCard.BuildCard<RocketJumper>();
            CustomCard.BuildCard<HeavyWeaponsGuy>();

            // DeBuffs
            CustomCard.BuildCard<WhiskeyBottle>();
            CustomCard.BuildCard<NerfOrNothin>();
            CustomCard.BuildCard<WeakBones>();
            CustomCard.BuildCard<Hunger>();
            CustomCard.BuildCard<Weakheart>();
            CustomCard.BuildCard<WallStreetCrash>();
            CustomCard.BuildCard<Amatuer>();
            CustomCard.BuildCard<DollarStoreFirearm>();
            CustomCard.BuildCard<Clumsy>();

            // Normal Cards
            CustomCard.BuildCard<ShieldOfTheDamned>();
            CustomCard.BuildCard<NinjitsuMaster>();
            CustomCard.BuildCard<BostonBoy>();
            CustomCard.BuildCard<GoldenApple>();
            CustomCard.BuildCard<EnchantedGoldenApple>();
            CustomCard.BuildCard<OneInTheChamber>();
            CustomCard.BuildCard<DrumMag>();
            CustomCard.BuildCard<AmmoContainer>();
            CustomCard.BuildCard<IllegalGunParts>();
            CustomCard.BuildCard<TwoInTheChamber>();
            CustomCard.BuildCard<RunItBack>();

            //God Cards
            CustomCard.BuildCard<TheCat>();
            CustomCard.BuildCard<TheSoulConsumer>();
            CustomCard.BuildCard<AngelWeaver>();

            //Shrine Cards
            CustomCard.BuildCard<ShrineOfTheForgotten>();
            CustomCard.BuildCard<ShrineOfTheBalanced>();
            CustomCard.BuildCard<ShrineOfTheDamned>();

            //Damned Cards
            CustomCard.BuildCard<ShieldOfTheDamned>();

            instance = this;
        }
        public static UnstableCards instance { get; private set; }

        public static (GameObject AddToProjectile, GameObject effect, Explosion explosion) LoadExplosion(string name, Gun? gun = null)
        {
            // load explosion effect from Explosive Bullet card
            GameObject? explosiveBullet = (GameObject)Resources.Load("0 cards/Explosive bullet");

            Gun explosiveGun = explosiveBullet.GetComponent<Gun>();

            if (gun != null)
            {
                // change the gun sounds
                gun.soundGun.AddSoundImpactModifier(explosiveGun.soundImpactModifier);
            }

            // load assets
            GameObject A_ExplosionSpark = explosiveGun.objectsToSpawn[0].AddToProjectile;
            GameObject explosionCustom = Instantiate(explosiveGun.objectsToSpawn[0].effect);
            explosionCustom.transform.position = new Vector3(1000, 0, 0);
            explosionCustom.hideFlags = HideFlags.HideAndDontSave;
            explosionCustom.name = name;
            DestroyImmediate(explosionCustom.GetComponent<RemoveAfterSeconds>());
            Explosion explosion = explosionCustom.GetComponent<Explosion>();

            return (A_ExplosionSpark, explosionCustom, explosion);
        }
    }
}
