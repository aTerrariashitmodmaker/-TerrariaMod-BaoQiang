using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using 爆枪英雄.声音;

namespace 爆枪英雄.Items.武器

{
    public class 卡特巨炮 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 63;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 72;
            Item.height = 31;
            Item.useTime = 33;
            Item.useAnimation = 33;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = 声音路径.卡特巨炮声音;
            Item.autoReuse = true;
            Item.crit = 20;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.shoot = ProjectileType<卡特巨炮炮弹>();
            Item.shootSpeed = 16;
            Item.scale = 1f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 a = velocity.RotatedBy(-36.9 / 180 * Math.PI * 0.8f);
            for (int i = 0; i < 10; i++)
            {
                Vector2 b = a.RotatedBy(8.2 / 180 * i * Math.PI * 0.8f);
                Projectile.NewProjectile(source, position, b, ProjectileType<卡特巨炮炮弹>(), Item.damage, Item.knockBack, player.whoAmI);
            }
            if (Main.rand.NextBool(3))
            {
                Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<卡特手持弹幕>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SnowmanCannon)
                .AddIngredient(ItemID.RocketLauncher)
                .AddIngredient(ItemID.Ectoplasm, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            base.AddRecipes();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "1", "最爽的武器之一"));
            tooltips.Add(new TooltipLine(Mod, "2", "击中溅射：击中敌人和墙壁都会溅射"));
            tooltips.Add(new TooltipLine(Mod, "3", "瞬间连发：33%发射额外一次弹幕，16.7%发射额外二次弹幕"));
            tooltips.Add(new TooltipLine(Mod, "4", "散射：每次射击发射10个追踪弹幕"));
            tooltips.Add(new TooltipLine(Mod, "5", "爆石：请手动添加该神级技能"));
            foreach (TooltipLine line in tooltips)
            {
                if (line.Name == "ItemName")
                    line.OverrideColor = Color.MediumPurple;
                if (line.Name == "1")
                    line.OverrideColor = Color.CadetBlue;
                if (line.Name == "2")
                    line.OverrideColor = Color.GreenYellow;
                if (line.Name == "3")
                    line.OverrideColor = Color.Wheat;
                if (line.Name == "4")
                    line.OverrideColor = Color.Red;
                if (line.Name == "5")
                    line.OverrideColor = Color.Red;
            }
            base.ModifyTooltips(tooltips);
        }
    }
}