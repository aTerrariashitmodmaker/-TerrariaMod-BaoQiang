
using System.Collections.Generic;
using 爆枪英雄.NPCs;
using 爆枪英雄.声音;

namespace 爆枪英雄.Items.武器

{
    public class 烟花弩 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 46;
            Item.height = 28;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(0, 10);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = null;
            Item.autoReuse = true;
            Item.crit = 10;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.shoot = ProjectileType<烟花弩手持弹幕>();
            Item.shootSpeed = 8f;
            Item.scale = 1f;
            Item.channel=true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return !player.HasBuff(BuffType<烟花弩冷却>());
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2  )
            {
                player.AddBuff(BuffType<烟花弩冷却>(), player.GetModPlayer<技能效果>().CdTime(40));
                Item item=SmMthd.FindMaxDmgItem(player);
                Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, ProjectileType<烟花弩主动弹幕>(), 20 +item.damage, 6, player.whoAmI);
                return false;
            }
            return base.CanUseItem(player);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "1", "烟花:这是一把真正的烟花,右键主动降下一片烟花雨，伤害与背包中最高伤害武器有关，冷却40秒"));         
            foreach (TooltipLine line in tooltips)
            {
                if (line.Name == "ItemName") line.OverrideColor = Main.DiscoColor;
                if (line.Name == "1") line.OverrideColor = Main.DiscoColor;
              
            }
            base.ModifyTooltips(tooltips);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BlueRocket,10)
                .AddIngredient(ItemID.YellowRocket,10)
                .AddIngredient(ItemID.RedRocket,10)
                .AddIngredient(ItemID.GreenRocket,10)
                .AddIngredient(ItemID.RainbowBrick,10)
                .AddIngredient(ItemID.PixieDust,20)
                .AddIngredient(ItemID.UnicornHorn,2)
                .AddIngredient(ItemID.SoulofLight,10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            base.AddRecipes();
        }

    }
}