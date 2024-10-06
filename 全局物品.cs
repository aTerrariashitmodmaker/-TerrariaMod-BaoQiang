
using System.Collections.Generic;
using 爆枪英雄;
using 爆枪英雄.Items.武器;
using 爆枪英雄.技能;
using Color = Microsoft.Xna.Framework.Color;

namespace 新
{
    public class 全局物品 : GlobalItem
    {
        private static void ModifyNameColor(Item item, int typ, Color color, TooltipLine line)
        {
            if (item.type == typ)
            {
                if (line.Name == "ItemName")
                {
                    line.OverrideColor = color;
                }
            }
        }
        private void AddTooltips(Item item, int type, string tooltipName, string content, List<TooltipLine> tooltips)
        {
            if (item.type == type)
            {
                tooltips.Add(new TooltipLine(Mod, tooltipName, content));

            }
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (player.GetModPlayer<技能效果>().InVehicle && item == player.HeldItem)
            {
                return false;
            }
            return base.CanUseItem(item, player);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {

            AddTooltips(item, ItemType<金蝉>(), "金蝉一", "瞬间连发：发射时有33%发射一发额外子弹", tooltips);
            AddTooltips(item, ItemType<金蝉>(), "金蝉二", "爆胆：击中目标使其防御降为0,所受伤害增加60%，并且施加困惑debuff8秒", tooltips);         
            AddTooltips(item, ItemType<迅龙>(), "迅龙一", "派生导弹：使用时有33%概率发射派生导弹，伤害为1.2倍", tooltips);
            AddTooltips(item, ItemType<迅龙>(), "迅龙二", "风雷：持握武器时获得效果，增加移动力，移动时击中敌人会产生闪电，且移速越快，闪电越多", tooltips);
            AddTooltips(item, ItemType<青蜂>(), "青蜂一", "派生导弹：使用时有33%概率发射派生导弹，伤害为1.2倍", tooltips);
            AddTooltips(item, ItemType<青蜂>(), "青蜂二", "飞镰：击中敌人有20%概率生成在敌人之间弹跳5次的飞镰，伤害为3倍", tooltips);

            foreach (TooltipLine line in tooltips)
            {

                ModifyNameColor(item, ItemType<金蝉>(), Color.OrangeRed, line);
                ModifyNameColor(item, ItemType<迅龙>(), Color.DarkBlue, line);

                if (line.Name == "金蝉一")
                {
                    line.OverrideColor = Color.Green;
                }
                if (line.Name == "金蝉二")
                {
                    line.OverrideColor = Color.Red;
                }
                if (line.Name == "屠夫一")
                {
                    line.OverrideColor = Color.Green;
                }
                if (line.Name == "屠夫二")
                {
                    line.OverrideColor = Color.Gold;
                }
                if (line.Name == "屠夫三")
                {
                    line.OverrideColor = Color.Blue;
                }
                if (line.Name == "屠夫四")
                {
                    line.OverrideColor = Color.Red;
                }
                if (line.Name == "屠夫五")
                {
                    line.OverrideColor = Color.OrangeRed;
                }
                if (line.Name == "迅龙一")
                {
                    line.OverrideColor = Color.OrangeRed;
                }
                if (line.Name == "迅龙二")
                {
                    line.OverrideColor = Color.Red;
                }
                if (line.Name == "青蜂一")
                {
                    line.OverrideColor = Color.OrangeRed;
                }
                if (line.Name == "青蜂二")
                {
                    line.OverrideColor = Color.Red;
                }

            }
            base.ModifyTooltips(item, tooltips);
        }
        private static bool MaxSkill(Player player)
        {
            if (player.GetModPlayer<技能效果>().近视
                + player.GetModPlayer<技能效果>().远视
                + player.GetModPlayer<技能效果>().隐身
                + player.GetModPlayer<技能效果>().派生导弹
                + player.GetModPlayer<技能效果>().嗜爪
                + player.GetModPlayer<技能效果>().狂暴
                + player.GetModPlayer<技能效果>().金刚钻
                + player.GetModPlayer<技能效果>().电离折射
                + player.GetModPlayer<技能效果>().欺凌
                + player.GetModPlayer<技能效果>().反击
                + player.GetModPlayer<技能效果>().先锋盾
                + player.GetModPlayer<技能效果>().全域光波
                + player.GetModPlayer<技能效果>().万弹归宗 > 7)
                return false;
            else
                return true;
        }
        public override void UpdateInventory(Item item, Player player)
        {           
            if (item.type == ItemType<远视>() && item.favorited && MaxSkill(player))
            {
                player.AddBuff(BuffType<远视效果>(), 60);
                player.GetModPlayer<技能效果>().远视 = 1;
            }
            if (item.type == ItemType<近视>() && item.favorited && MaxSkill(player))
            {
                player.AddBuff(BuffType<近视效果>(), 60);
                player.GetModPlayer<技能效果>().近视 = 1;
            }
            if (item.type == ItemType<隐身>() && item.favorited && MaxSkill(player)) player.GetModPlayer<技能效果>().隐身 = 1;
            if (item.type == ItemType<派生导弹>() && item.favorited && MaxSkill(player))
            {
                player.AddBuff(BuffType<派生导弹效果>(), 60);
                player.GetModPlayer<技能效果>().派生导弹 = 1;
            }
            if (item.type == ItemType<嗜爪>() && item.favorited && MaxSkill(player)) player.GetModPlayer<技能效果>().嗜爪 = 1;
            if (item.type == ItemType<万弹归宗>() && item.favorited && MaxSkill(player)) player.GetModPlayer<技能效果>().万弹归宗 = 1;
            if (item.type == ItemType<狂暴>() && item.favorited && MaxSkill(player)) player.GetModPlayer<技能效果>().狂暴 = 1;
            if (item.type == ItemType<金刚钻>() && item.favorited && MaxSkill(player)) player.GetModPlayer<技能效果>().金刚钻 = 1;
            if (item.type == ItemType<电离折射>() && item.favorited && MaxSkill(player)) player.GetModPlayer<技能效果>().电离折射 = 1;
            if (item.type == ItemType<反击>() && item.favorited && MaxSkill(player)) player.GetModPlayer<技能效果>().反击 = 1;
            if (item.type == ItemType<全域光波>() && item.favorited && MaxSkill(player)) player.GetModPlayer<技能效果>().全域光波 = 1;
            if (item.type == ItemType<欺凌>() && item.favorited && MaxSkill(player))
            {
                player.AddBuff(BuffType<欺凌效果>(), 60);
                player.GetModPlayer<技能效果>().欺凌 = 1;
            }
            if (item.type == ItemType<先锋盾>() && item.favorited && MaxSkill(player))
            {
                player.AddBuff(BuffType<先锋盾效果>(), 60);
                player.GetModPlayer<技能效果>().先锋盾 = 1;
            }
        }


    }
}

