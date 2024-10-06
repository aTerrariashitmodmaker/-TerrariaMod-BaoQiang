using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent.ItemDropRules;
using 爆枪英雄.Items.武器;

namespace 爆枪英雄
{
    public class 全局NPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public Vector2 v = new(-50, -40);
        public bool qibudu = false;
        public static int QibuduDmg { get; set; }
        public int FrostTime = 0;
        public int chiYanHit = 0;
        public Player play = Main.player[Main.myPlayer];
        public override void ResetEffects(NPC npc)
        {
            qibudu = false;
            if (FrostTime > 0)
            {
                FrostTime--;
            }
            base.ResetEffects(npc);
        }
        public override void AI(NPC npc)
        {

            base.AI(npc);
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (qibudu)
            {
                npc.lifeRegen -= QibuduDmg * 2;
                damage = QibuduDmg;
            }
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            /* Player player =Main.LocalPlayer;
             if (技能效果.隐身冷却 == 0&&技能效果.绘制时间>0 )
             {
                 技能效果.绘制时间--;
                 Utils.DrawBorderString(spriteBatch, "隐身冷却结束",player.position+v- Main.screenPosition, Color.Green);
             }         */
            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
        private void HitByChiyan(NPC npc, Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            Item item = player.HeldItem;
            if (item.type == ItemType<炽焰>())
            {
                chiYanHit += 1;
                if (chiYanHit >= 40)
                {
                    chiYanHit = 0;
                    int a = (int)Math.Min(npc.lifeMax * 0.05f, 2000);
                    npc.SimpleStrikeNPC(a + npc.defense + 300, default, Main.rand.NextBool(2));
                    Rectangle pos = new((int)npc.position.X,
                                        (int)npc.position.Y,
                                        npc.width,
                                        npc.height);
                    CombatText.NewText(pos, Color.MediumVioletRed, a, true, false);
                }
            }
        }
        private void Frost(NPC npc, Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.type == ProjectileType<寒霜弹幕>() && FrostTime <= 0)
            {
                npc.buffImmune[BuffType<寒霜之力>()] = false;
                npc.AddBuff(BuffType<寒霜之力>(), 90);
                FrostTime = player.GetModPlayer<技能效果>().CdTime(10);
            }
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            HitByChiyan(npc, projectile);
            Frost(npc, projectile);
            base.OnHitByProjectile(npc, projectile, hit, damageDone);
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.Hellbat)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<神雀>(), 15));
            }
            base.ModifyNPCLoot(npc, npcLoot);
        }
        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            base.ModifyActiveShop(npc, shopName, items);
        }
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Merchant)
            {
                Item shopItem = new(ItemType<白刘海头饰>())
                {
                    // 设置特别的售卖价格，这个默认是null
                    // 但如果这个属性被设置了就会使用这个特别设置的价格来售卖
                    // 不设置即使用物品本身的价值
                    shopCustomPrice = Item.buyPrice(0, 10)
                };
                shop.Add(new NPCShop.Entry(shopItem));
            }
            base.ModifyShop(shop);
        }
    }
}
