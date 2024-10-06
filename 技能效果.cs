using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader.IO;
using 爆枪英雄.Items.武器;
using 爆枪英雄.NPCs;

namespace 爆枪英雄
{
    public class 技能效果 : ModPlayer
    {
        public bool 冷却标记=false;
        public int 远视 = 0;
        public int 近视 = 0;
        public int 隐身 = 0;
        public bool 神级技能 = false;
        public bool 爆石 = false;
        public bool 七步毒 = false;
        public bool 快感 = false;
        public int 派生导弹 = 0;
        public int 嗜爪 = 0;
        public int 狂暴 = 0;
        public int 金刚钻 = 0;
        public int 电离折射 = 0;
        public int 欺凌 = 0;
        public bool 镭射穿梭器 = false;
        public int 反击 = 0;
        public int 万弹归宗 = 0;
        public int 全域光波 = 0;
        public int 先锋盾 = 0;
        public bool 先锋 = false;
        public int MaxHitByDmg = 99999;
        public bool C形枪管 = false;
        public bool InVehicle = false;
        public int SilverFoxHit = 0;
        public int BatCount = 0;
        public int hitTime=0;
        //
        public float 技能急速 = 0f;
        //

        public override void ResetEffects()
        {
            冷却标记 = false;
            远视 = 0;
            近视 = 0;
            隐身 = 0;
            神级技能 = false;
            爆石 = false;
            七步毒 = false;
            快感 = false;
            派生导弹 = 0;
            嗜爪 = 0;
            狂暴 = 0;
            金刚钻 = 0;
            电离折射 = 0;
            欺凌 = 0;
            镭射穿梭器 = false;
            反击 = 0;
            万弹归宗 = 0;
            全域光波 = 0;
            先锋盾 = 0;
            先锋 = false;
            MaxHitByDmg = 99999;
            C形枪管 = false;
            InVehicle = false;
            //
            技能急速 = 0f;
            //
        }
        public  int CdTime(int second)
        {
            float k = 100 / (技能急速 + 100);
            return (int)(second * 60 * k);
        }
        //闪避
        #region
        public override bool ConsumableDodge(Player.HurtInfo info)
        {
            if (Player.HasBuff(BuffType<闪避>()))
            {
                Player.SetImmuneTimeForAllTypes(150);
                Player.ClearBuff(BuffType<闪避>()); 
                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(Player.Center + speed * 16, DustID.UltraBrightTorch, speed * 5, Scale: 1.5f);
                    d.noGravity = true;
                }
                SoundEngine.PlaySound(SoundID.Shatter with { Pitch = 0.5f });
                return true;
            }
            return false;
        }
        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (Player.HasBuff(BuffType<镭射穿梭器效果>())) return true;
            if (Player.HasBuff(BuffType<电离折射效果>())) return true;
            return false;
        }
        #endregion

        //技能主体
        #region
        private void YuanShi(NPC target, ref NPC.HitModifiers modifiers)
        {
            Vector2 a = (target.position - Player.position);
            float b = a.Length();
            if (b >= 200f)
            {
                float c = a.Length() / (1101.4514f * 1.5f);
                modifiers.SourceDamage += Math.Min(c, 0.8f);
            }
        }
        private void JinShi(NPC target, ref NPC.HitModifiers modifiers)
        {
            Vector2 a = (target.position - Player.position);
            float b = a.Length();
            if (b <= 400f)
            {
                float c = 1f - a.Length() / 400f;
                modifiers.SourceDamage += Math.Min(c, 1f);
            }
        }

        private void Qiling(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (欺凌 == 1)
            {
                float a = (1f - (float)target.life / (float)target.lifeMax) / 2;
                float b = Math.Min(a, 0.25f);
                modifiers.SourceDamage += b;
                if ((float)target.life / (float)target.lifeMax <= 0.35f) modifiers.SourceDamage *= 1.5f;
            }

        }
        private void Qibudu(NPC npc, int damageDone)
        {
            if (七步毒)
            {
                Vector2 vel = npc.velocity;
                float a = vel.Length() / 5;
                int b = (int)(damageDone * (a + 1)) / 2;
                int c = Math.Max(b, 20);
                npc.buffImmune[BuffType<七步毒敌>()] = false;
                npc.AddBuff(BuffType<七步毒敌>(), 5 * 60);
                全局NPC.QibuduDmg = c;
            }
        }
        private void Baoshi()
        {
            if (爆石 && SmMthd.Percented(33))
            {
                for (int i = 0; i < 5; i++)
                {

                    float a = Main.rand.NextFloat(0, MathHelper.Pi);
                    Vector2 vel = new((float)Math.Cos(-a), (float)Math.Sin(-a));
                    Item item = Player.HeldItem;
                    Projectile.NewProjectile(Player.GetSource_FromAI(), Player.Center, vel * 14f, ProjectileType<爆石弹幕>(), item.damage * 2, default, Player.whoAmI);
                }
            }
        }
        private void BaoshiProj(Projectile proj)
        {
            if (爆石 &&SmMthd.Percented(8) && proj.type != ProjectileType<爆石弹幕>())
            {
                for (int i = 0; i < 5; i++)
                {
                    float a = Main.rand.NextFloat(0, MathHelper.Pi);
                    Vector2 vel = new((float)Math.Cos(-a), (float)Math.Sin(-a));
                    Projectile.NewProjectile(Player.GetSource_FromAI(), Player.Center, vel * 14f, ProjectileType<爆石弹幕>(), proj.damage , default, Player.whoAmI);
                }

            }
        }
        private void Kuaigan()
        {
            if (快感)
            {
                if (Main.rand.NextBool(15))
                {
                    Player.AddBuff(BuffType<快感>(), 2 * 60);
                }
            }

        }
        private void PaiSheng(Projectile proj, NPC npc, int dmg)
        {
            int a = 5;
            if (Main.hardMode)
            {
                a = 3;
            }
            if (NPC.downedMoonlord)
            {
                a = 2;
            }
            if (派生导弹 == 1 && proj.type != ProjectileType<小导弹>() && Main.rand.NextBool(a))
            {
                Vector2 vel = npc.Center - Player.Center;
                vel.Normalize();
                Projectile.NewProjectile(Player.GetSource_FromAI(), Player.Center, vel * 17f, ProjectileType<小导弹>(), (int)(dmg * 1.1f), 5, Player.whoAmI);
            }
        }
        private void PaiShengItem(NPC npc, int dmg)
        {
            int a = 5;
            if (Main.hardMode)
            {
                a = 3;
            }
            if (NPC.downedMoonlord)
            {
                a = 2;
            }
            if (派生导弹 == 1 && Main.rand.NextBool(a))
            {
                Vector2 vel = npc.Center - Player.Center;
                vel.Normalize();
                Projectile.NewProjectile(Player.GetSource_FromAI(), Player.Center, vel * 17f, ProjectileType<小导弹>(), (int)(dmg * 1.5f), 5, Player.whoAmI);
            }
        }

        private void ZhiCanMang(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (npc.HasBuff(BuffType<致残>())) modifiers.FinalDamage *= 0.5f;
            if (npc.HasBuff(BuffType<致盲>()) && Main.rand.NextBool(4))
            {
                modifiers.FinalDamage *= 0;
                modifiers.Knockback *= 0;
            }
        }
        private void QiMang(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.HasBuff(BuffType<欺盲>()))
            {
                modifiers.FinalDamage += 0.4f;
                modifiers.CritDamage += 0.35f;
            }
        }
        private void XianFengShield(ref Player.HurtModifiers modifiers)
        {
            modifiers.SetMaxDamage(MaxHitByDmg);
        }

        private void FanJiStealLife(int damageDone)
        {
            if (Player.HasBuff(BuffType<反击效果>()))
            {
                float health = damageDone * 0.08f;
                Player.Heal((int)health);
            }
        }
        void CXinOnHit(Projectile proj, NPC target, NPC.HitInfo hit)
        {
            if (C形枪管 && proj.friendly)
            {
                int dmg = (int)(hit.Damage * 0.12f + 12);
                target.SimpleStrikeNPC(dmg, default, Main.rand.NextBool(2));
            }
        }
        private void SummonBat(Projectile proj)
        {
            if (proj.type == ProjectileType<银狐弹幕>()&& BatCount < 10)
            {
                SilverFoxHit++;
                if (SilverFoxHit >= 10)
                {
                    SilverFoxHit = 0;
                    NPC.NewNPC(Player.GetSource_FromAI(), (int)Player.Center.X, (int)Player.Center.Y, NPCType<友好蝙蝠>(), 0, Player.whoAmI);

                }
            }           
        }

        private void ShuoJinHit(NPC target)
        {
            if (Player.HeldItem.type == ItemType<烁金>())
            {
                hitTime++;
                if(hitTime >= 27) 
                { 
                    hitTime = 0;
                    for (int i = 0; i < 50; i++)
                    {
                        Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                        Dust d = Dust.NewDustPerfect(target.Center + speed * 16, DustID.Firework_Red, speed * 5, Scale: 1.5f);
                        d.noGravity = true;
                    }
                    SoundEngine.PlaySound(SoundID.Shatter with { Pitch = 0.5f });

                    int a = (int)Math.Min(target.lifeMax * 0.05f, 2000)+ Player.HeldItem.damage*17 + target.defense;
                    target.SimpleStrikeNPC(a  , default, true);
                    Rectangle pos = new((int)target.position.X,
                                        (int)target.position.Y,
                                        target.width,
                                        target.height);
                    CombatText.NewText(pos, Color.MediumVioletRed, a*2, true, false);
                }
            }
        }
        #endregion

        //技能调用
        #region
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.HasBuff(BuffType<破晓一击>()))
            {
                Player.ClearBuff(BuffType<破晓一击>());
                Player.ClearBuff(BuffID.Invisibility);
            }
            Qibudu(target, damageDone);
            FanJiStealLife(damageDone);
            Kuaigan();
            ShuoJinHit(target);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (远视 == 1)
            {
                YuanShi(target, ref modifiers);
            }
            if (近视 == 1)
            {
                JinShi(target, ref modifiers);
            }
            if (Player.HasBuff(BuffType<破晓一击>()))
            {
                modifiers.SetCrit();
                modifiers.CritDamage *= 1.5f;
                modifiers.FinalDamage *= 2.5f;
            }
            if (target.HasBuff(BuffType<爆胆>()))
            {
                modifiers.FinalDamage += 0.6f;
            }
            if (Player.HasBuff(BuffType<镭射穿梭器效果>()))
            {
                modifiers.FinalDamage *= 1.4f;
            }
            if (Player.HasBuff(BuffType<嗜爪效果>()))
            {
                float a = 0.6f;
                if (Main.hardMode)
                {
                    a = 1f;
                }
                modifiers.FinalDamage += a;
            }
            if (Player.HasBuff(BuffType<金刚钻效果>()))
            {
                float a = 0.3f;
                if (Main.hardMode)
                {
                    a = 0.4f;
                }
                modifiers.FinalDamage += a;
            }
         
            Qiling(target, ref modifiers);
            QiMang(target, ref modifiers);
            base.ModifyHitNPC(target, ref modifiers);
        }
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Baoshi();
            PaiShengItem(target, damageDone);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            BaoshiProj(proj);
            PaiSheng(proj, target, damageDone);
            CXinOnHit(proj, target, hit);
            SummonBat(proj);
        }
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            HitDianli();
            base.OnHitByNPC(npc, hurtInfo);
        }
        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            HitDianli();
            base.OnHitByProjectile(proj, hurtInfo);
        }
        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            ZhiCanMang(npc, ref modifiers);
            XianFengShield(ref modifiers);
            base.ModifyHitByNPC(npc, ref modifiers);
        }
        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            XianFengShield(ref modifiers);
            base.ModifyHitByProjectile(proj, ref modifiers);
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {

            base.ModifyHitNPCWithProj(proj, target, ref modifiers);
        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (InVehicle) drawInfo.hideEntirePlayer = true;
            base.ModifyDrawInfo(ref drawInfo);
        }
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        }
        public override void ModifyDrawLayerOrdering(IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position> positions)
        {
            base.ModifyDrawLayerOrdering(positions);
        }
        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            base.HideDrawLayers(drawInfo);
        }
        #endregion

        //主动技能按键
        #region
        private void WanDan()
        {
            if (万弹归宗 == 1 && (!Player.HasBuff(BuffType<万弹归宗冷却>()) && 爆枪英雄.cykey.JustPressed))
            {
                Player.AddBuff(BuffType<万弹归宗冷却>(), CdTime(60));
                int dmg = 6;
                if (NPC.downedBoss2)
                {
                    dmg = 8;
                }
                if (NPC.downedQueenBee)
                {
                    dmg = 12;
                }
                if (NPC.downedBoss3)
                {
                    dmg = 14;
                }
                if (NPC.downedDeerclops)
                {
                    dmg = 18;
                }
                if (Main.hardMode)
                {
                    dmg = 21;
                }
                if (NPC.downedMechBossAny)
                {
                    dmg = 32;
                }
                if (NPC.downedPlantBoss)
                {
                    dmg = 38;
                }
                if (NPC.downedAncientCultist)
                {
                    dmg = 50;
                }
                if (NPC.downedMoonlord)
                {
                    dmg = 300;
                }
                Projectile.NewProjectile(Player.GetSource_FromAI(), Player.Center, Vector2.Zero, ProjectileType<万弹归宗弹幕>(), dmg + SmMthd.FindMaxDmgItem(Player).damage / 2, 6, Player.whoAmI);
            }
        }
        private void YinShen()
        {
            if (隐身 == 1 && (!Player.HasBuff(BuffType<隐身冷却>())) && 爆枪英雄.YinShenKey.JustPressed)
            {
                Player.AddBuff(BuffType<隐身冷却>(), CdTime(20));
                Player.AddBuff(BuffType<破晓一击>(), 12 * 60);
                Player.AddBuff(BuffID.Invisibility, 12 * 60);
            }
        }
        private void ShiZhua()
        {
            if (嗜爪 == 1 && (!Player.HasBuff(BuffType<嗜爪冷却>())) && 爆枪英雄.shizhua.JustPressed)
            {
                Player.AddBuff(BuffType<嗜爪冷却>(), CdTime(45));
                Player.AddBuff(BuffType<嗜爪效果>(), 3 * 60);
            }
        }
        private void KuangBao()
        {
            if (狂暴 == 1 && (!Player.HasBuff(BuffType<狂暴冷却>())) && 爆枪英雄.kuangbao.JustPressed)
            {
                Player.AddBuff(BuffType<狂暴冷却>(), CdTime(35));
                Player.AddBuff(BuffType<狂暴效果>(), 7 * 60);
            }
        }
        private void JinGangZuan()
        {
            if (金刚钻 == 1 && (!Player.HasBuff(BuffType<金刚钻冷却>())) && 爆枪英雄.jingangzuan.JustPressed)
            {
                Player.AddBuff(BuffType<金刚钻冷却>(), CdTime(35));
                Player.AddBuff(BuffType<金刚钻效果>(), 10 * 60);
            }
        }
        private void Dianlizheshe()
        {
            if (电离折射 == 1 && (!Player.HasBuff(BuffType<电离折射冷却>())) && 爆枪英雄.dianlizheshe.JustPressed)
            {
                Player.AddBuff(BuffType<电离折射冷却>(), CdTime(60));
                Player.AddBuff(BuffType<电离折射效果>(), 5 * 60);
                int dmg = (int)((10 + Player.statDefense * 2) * 1 + (Player.endurance * 2));
                Projectile.NewProjectile(Player.GetSource_FromAI(), Player.Center, Vector2.Zero, ProjectileType<电离折射弹幕>(), dmg, 0, Player.whoAmI);
            }
        }
        private void HitDianli()
        {
            int a = 2;
            if (Player.HasBuff(BuffType<电离折射冷却>()))
            {
                if (Main.hardMode)
                {
                    a = 3;
                }
                Player.AddBuff(BuffType<电离折射冷却>(), -60 * a);
            }
        }
        private void LeisheTp()
        {
            if (镭射穿梭器 && (!Player.HasBuff(BuffType<镭射穿梭器冷却>())) && 爆枪英雄.LeiShe.JustPressed)
            {
                bool canTel = false;
                if (Collision.CanHit(Player.Center, 16, 32, Main.MouseWorld, 16, 32)) canTel = true;
                if (canTel)
                {
                    Player.Teleport(Main.MouseWorld, 2);
                    Player.AddBuff(BuffType<镭射穿梭器效果>(), 4 * 60);
                    Player.AddBuff(BuffType<镭射穿梭器冷却>(), CdTime(60));

                }

            }
        }
        void FanJi()
        {
            if (反击 == 1 && (!Player.HasBuff(BuffType<反击冷却>())) && 爆枪英雄.FanJi.JustPressed)
            {
                Player.AddBuff(BuffType<反击效果>(), 4 * 60);
                Player.AddBuff(BuffType<反击冷却>(), CdTime(60));
            }
        }
        void QuanYuRay()
        {
            if (全域光波 == 1 && (!Player.HasBuff(BuffType<全域光波冷却>())) && 爆枪英雄.QuanYu.JustPressed)
            {
                Player.AddBuff(BuffType<全域光波冷却>(), CdTime(60));
                Vector2 vel = new Vector2(10f, 0);
                Projectile.NewProjectile(Player.GetSource_FromAI(), Player.Center, vel, ProjectileType<全域光波弹幕>(), 114, 0, Player.whoAmI);
                Projectile.NewProjectile(Player.GetSource_FromAI(), Player.Center, -vel, ProjectileType<全域光波弹幕>(), 114, 0, Player.whoAmI);
            }
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            YinShen();
            WanDan();
            ShiZhua();
            KuangBao();
            JinGangZuan();
            Dianlizheshe();
            LeisheTp();
            FanJi();
            QuanYuRay();
        }
        #endregion
       

    }


}

