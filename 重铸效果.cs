using Terraria.Utilities;
using 爆枪英雄.Items.道具;
using 爆枪英雄.Prefix;

namespace 爆枪英雄
{
    public class 重铸物品 : GlobalItem
    {
        public override void UpdateInventory(Item item, Player player)
        {
            if (item.type == ItemType<闪耀重铸石>())
            {
                player.GetModPlayer<重铸玩家>().闪耀重铸 = true;
            }
            if (item.type == ItemType<绝世重铸石>())
            {
                player.GetModPlayer<重铸玩家>().绝世重铸 = true;
            }

        }
        public override int ChoosePrefix(Item item, UnifiedRandom rand)
        {
            Player player = Main.player[Main.myPlayer];
            if (player.GetModPlayer<重铸玩家>().闪耀重铸 && item.accessory == true && (!player.GetModPlayer<重铸玩家>().绝世重铸))
            {
                return PrefixType<闪耀>();
            }
            if (player.GetModPlayer<重铸玩家>().绝世重铸 && item.accessory == true)
            {
                return PrefixType<绝世>();
            }
            return base.ChoosePrefix(item, rand);
        }
        private void Reforge(Item item, int typ)
        {
            Player player = Main.player[Main.myPlayer];
            if (player.HasItem(typ) && item.accessory == true)
            {
                Item[] inventory = player.inventory;
                for (int k = 0; k < inventory.Length; k++)
                {
                    if (inventory[k].type == typ)
                    {
                        inventory[k].stack--;
                    }
                }
            }
        }
        public override void PreReforge(Item item)
        {
            Player player = Main.player[Main.myPlayer];
            if (!player.HasItem(ItemType<绝世重铸石>()))
            {
                Reforge(item, ItemType<闪耀重铸石>());
            }
            Reforge(item, ItemType<绝世重铸石>());
            base.PreReforge(item);
        }
    }
    public class 重铸玩家 : ModPlayer
    {
        public bool 闪耀重铸 = false;
        public bool 绝世重铸 = false;
        public override void ResetEffects()
        {
            绝世重铸 = false;
            闪耀重铸 = false;
        }
    }
}
