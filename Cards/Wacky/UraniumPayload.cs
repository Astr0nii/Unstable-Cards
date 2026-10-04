using ClassesManagerReborn.Util;
using System.Linq;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnstableCards.Cards.NameClasses;

namespace UnstableCards.Cards.Wacky
{
    class UraniumPayload : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = WackyClass.name;
        }
        private readonly ObjectsToSpawn[] explosionToSpawn = new ObjectsToSpawn[1];

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.attackSpeed = 4.0f;
            gun.projectileColor = Color.green;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gun.damage *= 4f;

            characterStats.movementSpeed *= 0.7f;

            gun.attackSpeed *= 2f;

            // add explosion effect
            if (explosionToSpawn[0] == null)
            {
                (GameObject AddToProjectile, GameObject effect, Explosion explosion) = UnstableCards.LoadExplosion("explosionUraniumPayload", gun);

                if (explosion != null)
                {
                    // Rebalanced: the old blast (force x16, range x3) nuked entire rooms on every shot.
                    // Uranium is still unstable, just a *smaller* kind of unstable.
                    explosion.force *= 8f;
                    explosion.range *= 1.5f;

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
            return "Uranium Payload";
        }
        protected override string GetDescription()
        {
            return "Makes your bullets explosive. Ammunition infused with 20KG of <color=#84FF00>Uranium 235</color> for unstable results. 100% Compliant with OSHA guidelines! (Now only a *localised* radiation leak.)";
        }
        protected override GameObject GetCardArt()
        {
            return UnstableAssets.UraniumPayloadArt;
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
                    stat = "Instability",
                    amount = "+9999%",
                    simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "ATKSPD",
                    amount = "8.0s",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Blast Radius",
                    amount = "Small-ish",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Movement Speed",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                }
            };

        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.PoisonGreen;
        }
        public override string GetModName()
        {
            return UnstableCards.ModInitials;
        }
    }
}
