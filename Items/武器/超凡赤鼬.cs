
using System.Collections.Generic;
using Terraria.DataStructures;
using 爆枪英雄.声音;


namespace 爆枪英雄.Items.武器

{
    public class 超凡赤鼬 : ModItem
    {

        public int counter = 0;
        public override void SetDefaults()
        {
            Item.damage = 81;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 24;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 8;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = 声音路径.霰弹枪;
            Item.autoReuse = true;
            Item.crit = 20;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.shoot = ProjectileType<赤鼬弹幕>();
            Item.shootSpeed = 24;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 11; i++)
            {
                float randAngle = Main.rand.NextFloat(-0.32f, 0.33f);
                float randVelMultiplier = Main.rand.NextFloat(0.85f, 1.15f);
                Vector2 cwVelocity = velocity.RotatedBy(randAngle) * randVelMultiplier;
                Projectile.NewProjectile(source, position, cwVelocity, type, damage, knockback, player.whoAmI, 0f, 0f);
            }
            return false;
        }
        public override void HoldItem(Player player)
        {
            bool haveNpc = SmMthd.FindTar(player, 1500);
            counter++;
            if (counter >= 59 && haveNpc)
            {
                counter = 0;
                Projectile.NewProjectile(Item.GetSource_FromAI(), player.Center.X, player.Center.Y, 0f, 0f, ProjectileType<赤鼬导弹发射器>(), Item.damage * 2, 0, player.whoAmI, 5, 8);
            }
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Color color1 = new(255, 215 / 255, 0);
            tooltips.Add(new TooltipLine(Mod, "2", "超鼬灭：召唤更强的炮台轰击敌人"));
            foreach (TooltipLine line in tooltips)
            {
                if (line.Name == "ItemName") line.OverrideColor = color1;
                if (line.Name == "2") line.OverrideColor = Color.Green;
            }
            base.ModifyTooltips(tooltips);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BeetleShell, 20)
                .AddIngredient(ItemID.Ectoplasm, 20)
                .AddIngredient<赤鼬>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}