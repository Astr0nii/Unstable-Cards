using BepInEx;
using UnityEngine;
using UnboundLib.Cards;
using HarmonyLib;
using RarityLib.Utils;
using System.Collections.Generic;
using UnstableCards.Cards.Wacky;
using UnstableCards.Cards.Debuffs;
using UnstableCards.Cards.God;
using UnstableCards.Cards.Buff;
using UnstableCards.Cards.Shrine;
using UnstableCards.Cards.Damned;
using UnboundLib;
using ModdingUtils;
using UnboundLib.Utils;
using System.Linq;

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
    [BepInDependency("com.willuwontu.rounds.BlockForcePatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.RespawnPatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.rounds.willuwontu.gunchargepatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willis.rounds.modsplus", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.root.projectile.size.patch", BepInDependency.DependencyFlags.HardDependency)]

    // Declaring that our mod exists to BepIn
    [BepInPlugin(ModId, ModName, Version)]

    //The game our mod is associated with
    [BepInProcess("Rounds.exe")]

    public class UnstableCards : BaseUnityPlugin
    {
        private const string ModId = "com.Astr0ni.Rounds.UnstableCards";
        private const string ModName = "Unstable Cards";
        private const string Version = "2.4.0"; // Mod version (major.minor.patch)

        public const string ModInitials = "UC";


        internal static List<CardInfo> damnedCards = new List<CardInfo>();

        void Awake()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        void Start()
        {
            // Wacky Cards
            CustomCard.BuildCard<Boomstick>();
            CustomCard.BuildCard<PointClickAdventureGame>();
            CustomCard.BuildCard<UraniumPayload>();
            CustomCard.BuildCard<StabbinLicense>();
            CustomCard.BuildCard<Detonator>();
            CustomCard.BuildCard<StoneStatue>();
            CustomCard.BuildCard<RocketJumper>();
            CustomCard.BuildCard<HeavyWeaponsGuy>();
            CustomCard.BuildCard<Fireflies>();
            CustomCard.BuildCard<WavyBullets>();
            CustomCard.BuildCard<Bubble>();

            // Normal Cards
            CustomCard.BuildCard<RustBucket>();
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
            CustomCard.BuildCard<WeaponServicing>();
            CustomCard.BuildCard<DenseBullets>();
            CustomCard.BuildCard<Professional>();
            CustomCard.BuildCard<BulletConjurer>();
            CustomCard.BuildCard<Machina>();

            //God Cards
            CustomCard.BuildCard<TheCat>();
            CustomCard.BuildCard<TheSoulConsumer>();
            CustomCard.BuildCard<AngelWeaver>();

            //Totem Cards
            CustomCard.BuildCard<TotemOfTheForgotten>();
            CustomCard.BuildCard<TotemOfTheBalanced>();
            CustomCard.BuildCard<TotemOfTheDamned>();

            //Damned Totem Cards
            CustomCard.BuildCard<HeartOfTheDamned>(c => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(c); damnedCards.Add(c); });
            CustomCard.BuildCard<SkullOfTheDamned>(c => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(c); damnedCards.Add(c); });
            CustomCard.BuildCard<ShieldOfTheDamned>(c => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(c); damnedCards.Add(c); });
            CustomCard.BuildCard<SoulOfTheDamned>(c => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(c); damnedCards.Add(c); });
            CustomCard.BuildCard<BulletsOfTheDamned>(c => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(c); damnedCards.Add(c); });

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
