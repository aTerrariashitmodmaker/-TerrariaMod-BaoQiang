using Terraria.DataStructures;

namespace 爆枪英雄.Items.装置

{
    public class 闪电塔 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 3;
            Item.DamageType = DamageClass.Summon;
            Item.sentry = true;
            Item.mana = 10;
            Item.width = 66;
            Item.height = 68;
            Item.useTime = Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5f;
            Item.value = 100000;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.shoot = ProjectileType<闪电塔哨兵>();
        }
        public override bool CanUseItem(Player player)
        {
            if (NPC.downedSlimeKing) Item.damage = 4;
            if (NPC.downedBoss1) Item.damage = 5;
            if (NPC.downedBoss2) Item.damage = 7;
            if (NPC.downedQueenBee) Item.damage = 9;
            if (NPC.downedBoss3) Item.damage = 11;
            if (Main.hardMode) Item.damage = 15;
            if (NPC.downedQueenSlime) Item.damage = 18;
            if (NPC.downedMechBossAny) Item.damage = 21;
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) Item.damage = 25;
            if (NPC.downedPlantBoss) Item.damage = 30;
            if (NPC.downedGolemBoss) Item.damage = 35;
            if (NPC.downedAncientCultist) Item.damage = 40;
            if (NPC.downedMoonlord) Item.damage = 50;

            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //CalamityUtils.OnlyOneSentry(player, type);
            int p = Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, type, damage, knockback, player.whoAmI, 16f);
            if (Main.projectile.IndexInRange(p))
                Main.projectile[p].originalDamage = Item.damage;
            player.UpdateMaxTurrets();
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Glass, 10)
                .AddIngredient(ItemID.CopperBar, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.Glass, 10)
                .AddIngredient(ItemID.TinBar, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            base.AddRecipes();
        }

    }
}