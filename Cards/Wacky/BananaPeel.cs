using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnstableCards.Cards.NameClasses;

namespace UnstableCards.Cards.Wacky
{
    /// <summary>
    /// It's a banana. It's peel. Physics says yes.
    /// Your bullets become slippery yellow projectiles that bounce around like the
    /// floor is made of banana peel (it is now), while YOU slip-slide into a
    /// permanent mild state of speed. Nobody is safe. Least of all you.
    /// </summary>
    class BananaPeel : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = WackyClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            // The bananas themselves: fast, bouncy, deeply unserious.
            gun.projectileColor = new Color(1f, 0.9f, 0.2f); // banana, obviously
            gun.projectileSpeed = 1.3f;
            gun.projectileSize = 1.25f;
            gun.gravity = 1.5f;                              // nature calls
            gun.damage = 0.7f;                               // -30%: it's fruit, not a felony
            gun.knockback = 3.0f;                            // peel-powered propulsion
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // You laid the peel down, so naturally YOU are the one who slips.
            // Friction has left the chat. Braking is a suggestion now.
            characterStats.movementSpeed *= 1.2f;

            // Also your hands are very slippery right now. Loading takes longer.
            gunAmmo.reloadTimeAdd += 0.4f;
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override string GetTitle()
        {
            return "Banana Peel";
        }
        protected override string GetDescription()
        {
            return "Slippery bullets, slippery shoes. Cartoon physics do not care about your K/D ratio.";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
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
                    stat = "Movement Speed",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullet Speed",
                    amount = "+30%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload Time",
                    amount = "+40%",
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
            return "of the banana peel";
        }
    }
}
