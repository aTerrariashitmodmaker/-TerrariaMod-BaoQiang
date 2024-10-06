using Terraria.DataStructures;
using 爆枪英雄.声音;

namespace 爆枪英雄.Items.武器

{
    public class 青蜂 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 22;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = 声音路径.步枪;
            Item.autoReuse = true;
            Item.crit = 20;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.shoot = ProjectileType<青蜂弹幕>();
            Item.shootSpeed = 24f;
            Item.scale = 1f;
        }
        public override bool CanUseItem(Player player)
        {
            if (NPC.downedBoss1)
            {
                Item.damage = 8;
            }
            if (NPC.downedBoss2)
            {
                Item.damage = 12;
            }
            if (NPC.downedQueenBee)
            {
                Item.damage = 16;
            }
            if (NPC.downedBoss3)
            {
                Item.damage = 20;
            }
            if (NPC.downedDeerclops)
            {
                Item.damage = 22;
            }
            if (Main.hardMode)
            {
                Item.damage = 28;
                Item.useTime = 8;
                Item.useAnimation = 8;
            }
            if (NPC.downedMechBossAny)
            {
                Item.damage = 38;
                Item.crit = 45;
            }

            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextBool(3))
            {
                Projectile.NewProjectile(source, position, velocity * 17 / 25f, ProjectileType<小导弹>(), (int)(damage * 1.2f), knockback);
            }

            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood)
                .AddIngredient(ItemID.FallenStar)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}