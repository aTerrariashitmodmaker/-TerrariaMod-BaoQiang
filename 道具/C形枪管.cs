namespace 爆枪英雄.Items.道具
{
    public class C形枪管 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 44;
            Item.value = 100000;
            Item.rare = ItemRarityID.Green;
            Item.scale = 1f;
            Item.accessory = true;

            base.SetDefaults();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Generic) += 12;
            player.GetModPlayer<技能效果>().C形枪管 = true;
            base.UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            CreateRecipe()

                .AddIngredient(ItemID.Glass, 5)
                .AddIngredient(ItemID.Cobweb, 30)
                .AddIngredient(ItemID.LuckyHorseshoe)
                .AddRecipeGroup(RecipeGroupID.IronBar, 10)
                .AddTile(TileID.Anvils)
                .Register();
            base.AddRecipes();
        }
        public override void UpdateInventory(Player player)
        {

            base.UpdateInventory(player);
        }
    }
}