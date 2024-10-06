using System;
using System.Net;
using Terraria;

namespace 爆枪英雄.NPCs
{
    public class 友好蝙蝠 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 20;
            NPC.damage = 25;
            NPC.defense = 100;
            NPC.lifeMax = 800;
            NPC.friendly = true;
            //NPC.dontCountMe = true;
            //NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.DeathSound = SoundID.NPCDeath11;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            //NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            NPC.npcSlots = 0f;
        }

        float c = 0;
        private void MoveAroundPlayer(Player player)
        {
            Vector2 diff = NPC.Center - player.Center;
            float dis = diff.Length();
            diff.Normalize();
            //diff = diff.RotatedBy(MathHelper.PiOver2);
            NPC.velocity -= diff * 0.2f;

            if (NPC.Center.X < player.Center.X)
            {
                NPC.velocity.X += 0.1f;
            }
            if (NPC.Center.X > player.Center.X)
            {
                NPC.velocity.X -= 0.1f;
            }
            if (NPC.Center.Y < player.Center.Y)
            {
                NPC.velocity.Y += 0.1f;
            }
            if (NPC.Center.Y > player.Center.Y)
            {
                NPC.velocity.Y -= 0.1f;
            }

            if (dis > 80f)
            {
                if (Vector2.Dot(NPC.velocity, -diff) < 0.25f)
                {
                    NPC.velocity *= 0.9f;
                }
            }

            if (NPC.velocity.Length() > 7.5f && dis < 200f)
            {
                NPC.velocity = Vector2.Normalize(NPC.velocity) * 7.5f;
                NPC.velocity *= 0.98f;
            }
            if (Math.Abs(NPC.velocity.X) < 0.016f || Math.Abs(NPC.velocity.Y) < 0.016f)
            {
                NPC.velocity = Main.rand.NextVector2Circular(1, 1) * 2f;
                NPC.velocity *= 1.1f;
                NPC.netUpdate = true;
            }
        }
        public override void AI()
        {
            //
            if (c % 25 == 0)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ProjectileType<蝙蝠接触伤害>(), NPC.damage, 6, default, NPC.whoAmI);
            }
            //
            c++;
            if (c % 10 == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.ShimmerTorch, 0, 0, 0, Color.HotPink)];
            }
            //
            Player player = Main.player[(int)NPC.ai[0]];
            if (Vector2.Distance(player.Center, NPC.Center) > 1500f)
            {
                NPC.Center = player.Center;
                NPC.netUpdate = true;
            }
            //
            if (player.dead)
            {
                NPC.life = -1;
            }
            //
            int TarWho = -1;// 目标whoAmI
            float misdis = 1500f;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active && Main.npc[i].CanBeChasedBy(NPC, true) && (Main.npc[i].damage > 0))
                {
                    float dis = player.Distance(Main.npc[i].Center);
                    if (dis < misdis)
                    {
                        misdis = dis;
                        TarWho = Main.npc[i].whoAmI;
                    }
                }
            }
            //if (player.MinionAttackTargetNPC >= 0) target = Main.npc[player.MinionAttackTargetNPC];
            //if (target != null && target.CanBeChasedBy())
            //{
            //    if (NPC.Distance(target.Center) < 1500f) TarWho = target.whoAmI;
            //}
            //if (TarWho < 0)
            //{
            //    foreach (NPC npc in Main.npc)
            //    {
            //        if ( (!npc.friendly)&&(npc.CanBeChasedBy()) && (player.Distance(npc.Center) < 1000f))
            //        {
            //            if (NPC.Distance(npc.Center) < 1000f)
            //            {
            //                TarWho = npc.whoAmI;
            //            }
            //        }
            //    }
            //}
            //
            if (TarWho != -1)
            {
                NPC npc = Main.npc[TarWho];
                Vector2 tarVel = npc.Center - NPC.Center;
                float speed = 0.4f;
                float dis = tarVel.Length();
                // 越近越快
                if (dis < 600f)
                {
                    speed = 0.5f;
                }
                if (dis < 300f)
                {
                    speed = 0.7f;
                }
                if (dis > Math.Min(npc.Size.Length(), 80f))
                {
                    // 弹幕速度加上朝着npc的单位向量*追击速度系数*1.5f
                    NPC.velocity += Vector2.Normalize(tarVel) * speed * 2f;
                    //如果追踪方向和速度方向夹角过大，减速(向量点乘你们自己研究，绝对值越大两向量夹角越大，范围是正负1)
                    if (Vector2.Dot(NPC.velocity, tarVel) < 0.25f)
                    {
                        NPC.velocity *= 0.8f;
                    }
                }
                if (NPC.velocity.Length() > 12f)
                {
                    NPC.velocity = Vector2.Normalize(NPC.velocity) * 12f;
                }
            }
            //无目标待机
            else
            {
                MoveAroundPlayer(player);


                //float speed = 0.2f;// 游荡速度系数
                //float dis = player.Center.Distance(NPC.Center);// 弹幕到玩家距离，越近越慢
                //if (dis < 200f)
                //{
                //    speed = 0.12f;
                //}
                //if (dis < 140f)
                //{
                //    speed = 0.06f;
                //}
                //if (dis > 100f)
                //{
                //    // abs绝对值，sign正负
                //    float toPx = player.Center.X - NPC.Center.X;
                //    float toPy = player.Center.Y - NPC.Center.Y;
                //    if (Math.Abs(toPx) > 20f)// 离玩家水平距离大于20，就给朝向玩家的水平速度
                //    {
                //        NPC.velocity.X += speed * Math.Sign(toPx);
                //    }
                //    if (Math.Abs(toPy) > 8f)// 离玩家垂直距离大于10，就给朝向玩家的垂直速度
                //    {
                //        NPC.velocity.Y += speed * Math.Sign(toPy);
                //    }
                //}
                //else if (NPC.velocity.Length() > 2f)// 在100f内且速度较大就减速
                //{
                //    NPC.velocity *= 0.9f;
                //}
                //if (Math.Abs(NPC.velocity.Y) < 1f)// 没啥速度了就慢慢向上游动
                //{
                //    NPC.velocity.Y -= 0.04f;
                //}
                //if (NPC.velocity.Length() > 8f)
                //{
                //    NPC.velocity = Vector2.Normalize(NPC.velocity) * 8f;
                //}
            }

            base.AI();
        }
        public override void FindFrame(int frameHeight)
        {
            int count = 0;

            if (c % 10 > 5)
            {
                count = 1;
            }
            if (NPC.velocity.X > 0f)
            {
                count += 2;
            }
            NPC.frame.Y = count * frameHeight;
            base.FindFrame(frameHeight);
        }
        //public override void OnKill()
        //{
        //    Player player = Main.player[(int)NPC.ai[0]];
        //    player.GetModPlayer<技能效果>().BatCount--;
        //    base.OnKill();
        //}
    }
}
