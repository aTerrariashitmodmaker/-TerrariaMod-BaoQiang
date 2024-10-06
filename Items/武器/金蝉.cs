using Terraria.DataStructures;
using 爆枪英雄.声音;

namespace 爆枪英雄.Items.武器
{
    public class 金蝉 : ModItem
    {
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.爆枪英雄.hjson file.

        public override void SetDefaults()
        {
            Item.damage = 189;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 18;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = 声音路径.狙击枪;
            Item.autoReuse = true;
            Item.shoot = ProjectileType<金蝉弹幕>();
            Item.shootSpeed = 26;
            Item.useTurn = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextBool(3))
            {
                Projectile.NewProjectile(player.GetSource_FromAI(), position, velocity * 1.1f, type, damage, knockback, player.whoAmI);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
           .AddIngredient(ItemID.SniperRifle)
           .AddIngredient(ItemID.Ectoplasm, 10)
           .AddTile(TileID.MythrilAnvil)
           .Register();
        }
    }
}