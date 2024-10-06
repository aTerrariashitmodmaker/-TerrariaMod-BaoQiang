namespace 爆枪英雄.Items.装备
{
    [AutoloadEquip(EquipType.Head)]
    public class 幽灵副官帽 : ModItem
    {
        public int t = 0;
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
            Item.defense = 5;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<幽灵弹甲>() && legs.type == ItemType<幽灵副官裤>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "每3秒恢复2%生命，20技能急速，20%移速，17%三倍暴击概率";
            player.moveSpeed += 0.2f;
            player.GetModPlayer<某些装备效果>().三倍暴击概率 += 17;
            player.GetModPlayer<技能效果>().技能急速 += 20;
            player.lifeRegen += (int) (player.statLifeMax2 * 0.02);
            base.UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CrimsonHelmet)
                .Register();
            base.AddRecipes();
        }
    }
}
