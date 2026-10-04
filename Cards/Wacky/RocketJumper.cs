using ClassesManagerReborn.Util;
using System.Linq;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnstableCards.Cards.NameClasses;

namespace UnstableCards.Cards.Wacky
{
    class RocketJumper : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = WackyClass.name;
        }
        private readonly ObjectsToSpawn[] explosionToSpawn = new ObjectsToSpawn[1];

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.projectileColor = Color.yellow;
            gun.bulletDamageMultiplier = 0f;
            gun.gravity = 0f;
            gun.projectileSpeed = 2f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gunAmmo.maxAmmo += 10;
            characterStats.gravity *= 0.85f;

            // add explosion effect
            if (explosionToSpawn[0] == null)
            {
                (GameObject AddToProjectile, GameObject effect, Explosion explosion) = UnstableCards.LoadExplosion("explosionRocketJumper", gun);

                if (explosion != null)
                {
                    explosion.force *= 10f;
                    explosion.range *= 2.25f;
                    explosion.damage = 0f;

                    explosionToSpawn[0] = new ObjectsToSpawn
                    {
                        AddToProjectile = AddToProjectile,
                        direction = ObjectsToSpawn.Direction.forward,
                        effect = effect,
                        normalOffset = 0.1f,
                        scaleFromDamage = 0f,
                        scaleStackM = 0.2f,
                        scaleStacks = true,
                        spawnAsChild = false,
                        spawnOn = ObjectsToSpawn.SpawnOn.all,
                        stacks = 1,
                        stickToAllTargets = false,
                        stickToBigTargets = false,
                        zeroZ = false,
                    };
                }
            }
            if (explosionToSpawn[0] != null && !gun.objectsToSpawn.Contains(explosionToSpawn[0]))
            {
                gun.objectsToSpawn = gun.objectsToSpawn.Append(explosionToSpawn[0]).ToArray();
            }
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gun.objectsToSpawn = gun.objectsToSpawn.Except(explosionToSpawn).ToArray();
        }

        protected override string GetTitle()
        {
            return "Rocket Jumper";
        }
        protected override string GetDescription()
        {
            return "Harmless rockets! Meant for rocket jumping practice sessions! (or knocking your friends to infinity and beyond!)";
        }
        protected override GameObject GetCardArt()
        {
            return UnstableAssets.RocketJumperArt;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Scarce");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage",
                    amount = "Harmless",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "+10",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Gravity",
                    amount = "-15%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                }
            };

        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }
        public override string GetModName()
        {
            return UnstableCards.ModInitials;
        }
    }
}
