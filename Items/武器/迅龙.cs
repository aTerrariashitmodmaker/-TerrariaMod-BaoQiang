using Terraria.DataStructures;
using 爆枪英雄.声音;

namespace 爆枪英雄.Items.武器

{
    public class 迅龙 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 70;
            Item.height = 31;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = 声音路径.步枪;
            Item.autoReuse = true;
            Item.crit = 20;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.shoot = ProjectileType<迅龙弹幕>();
            Item.shootSpeed = 25;
            Item.scale = 1f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, -1f);
            if (Main.rand.NextBool(3))
            {
                Projectile.NewProjectile(source, position, velocity * 17 / 25f, ProjectileType<小导弹>(), (int)(damage * 1.2f), knockback);
            }
            return false;
        }

        public override void HoldItem(Player player)
        {
            player.AddBuff(BuffType<风雷>(), 60);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Ectoplasm)
                .AddIngredient(ItemID.ChlorophyteBar)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}