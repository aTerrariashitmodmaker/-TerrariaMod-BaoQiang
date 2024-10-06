namespace 爆枪英雄.Items.装备
{
    [AutoloadEquip(EquipType.Body)]
    public class 丛林防弹衣 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = 8; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance += 0.05f;
            player.GetModPlayer<某些装备效果>().丛林衣 = true;
            base.UpdateEquip(player);
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.JungleShirt)
                .Register();
        }
    }
}
