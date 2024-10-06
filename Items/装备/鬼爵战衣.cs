namespace 爆枪英雄.Items.装备
{
    [AutoloadEquip(EquipType.Body)]
    public class 鬼爵战衣 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Yellow; // The rarity of the item
            Item.defense = 23; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 2;
            base.UpdateEquip(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BeetleScaleMail)
                .AddIngredient<幽鬼战衣>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
