using System;
using Terraria.Audio;



namespace 爆枪英雄.Projectiles
{
    public class 炮弹 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 39;
            Projectile.height = 17;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 200;//弹幕存活时间，帧
            Projectile.tileCollide = false;//是否穿墙，ture为不穿墙
            Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
            Projectile.localNPCHitCooldown = 15;//独立无敌帧，每x帧造成一次伤害
            Projectile.extraUpdates = 1;//额外刷新，1是每帧刷新2次代码
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.light = 0.8f;
            Projectile.scale = 1f;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Center().Distance(Projectile.Center) <= 50)
            {
                Projectile.Kill();
                return true;
            }
            return null;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int num570 = 0; num570 < 7; num570++)
            {
                _ = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
            }
            for (int num571 = 0; num571 < 3; num571++)
            {
                int num573 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2.5f);
                Main.dust[num573].noGravity = true;
                Dust dust22 = Main.dust[num573];
                dust22.velocity *= 3f;
                num573 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                dust22 = Main.dust[num573];
                dust22.velocity *= 2f;
            }

        }

    }
}
