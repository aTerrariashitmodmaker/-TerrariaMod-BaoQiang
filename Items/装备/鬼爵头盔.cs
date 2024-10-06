using 爆枪英雄.NPCs;

namespace 爆枪英雄.Items.装备
{
    [AutoloadEquip(EquipType.Head)]
    public class 鬼爵头盔 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Yellow; // The rarity of the item
            Item.defense = 19; // The amount of defense the item will give when equipped
            base.SetDefaults();
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<鬼爵战衣>() && legs.type == ItemType<鬼爵战裤>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "每3秒恢复8%生命，35技能急速，26%射速，40%三倍暴击概率\n分身：召唤一个强劲的鬼爵分身，该分身会轮换使用迅龙，剑齿，沙鳄三种武器，分身被击败后，两分钟之后才能再次召唤";
            player.GetModPlayer<某些装备效果>().三倍暴击概率 += 40;
            player.GetModPlayer<某些装备效果>().鬼爵套 = true;
            player.GetAttackSpeed(DamageClass.Generic) += 0.26f;
            player.lifeRegen += (int)(player.statLifeMax2 * 0.08);
            if (player.GetModPlayer<某些装备效果>().分身冷却 <= 0 && (!player.HasBuff(BuffType<分身>())))
            {
                NPC.NewNPC(player.GetSource_FromAI(), (int)player.Center.X, (int)player.Center.Y, NPCType<鬼爵分身>(), default, player.whoAmI);
            }
            base.UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BeetleHelmet)
                .AddIngredient<幽鬼头盔>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
