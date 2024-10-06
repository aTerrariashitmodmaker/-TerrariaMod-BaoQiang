namespace 爆枪英雄.Items.装置
{
    public class 镭射穿梭器 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 62;
            Item.value = 1000000;
            Item.rare = ItemRarityID.Purple;
            Item.scale = 1f;
            Item.accessory = true;

            base.SetDefaults();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 0.1f;
            int a = (int)(player.statLifeMax2 * 0.2f);
            player.statLifeMax2 += a;
            player.GetModPlayer<技能效果>().镭射穿梭器 = true;
            player.GetModPlayer<技能效果>().技能急速 += 15f;
            base.UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.RodofDiscord)
                .AddIngredient(ItemID.Glass, 5)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            base.AddRecipes();
        }
        public override void UpdateInventory(Player player)
        {

            base.UpdateInventory(player);
        }
    }
}