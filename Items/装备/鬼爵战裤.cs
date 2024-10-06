namespace 爆枪英雄.Items.装备
{
    [AutoloadEquip(EquipType.Legs)]
    public class 鬼爵战裤 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Yellow; // The rarity of the item
            Item.defense = 17; // The amount of defense the item will give when equipped
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 2;
            base.UpdateEquip(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BeetleLeggings)
                .AddIngredient<幽鬼战裤>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
