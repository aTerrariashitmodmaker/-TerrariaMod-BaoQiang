using System.Collections.Generic;
using Terraria.DataStructures;

namespace 爆枪英雄.Items.武器
{
    public class 烁金 : ModItem
    {
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.爆枪英雄.hjson file.

        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 25;
            Item.useTime = 4;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6;
            Item.value = 1000000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shoot = ProjectileType<烁金弹幕>();
            Item.shootSpeed = 19f;
            Item.crit = 20;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float rot = Main.rand.NextFloat(-0.07f, 0.071f);
            Vector2 vel=Vector2.Normalize(velocity)*45f;
            Projectile.NewProjectile(source, position+vel.RotatedBy(rot), velocity.RotatedBy( rot), type, damage, knockback,player.whoAmI);
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {        
            tooltips.Add(new TooltipLine(Mod, "3", "众口烁金：手持武器时，每击中目标27次，造成35倍外加目标最大生命值5%（不超过2000）的伤害。(爆石洗不出来吗(●ˇ∀ˇ●))"));
            foreach (TooltipLine line in tooltips)
            {
                if (line.Name == "ItemName") line.OverrideColor = Color.MediumPurple;              
                if (line.Name == "3") line.OverrideColor = Color.Green;
            }
            base.ModifyTooltips(tooltips);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
           .AddIngredient<寒霜>()
           .AddIngredient<炽焰>()
           .AddIngredient(ItemID.FragmentSolar, 10)
           .AddTile(TileID.LunarCraftingStation)
           .Register();
        }
    }
}