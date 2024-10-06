namespace 爆枪英雄.Items.装备
{
    [AutoloadEquip(EquipType.Head)]
    public class 暗影作训帽 : ModItem
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
            Item.defense = 5;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<暗影作训衣>() && legs.type == ItemType<暗影作训裤>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "20%闪避概率，10%伤害，20%移速，17%射击速度";
            player.GetDamage(DamageClass.Generic) += 0.1f;
            player.moveSpeed += 0.2f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.17f;
            player.GetModPlayer<某些装备效果>().闪避概率 += 20;
            base.UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ShadowHelmet)
                .Register();
            base.AddRecipes();
        }
    }
}
