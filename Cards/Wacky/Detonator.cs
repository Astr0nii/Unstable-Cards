using ClassesManagerReborn.Util;
using System.Linq;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnstableCards.Cards.NameClasses;

namespace UnstableCards.Cards.Wacky
{
    class Detonator : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = WackyClass.name;
        }
        private readonly ObjectsToSpawn[] explosionToSpawn = new ObjectsToSpawn[1];

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.bulletDamageMultiplier = 2.0f;
            gun.size = 10.0f;
            gun.projectileSpeed = 0.5f;
            gun.unblockable = true;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // add explosion effect
            if (explosionToSpawn[0] == null)
            {
                (GameObject AddToProjectile, GameObject effect, Explosion explosion) = UnstableCards.LoadExplosion("explosionDetonator", gun);

                if (explosion != null)
                {
                    explosion.force *= 10f;
                    explosion.range *= 3f;
                    explosion.damage = int.MaxValue;
                    explosion.ignoreWalls = true;

                    explosionToSpawn[0] = new ObjectsToSpawn
                    {
                        AddToProjectile = AddToProjectile,
                        direction = ObjectsToSpawn.Direction.forward,
                        effect = effect,
                        normalOffset = 1f,
                        scaleFromDamage = 1f,
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
            return "Detonator";
        }
        protected override string GetDescription()
        {
            return "Blast everything around you (including yourself) into oblivion.";
        }
        protected override GameObject GetCardArt()
        {
            return UnstableAssets.DetonatorArt;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Epic");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Self Explosion",
                    amount = "explosion!",
                    simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
                }
            };

        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }
        public override string GetModName()
        {
            return UnstableCards.ModInitials;
        }
    }
}
