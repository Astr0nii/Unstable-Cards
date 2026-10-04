using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnstableCards.Cards.NameClasses;

namespace UnstableCards.Cards.Wacky
{
    /// <summary>
    /// "Gimme the keys!" Your bullets are now a used car lot.
    /// Every shot fires a slow, bouncy little car that does contact damage,
    /// careens around the room, and leaves a *mild* oil leak wherever it rolls.
    /// Wacky, useless, and somehow still on fire. Three for the price of one (insurance not included).
    /// </summary>
    class Dealership : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = WackyClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            // A convoy of tiny cars: slow, chunky, bouncy and only mildly dangerous.
            gun.numberOfProjectiles += 3;
            gun.projectileSpeed = 0.6f;
            gun.projectileSize = 1.5f;
            gun.projectileColor = new Color(0.9f, 0.2f, 0.15f); // guaranteed accident-free red
            gun.damage = 0.4f;                                  // -60% per bullet, they're small cars
            gun.knockback = 2.0f;                               // light bump, like a shopping trolley
            gun.gravity = 0.5f;                                 // these things definitely have suspension issues
            gun.spread = 0.2f;                                  // parking is not their strong suit
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // The cars are loud. Very loud. Your ears were not consulted.
            gunAmmo.reloadTimeAdd += 0.5f;
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override string GetTitle()
        {
            return "Dealership";
        }
        protected override string GetDescription()
        {
            return "Your bullets are now tiny used cars. They bounce, they honk, they leave mild oil leaks. All sales final.";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }
        protected override CardInfoMod.Brief GetBrief()
        {
            return new CardInfoMod.Brief(CardInfoMod.Brief.Direction.None);
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullets",
                    amount = "+3",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullet Size",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-60%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Bullet Speed",
                    amount = "-40%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload Time",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                }
            };
        }
        protected override CardCategory GetCategory()
        {
            return null;
        }
        protected override string GetModdableText()
        {
            return "of the dealership";
        }
    }
}
