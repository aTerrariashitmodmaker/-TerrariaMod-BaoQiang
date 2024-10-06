using System.Collections.Generic;

namespace 爆枪英雄.Prefix
{
    public class 闪耀 : ModPrefix
    {
        public virtual float Power => 1.25f;
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override float RollChance(Item item)
        {
            return 0f;
        }

        public override bool CanRoll(Item item)
        {
            return true;
        }


        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            base.SetStats(ref damageMult, ref knockbackMult, ref useTimeMult, ref scaleMult, ref shootSpeedMult, ref manaMult, ref critBonus);
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.03f * Power;
        }
        public override void ApplyAccessoryEffects(Player player)
        {
            //player.GetModPlayer<技能效果>().技能急速 -= 0.1f;
            player.GetModPlayer<技能效果>().技能急速 += 15f;
            base.ApplyAccessoryEffects(player);
        }
        public override void Apply(Item item)
        {
            base.Apply(item);
        }
        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "Prefix", "15点技能急速")
            {
                IsModifier = true, // Sets the color to the positive modifier color.
            };
        }
    }
    public class 绝世 : ModPrefix
    {
        public virtual float Power => 1.25f;
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override float RollChance(Item item)
        {
            return 0f;
        }

        public override bool CanRoll(Item item)
        {
            return true;
        }


        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            base.SetStats(ref damageMult, ref knockbackMult, ref useTimeMult, ref scaleMult, ref shootSpeedMult, ref manaMult, ref critBonus);
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.03f * Power;
        }
        public override void ApplyAccessoryEffects(Player player)
        {
            // player.GetModPlayer<技能效果>().技能急速 -= 0.2f;
            player.GetModPlayer<技能效果>().技能急速 += 27.5f;
            base.ApplyAccessoryEffects(player);
        }
        public override void Apply(Item item)
        {
            base.Apply(item);
        }
        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "Prefix", "28点技能急速")
            {
                IsModifier = true, // Sets the color to the positive modifier color.
            };
        }
    }
}