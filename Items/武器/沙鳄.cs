using Terraria.DataStructures;
using 爆枪英雄.声音;

namespace 爆枪英雄.Items.武器

{
    public class 沙鳄 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 41;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 21;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = 声音路径.霰弹枪;
            Item.autoReuse = true;
            Item.crit = 20;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.shoot = ProjectileType<沙鳄弹幕>();
            Item.shootSpeed = 19;
            Item.scale = 1f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 11; i++)
            {
                float randAngle = Main.rand.NextFloat(-0.32f, 0.33f);
                float randVelMultiplier = Main.rand.NextFloat(0.85f, 1.15f);
                Vector2 cwVelocity = velocity.RotatedBy(randAngle) * randVelMultiplier;
                Projectile.NewProjectile(source, position, cwVelocity, type, damage, knockback, player.whoAmI, -1f, 0f);
            }
            return false;
        }
        public override void HoldItem(Player player)
        {
            player.GetModPlayer<技能效果>().快感 = true;
            base.HoldItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.OnyxBlaster)
                .AddIngredient(ItemID.AncientBattleArmorMaterial)
                .AddIngredient(ItemID.IllegalGunParts)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}