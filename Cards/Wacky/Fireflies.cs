using ClassesManagerReborn.Util;
using HarmonyLib;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnstableCards.Cards.NameClasses;
using static UnityEngine.Random;

namespace UnstableCards.Cards.Wacky
{
    class Fireflies : CustomCard
    {
        // How long a single firefly lives for, in seconds. After that it flickers out of existence.
        internal const float FIREFLY_LIFESPAN = 2.0f;

        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = WackyClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.spread = 0.1f;
            gun.projectileColor = Color.yellow;
            gun.gravity = 0;
            gun.projectileSpeed = 0.7f;
            gun.ignoreWalls = true;
            gun.damage = 0.2f;
            gun.multiplySpread = 1.75f;

        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gun.cos = Range(0.05f, 0.35f);
            gun.numberOfProjectiles += 4;
            gunAmmo.maxAmmo += 4;
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override string GetTitle()
        {
            return "Fireflies";
        }
        protected override string GetDescription()
        {
            return "Shoot slow moving fireflies, You won't believe your eyes! They phase through walls, but they don't live long.";
        }
        protected override GameObject GetCardArt()
        {
            return UnstableAssets.FirefliesArt;
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
                    positive = true,
                    stat = "Bullets",
                    amount = "+4",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Ignore Walls",
                    amount = "true",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Bullet Spread",
                    amount = "+75%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Bullet Speed",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Bullet Damage",
                    amount = "-80%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Bullet Lifespan",
                    amount = $"{FIREFLY_LIFESPAN:0.0}s",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                }
            };

        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }
        public override string GetModName()
        {
            return UnstableCards.ModInitials;
        }
    }

    /// <summary>
    /// Attaches the Fireflies lifespan timer to every Fireflies projectile as it is instantiated.
    /// Runs locally on every client, so the phasing fireflies poof away at the same moment for everyone.
    /// </summary>
    [HarmonyPatch(typeof(Gun), nameof(Gun.InstantiateProjectile))]
    internal static class Fireflies_ProjectileLifetime_Patch
    {
        const string FIREFLIES_CARD_NAME = "Fireflies";

        static void Postfix(Gun __instance, GameObject __result)
        {
            if (__result == null) return;

            // Only our own firefly gun gets a lifespan, every other bullet is left alone.
            CardInfo cardInfo = __instance.GetComponent<CardInfo>();
            bool isFireflies = cardInfo != null && cardInfo.cardName == FIREFLIES_CARD_NAME;
            if (!isFireflies && __instance.name != FIREFLIES_CARD_NAME) return;

            FirefliesLifespan lifespan = __result.AddComponent<FirefliesLifespan>();
            lifespan.lifespan = Fireflies.FIREFLY_LIFESPAN;
        }
    }
}
