using System.Collections.Generic;

namespace 爆枪英雄.Items.武器

{
    public class 光锥 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 210;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 26;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5;
            Item.value = 100000;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.crit = 7;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.shoot = ProjectileType<光锥激光>();
            Item.shootSpeed = 25;
            Item.scale = 1f;
            Item.channel = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LastPrism, 1)
                .AddIngredient(ItemID.LunarBar, 5)
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddTile(TileID.LunarCraftingStation);
            base.AddRecipes();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "1", "致盲：击中敌人使其攻击25%无效"));
            tooltips.Add(new TooltipLine(Mod, "2", "致残：击中敌人使其降低50%接触伤害"));
            tooltips.Add(new TooltipLine(Mod, "3", "炫目：击中敌人有1%闪避敌人攻击"));
            tooltips.Add(new TooltipLine(Mod, "4", "欺盲：击中敌人能增加对其伤害"));
            foreach (TooltipLine line in tooltips)
            {
                if (line.Name == "ItemName") line.OverrideColor = Color.CadetBlue;
                if (line.Name == "1") line.OverrideColor = Color.Blue;
                if (line.Name == "2") line.OverrideColor = Color.Green;
                if (line.Name == "3") line.OverrideColor = Color.OrangeRed;
                if (line.Name == "4") line.OverrideColor = Color.MediumPurple;
            }
            base.ModifyTooltips(tooltips);
        }
    }

}