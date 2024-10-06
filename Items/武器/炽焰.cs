using Terraria.DataStructures;

namespace 爆枪英雄.Items.武器
{
    public class 炽焰 : ModItem
    {
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.爆枪英雄.hjson file.

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.MagicSummonHybrid;
            Item.width = 64;
            Item.height = 25;
            Item.useTime = 5;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shoot = ProjectileType<炽焰弹幕>();
            Item.shootSpeed = 14f;
            Item.crit = 20;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int num6 = Main.rand.Next(2, 4);
            for (int index = 0; index < num6; ++index)
            {
                float SpeedX = velocity.X + Main.rand.Next(-20, 21) * 0.05f;
                float SpeedY = velocity.Y + Main.rand.Next(-20, 21) * 0.05f;
                Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0f, 0f);
            }
            for (int i = 0; i < 1; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(20));
                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);
                // Create a projectile.
                Projectile.NewProjectile(source, position, newVelocity * 0.8f, ProjectileID.BallofFire, (int)(damage * 2f), knockback);
            }

            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
           .AddIngredient(ItemID.Flamelash)
           .AddIngredient(ItemID.FlowerofFire)
           .AddIngredient(ItemID.Torch, 10)
           .AddIngredient(ItemID.HellstoneBar, 10)
           .AddTile(TileID.Anvils)
           .Register();
        }
    }
}