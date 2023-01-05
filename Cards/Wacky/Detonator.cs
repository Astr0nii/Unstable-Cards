using Photon.Pun.Simple;
using RarityLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using static CardInfoStat;
using static UnityEngine.Random;

namespace UnstableCards.Cards.Wacky
{
    class Detonator : CustomCard
    {
        private readonly ObjectsToSpawn[] explosionToSpawn = new ObjectsToSpawn[1];

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.destroyBulletAfter = 0.02f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // add explosion effect
            if (explosionToSpawn[0] == null)
            {
                (GameObject AddToProjectile, GameObject effect, Explosion explosion) = UnstableCards.LoadInstantExplosion("explosionBomberVest", gun);

                explosion.force *= 16f;
                explosion.range *= 8f;

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
                    zeroZ = true,
                    
                };
            }
            gun.objectsToSpawn = gun.objectsToSpawn.Concat(explosionToSpawn).ToArray();
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
            return "Blast everything (including yourself) into oblivion.";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
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
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                }
            };

        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }
        public override string GetModName()
        {
            return UnstableCards.ModInitialsWacky;
        }
    }
}
