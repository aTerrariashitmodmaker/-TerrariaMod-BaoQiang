using System;

namespace 爆枪英雄.Projectiles
{
    public class 闪电塔哨兵 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 100;
            Projectile.height = 168;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.sentry = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.light = 0.8f;

        }
        private void Gif(int allframe, int perf)
        {
            Projectile.frameCounter++;
            double a = Projectile.frameCounter % (allframe * perf) / perf;
            Projectile.frame = (int)Math.Floor(a);
        }//在AI（）使用gif( Main.projFrames[Projectile.type],5);
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Gif(Main.projFrames[Projectile.type], 6);
            if (闪电球目标扫描.FindEnemy(Projectile, 1500f, 60f))
            {

                for (int i = 0; i < 4; i++)
                {
                    double velRota = Main.rand.NextFloat(-0.18f, 0.18f);
                    float velScale = 1 + Main.rand.NextFloat(-0.142857f, 0.142857f);
                    float a = (float)Math.Cos(Math.PI * i / 2);
                    float b = (float)Math.Sin(Math.PI * i / 2);
                    Vector2 vel = new Vector2(a, b);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, (vel * 4f * velScale).RotatedBy(velRota), ProjectileType<闪电球>(), Projectile.damage, 0, player.whoAmI, 10f);
                }
                //int[] aryOn = 闪电球目标扫描.enemyArray(Projectile, 1500f, 3);
                //foreach (var targIndex in aryOn)
                //{
                //    NPC nPC = Main.npc[targIndex];
                //    Vector2 v2 = nPC.Center - Projectile.Center;
                //    v2.Normalize();
                //    float velScale = 1 + Main.rand.NextFloat(-0.142857f, 0.142857f);
                //    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, (v2 * 6f * velScale), ProjectileType<闪电球>(), Projectile.damage, 0, player.whoAmI, 10f);
                //}
            }
        }
        public override bool? CanDamage() => false;


    }


}
