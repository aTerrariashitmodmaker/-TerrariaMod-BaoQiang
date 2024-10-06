using Terraria.DataStructures;

namespace 爆枪英雄.Items.武器
{
    public class 寒霜 : ModItem
    {
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.爆枪英雄.hjson file.

        public override void SetDefaults()
        {
            Item.damage = 52;
            Item.DamageType = DamageClass.MagicSummonHybrid;
            Item.width = 64;
            Item.height = 23;
            Item.useTime = 3;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.shoot = ProjectileType<寒霜弹幕>();
            Item.shootSpeed = 9.5f;
            Item.useTurn = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextBool(2))
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(20));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                // Create a projectile.
                Projectile.NewProjectile(source, position, newVelocity * 1.3f, ProjectileID.BallofFrost, (int)(damage * 2.1f), knockback);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
           .AddIngredient(ItemID.FrostStaff)
           .AddIngredient(ItemID.FlowerofFrost)
           .AddIngredient(ItemID.SoulofLight, 10)
           .AddIngredient(ItemID.SoulofNight, 10)
           .AddTile(TileID.MythrilAnvil)
           .Register();
        }
    }
}