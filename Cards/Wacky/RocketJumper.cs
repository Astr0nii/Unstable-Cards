﻿using RarityLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace UnstableCards.Cards.Wacky
{
    class RocketJumper : CustomCard
    {
        private readonly ObjectsToSpawn[] explosionToSpawn = new ObjectsToSpawn[1];

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.projectileColor = Color.yellow;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gun.damage = 0f;
            gunAmmo.maxAmmo += 25;
            player.data.healthHandler.regeneration += 2;
            characterStats.gravity *= 0.5f;

            // add explosion effect
            if (explosionToSpawn[0] == null)
            {
                (GameObject AddToProjectile, GameObject effect, Explosion explosion) = UnstableCards.LoadExplosion("explosionRocketJumper", gun);

                explosion.force *= 16f;
                explosion.range *= 4f;
                explosion.damage = 0f;

                explosionToSpawn[0] = new ObjectsToSpawn
                {
                    AddToProjectile = AddToProjectile,
                    direction = ObjectsToSpawn.Direction.forward,
                    effect = effect,
                    normalOffset = 0.1f,
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
            gun.objectsToSpawn = gun.objectsToSpawn.Concat(explosionToSpawn).ToArray();
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
            return "Nearly Harmless rockets! Meant for rocket jumping practice sessions! (or knocking your friends to infinity and beyond)";
        }
        protected override GameObject GetCardArt()
        {
            return null;
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
                    amount = "Almost Harmless",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "+25",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Life Regeneration",
                    amount = "+2hp/s",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Gravity",
                    amount = "-50%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                }
            };

        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }
        public override string GetModName()
        {
            return UnstableCards.ModInitialsWacky;
        }
    }
}
