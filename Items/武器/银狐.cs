
using System.Collections.Generic;
using 爆枪英雄.NPCs;
using 爆枪英雄.声音;

namespace 爆枪英雄.Items.武器

{
    public class 银狐 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 27;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 46;
            Item.height = 28;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(0, 10);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = 声音路径.步枪;
            Item.autoReuse = true;
            Item.crit = 10;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.shoot = ProjectileType<银狐弹幕>();
            Item.shootSpeed = 20f;
            Item.scale = 1f;
        }
        int BatCount(Player player)
        {
            int batCount = 0;
            foreach (var npc in Main.npc)
            {
                if (npc.type == NPCType<友好蝙蝠>() && npc.ai[0] == player.whoAmI)
                {
                   batCount++;
                }
            }
            return batCount;
        }
        public override void HoldItem(Player player)
        {
            player.GetModPlayer<技能效果>().快感 = true;
            player.GetModPlayer<技能效果>().BatCount=BatCount(player);
            base.HoldItem(player);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "1", "穿人个数:3"));
            tooltips.Add(new TooltipLine(Mod, "2", "快感：手持武器时造成伤害有概率提升100%攻速和45%召唤伤害持续2秒，可叠加时间"));
            tooltips.Add(new TooltipLine(Mod, "3", "召唤蝙蝠：每击中敌人10次召唤一只吸血蝙蝠，最多10只"));
            foreach (TooltipLine line in tooltips)
            {
                if (line.Name == "ItemName") line.OverrideColor = Color.Silver;
                if (line.Name == "1") line.OverrideColor = Color.Cyan;
                if (line.Name == "2") line.OverrideColor = Color.Green;
                if (line.Name == "3") line.OverrideColor = Color.OrangeRed;
            }
            base.ModifyTooltips(tooltips);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PhoenixBlaster)
                .AddIngredient<神雀>(1)
                .AddTile(TileID.Hellforge)
                .Register();
            base.AddRecipes();
        }

    }
}