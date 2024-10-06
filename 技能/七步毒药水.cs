namespace 爆枪英雄.技能
{
    public class 七步毒药水 : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.UseSound = SoundID.Item3;
            Item.useTurn = true;
            Item.maxStack = 1;//最大堆叠数量
            Item.consumable = false; // 标记为可消耗			
            Item.value = 10000;//价值
            Item.rare = ItemRarityID.Red;//稀有度
            Item.useAnimation = 15;
            Item.useTime = 15;
        }
        public override bool? UseItem(Player player)
        {
            if (player.GetModPlayer<技能效果>().神级技能 == true)
            {
                return false;
            }
            else
            {
                player.AddBuff(BuffType<七步毒>(), 60 * 60 * 9999);
                return true;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
               .AddIngredient(ItemID.Stinger, 10)
               .AddIngredient(ItemID.VialofVenom, 5)
               .AddTile(TileID.Anvils)
               .Register();

        }
    }
}