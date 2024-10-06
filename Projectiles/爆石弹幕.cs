using System;
using Terraria;

namespace 爆枪英雄.Projectiles
{
    public class 爆石弹幕 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.penetrate = 1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 150;//弹幕存活时间，帧
            Projectile.tileCollide = true;//是否穿墙，ture为不穿墙
            Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
            Projectile.localNPCHitCooldown = 20;//独立无敌帧，每x帧造成一次伤害
            Projectile.extraUpdates = 0;//额外刷新，1是每帧刷新2次代码
            Projectile.DamageType = DamageClass.Generic;
            Projectile.scale = 1f;
        }
        private void Gif(int allframe, int perf)
        {
            Projectile.frameCounter++;
            double a = Projectile.frameCounter % (allframe * perf) / perf;
            Projectile.frame = (int)Math.Floor(a);
        }//在AI（）使用gif( Main.projFrames[Projectile.type],5);
        public override void AI()
        {
            if (Projectile.velocity.Y < 16f)
            {
                Projectile.velocity.Y += 0.2f;
            }
            if (Math.Abs(Projectile.velocity.Y) > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            Projectile.velocity *= 0.993f;
            Gif(Main.projFrames[Projectile.type], 5);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {


            if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
            {
                Projectile.velocity.X = oldVelocity.X * -0.8f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.8f;
            }
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Center().Distance(Projectile.Center) <= 80)
            {
                Projectile.Kill();
                return true;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 1;
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            base.OnHitNPC(target, hit, damageDone);
        }

    }
}
