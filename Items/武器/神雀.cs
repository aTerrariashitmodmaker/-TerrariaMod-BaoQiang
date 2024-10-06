using Terraria.DataStructures;

namespace 爆枪英雄.Items.武器

{
    public class 神雀 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 36;
            Item.height = 20;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5;
            Item.value = 100000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.crit = 4;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.shoot = ProjectileType<神雀弹幕>();
            Item.shootSpeed = 25;
            Item.scale = 1f;
        }
        public override void HoldItem(Player player)
        {
            player.GetModPlayer<技能效果>().七步毒 = true;
            player.GetModPlayer<技能效果>().快感 = true;
            base.HoldItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextBool(6))
            {
                Projectile.NewProjectile(source, position, velocity * 17 / 25f, ProjectileType<小导弹>(), (int)(damage * 1.2f), knockback);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

    }
}