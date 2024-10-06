using Terraria.DataStructures;
using 爆枪英雄.声音;

namespace 爆枪英雄.Items.武器
{
    public class 剑齿 : ModItem
    {
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.爆枪英雄.hjson file.

        public override void SetDefaults()
        {
            Item.damage = 123;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 20;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = 声音路径.狙击枪;
            Item.autoReuse = true;
            Item.shoot = ProjectileType<剑齿弹幕>();
            Item.shootSpeed = 20;
            Item.useTurn = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, -1f);
            if (player.velocity.X == 0 || player.velocity.Y == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    float randAngle = Main.rand.NextFloat(-0.35f, 0.34f);
                    float randVelMultiplier = Main.rand.NextFloat(0.7f, 1.3f);
                    Vector2 cwVelocity = velocity.RotatedBy(randAngle);
                    cwVelocity.Normalize();
                    Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, cwVelocity * 10f * randVelMultiplier, ProjectileType<影灭弹幕>(), damage * 2, 6);
                }
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
           .AddIngredient(ItemID.SniperRifle)
           .AddIngredient(ItemID.ShadowbeamStaff)
           .AddIngredient(ItemID.IllegalGunParts)
           .AddTile(TileID.MythrilAnvil)
           .Register();
        }
    }
}