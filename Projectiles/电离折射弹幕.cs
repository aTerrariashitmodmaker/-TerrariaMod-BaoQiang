using System;

namespace 爆枪英雄.Projectiles
{
    public class 电离折射弹幕 : ModProjectile
    {


        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 32;
            Projectile.height = 48;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 360;//弹幕存活时间，帧
            Projectile.tileCollide = true;//是否穿墙，ture为不穿墙
            Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
            Projectile.localNPCHitCooldown = 5;//独立无敌帧，每x帧造成一次伤害
            Projectile.extraUpdates = 0;//额外刷新，1是每帧刷新2次代码
            Projectile.DamageType = DamageClass.Generic;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.ignoreWater = true;

            // Projectile.aiStyle = 1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center;
            float a = Main.rand.Next(-314, 315);
            Vector2 vel = new((float)Math.Cos(a * 0.01), (float)Math.Sin(a * 0.01));
            int num = Dust.NewDust(player.Center, default, default, DustID.BlueFairy);
            Dust dust = Main.dust[num];
            dust.velocity = vel * 8f;
            dust.noGravity = true;
        }
    }
}
