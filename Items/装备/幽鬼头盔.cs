namespace 爆枪英雄.Items.装备
{
    [AutoloadEquip(EquipType.Head)]
    public class 幽鬼头盔 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.LightPurple; // The rarity of the item
            Item.defense = 8; // The amount of defense the item will give when equipped
            base.SetDefaults();
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<幽鬼战衣>() && legs.type == ItemType<幽鬼战裤>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "每3秒恢复4%生命，25技能急速，30%闪避概率，28%三倍暴击概率";
            player.GetModPlayer<某些装备效果>().三倍暴击概率 += 28;
            player.GetModPlayer<某些装备效果>().闪避概率 += 30;
            player.GetModPlayer<技能效果>().技能急速 += 25;
            player.lifeRegen += (int)(player.statLifeMax2 * 0.04);
            base.UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CrystalNinjaHelmet)
                .AddIngredient<幽灵副官帽>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
