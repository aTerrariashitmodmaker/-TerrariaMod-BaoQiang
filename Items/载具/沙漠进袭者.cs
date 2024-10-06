using System;


namespace 爆枪英雄.Items.载具
{
    public class 沙漠进袭者 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing; // how the player's arm moves when using the item
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item79; // What sound should play when using the item
            Item.noMelee = true; // this item doesn't do any melee damage
            base.SetDefaults();
        }
        public override bool CanUseItem(Player player)
        {
            if (!player.HasBuff(BuffType<载具冷却>()))
            {
                player.AddBuff(BuffType<沙漠进袭者霸符>(), 90 * 60);
                int cd = Math.Max(player.GetModPlayer<技能效果>().CdTime(180), 115 * 60);
                player.AddBuff(BuffType<载具冷却>(), cd);
                player.GetModPlayer<护盾机制>().当前载具耐久 = player.GetModPlayer<护盾机制>().最大载具耐久 = 1000 + player.statLife * 2;
                return true;
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wire, 50)
                .AddRecipeGroup(RecipeGroupID.Wood, 20)
                .AddRecipeGroup(RecipeGroupID.IronBar, 10)
                .AddTile(TileID.Anvils)
                .Register();
            base.AddRecipes();
        }
    }
}
