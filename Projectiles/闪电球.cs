using Microsoft.Xna.Framework.Graphics;
using System;

namespace 爆枪英雄.Projectiles
{
    public class 闪电球 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Main.projFrames[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 200;//弹幕存活时间，帧
            Projectile.tileCollide = false;//是否穿墙，ture为不穿墙			
            Projectile.DamageType = DamageClass.Generic;
            Projectile.scale = 1f;
            Projectile.light = 0.5141919810f;

        }
        private void Gif(int allframe, int perf)
        {
            Projectile.frameCounter++;
            double a = Projectile.frameCounter % (allframe * perf) / perf;
            Projectile.frame = (int)Math.Floor(a);
        }//在AI（）使用gif( Main.projFrames[Projectile.type],5);
        public override void AI()
        {
            //
            float spinTheta = 0.11f;
            if (Projectile.localAI[0] == 0f)
                Projectile.localAI[0] = Main.rand.NextBool() ? -spinTheta : spinTheta;
            Projectile.rotation += Projectile.localAI[0];
            //
            if (Projectile.timeLeft < 120) Projectile.velocity *= 0.95f;
            //
            --Projectile.ai[0];
            if (Projectile.ai[0] < 0f)
                闪电球目标扫描.MagnetSphereHitscan(Projectile, 600f, 8f, 10, 2, ProjectileType<闪电矢>(), 1.145141919810D, true);

            Gif(Main.projFrames[Projectile.type], 6);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            if (Projectile.timeLeft < 30)
            {
                float num7 = Projectile.timeLeft / 30f;
                Projectile.alpha = (int)(255f - 255f * num7);
            }
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 0);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Request<Texture2D>(Texture).Value;
            int num214 = Request<Texture2D>(Texture).Value.Height / Main.projFrames[Projectile.type];
            int y6 = num214 * Projectile.frame;
            Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, texture2D13.Width, num214)), Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2(texture2D13.Width / 2f, num214 / 2f), Projectile.scale, SpriteEffects.None, 0);
            return false;
        }

        public override bool? CanDamage() => false;

    }


}
