namespace 爆枪英雄.Items.装备
{
    [AutoloadEquip(EquipType.Head)]
    public class 丛林钢盔 : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
            // Setting IsTallHat is the only special thing this item does.
            ArmorIDs.Head.Sets.IsTallHat[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 7;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<丛林防弹衣>() && legs.type == ItemType<丛林特种裤>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "100额外生命值，100额外蓝量，2召唤栏，免疫中毒";
            player.statLifeMax2 += 100;
            player.statManaMax2 += 100;
            player.maxMinions += 2;
            player.buffImmune[BuffID.Poisoned] = true;
            base.UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                 .AddIngredient(ItemID.JungleHat)
                .Register();
            base.AddRecipes();
        }
    }
}
