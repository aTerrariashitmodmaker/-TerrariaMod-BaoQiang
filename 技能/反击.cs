namespace 爆枪英雄.技能
{
    public class 反击 : ModItem
    {

        public override void SetDefaults()
        {
            Item.maxStack = 1;//最大堆叠数量
            Item.consumable = false; // 标记为可消耗			
            Item.value = 10000;//价值
            Item.rare = ItemRarityID.Green;//稀有度     
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LifeCrystal, 1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}