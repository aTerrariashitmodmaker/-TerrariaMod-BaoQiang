namespace 爆枪英雄.Items.道具

{
    public class 闪耀重铸石 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.scale = 1f;
            Item.maxStack = 9999;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ManaCrystal, 2)
                .AddTile(TileID.Anvils)
                .Register();
            base.AddRecipes();
        }

    }
    public class 绝世重铸石 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.value = 100000;
            Item.rare = ItemRarityID.Lime;
            Item.scale = 1f;
            Item.maxStack = 9999;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemType<闪耀重铸石>())
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            base.AddRecipes();
        }

    }
}