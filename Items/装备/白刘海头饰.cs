namespace 爆枪英雄.Items.装备
{
    [AutoloadEquip(EquipType.Head)]
    public class 白刘海头饰 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.White; // The rarity of the item
            Item.defense = 2; // The amount of defense the item will give when equipped
            base.SetDefaults();
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "提升少许生命恢复，移速，伤害，减伤，攻击造成7点额外伤害";
            player.lifeRegen += 4;
            player.moveSpeed += 0.17f;
            player.GetDamage(DamageClass.Generic) += 0.07f;
            player.endurance += 0.07f;
            player.GetModPlayer<某些装备效果>().白刘海 = true;
            base.UpdateArmorSet(player);
        }
    }
}
