namespace 爆枪英雄.技能
{
    public class 欺凌 : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.maxStack = 1;//最大堆叠数量			
            Item.value = 10000;//价值
            Item.rare = ItemRarityID.Green;//稀有度
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Torch, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}