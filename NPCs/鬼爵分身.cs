using System;

namespace 爆枪英雄.NPCs
{
    public class 鬼爵分身 : ModNPC
    {

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            //NPC总共帧图数，一般为16+下面两种帧的帧数            
        }
        public override void SetDefaults()
        {
            NPC.friendly = true;
            NPC.width = 36;
            NPC.height = 63;
            NPC.aiStyle = -1;
            NPC.damage = 76;
            NPC.defense = 100;
            NPC.lifeMax = 5000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
        }
        int attackWays = 0;
        public override void AI()
        {
            NPC.ai[1]++;
            Player Owner = Main.player[(int)NPC.ai[0]];
            Owner.AddBuff(BuffType<分身>(), 60);
            if (Owner.dead )
            {
                NPC.life = -1;
            }
            //
            if (!NPC.WithinRange(Owner.Center, 1000f))
            {
                NPC.position = Owner.Center;
                NPC.velocity *= 0.3f;
                NPC.netUpdate = true;
            }
            //
            int TarWho = SmMthd.GetTarWho(NPC.Center, this, 800f);
            if (TarWho != -1)
            {
                NPC target = Main.npc[TarWho];
                Vector2 dis = target.Center - NPC.Center;
                float disY = Math.Abs(dis.Y);
                float disX = Math.Abs(dis.X);
                //
                if (disY > 130f + target.Size.Length() / 2)
                {
                    NPC.velocity.Y += dis.Y / 20f;
                }
                else
                {
                    NPC.velocity.Y -= 0.5f;
                    if (NPC.velocity.Length() > 1f)
                    {
                        NPC.velocity *= 0.9f;
                    }
                }

                if (disX > 150f + target.Size.Length() / 2)
                {
                    NPC.velocity.X += dis.X / 25f;
                }

                if (NPC.velocity.Length() > 8f)
                {
                    NPC.velocity = Vector2.Normalize(NPC.velocity) * 8f;
                }
                //
                if (NPC.ai[1] % 300 == 0)
                {
                    attackWays++;
                    if (attackWays > 2)
                    {
                        attackWays = 0;
                    }
                }

                if (attackWays == 0) JianChiShoot(dis);
                else if (attackWays == 1) XunLongShoot(dis);
                else if (attackWays == 2) ShaEShoot(dis);

            }
            else
            {
                if (NPC.WithinRange(Owner.Center, 1000f) && !NPC.WithinRange(NPC.Center, 300f))
                {
                    NPC.velocity = (Owner.Center - NPC.Center) / 10f;
                }
                else if (!NPC.WithinRange(Owner.Center, 160f))
                {
                    Vector2 vel = Owner.Center - NPC.Center;
                    vel.Normalize();
                    NPC.velocity = (NPC.velocity * 37f + vel * 17f) / 40f;
                }
                if ((NPC.Center.Y - Owner.Center.Y > 100f && NPC.ai[1] % 60 == 0))
                {
                    NPC.velocity.Y -= 10f;
                }
            }
            //
            base.AI();
        }
        public override void OnKill()
        {
            Player player = Main.player[(int)NPC.ai[0]];
            player.GetModPlayer<某些装备效果>().分身冷却 = 120 * 60f;
            base.OnKill();
        }
        public override void FindFrame(int frameHeight)
        {
            int count = 0;

            if (NPC.ai[1] % 10 > 5)
            {
                count = 1;
            }
            if (NPC.velocity.X < 0f)
            {
                count += 2;
            }
            NPC.frame.Y = count * frameHeight;
            base.FindFrame(frameHeight);
        }
        private void ShaEShoot(Vector2 dis)
        {
            if (NPC.ai[1] % 30 == 0 && dis.Length() <= 700f)
            {
                for (int i = 0; i < 11; i++)
                {
                    float randAngle = Main.rand.NextFloat(-0.32f, 0.33f);
                    float randVelMultiplier = Main.rand.NextFloat(0.85f, 1.15f);
                    Vector2 velocity = Vector2.Normalize(dis);
                    Vector2 cwVelocity = velocity.RotatedBy(randAngle);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, 18f * cwVelocity * randVelMultiplier, ProjectileType<沙鳄弹幕>(), NPC.damage, 3, -1, NPC.whoAmI);
                }
            }
        }
        private void XunLongShoot(Vector2 dis)
        {
            if (NPC.ai[1] % 7 == 0 && dis.Length() <= 850f)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, 18f * Vector2.Normalize(dis), ProjectileType<迅龙弹幕>(), NPC.damage, 3, -1, NPC.whoAmI);
            }
        }
        private void JianChiShoot(Vector2 dis)
        {
            if (NPC.ai[1] % 55 == 0 && dis.Length() <= 1000f)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, 18f * Vector2.Normalize(dis), ProjectileType<剑齿弹幕>(), NPC.damage, 3, -1, NPC.whoAmI);
                for (int i = 0; i < 2; i++)
                {
                    float randAngle = Main.rand.NextFloat(-0.35f, 0.34f);
                    float randVelMultiplier = Main.rand.NextFloat(0.7f, 1.3f);
                    Vector2 velocity = Vector2.Normalize(dis);
                    Vector2 cwVelocity = velocity.RotatedBy(randAngle);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, 18f * cwVelocity * randVelMultiplier, ProjectileType<影灭弹幕>(), NPC.damage * 2, 3, -1, NPC.whoAmI);
                }
            }
        }
    }
}
