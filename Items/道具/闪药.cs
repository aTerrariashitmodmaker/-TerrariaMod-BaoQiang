namespace 爆枪英雄.Items.道具
{
    public class 闪药 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 27;
            Item.height = 39;
            Item.value = 10000;
            Item.rare = ItemRarityID.Lime;
            Item.maxStack = 9999;
            Item.scale = 1f;
            Item.consumable = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.buffType = BuffType<快乐>();
            Item.buffTime = 10 * 60 * 60;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item3;

            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ItemType<闪耀重铸石>())
                .AddIngredient(ItemID.RagePotion, 5)
                .AddTile(TileID.Bottles)
                .Register();
            base.AddRecipes();
        }
    }
}