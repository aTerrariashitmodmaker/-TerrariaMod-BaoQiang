using Terraria.DataStructures;


namespace 爆枪英雄.Projectiles
{
    public class 溅射 : ModProjectile
    {

        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 1;//弹幕存活时间，帧
            Projectile.tileCollide = true;//是否穿墙，ture为不穿墙
            Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
            Projectile.localNPCHitCooldown = 1;//独立无敌帧，每x帧造成一次伤害
            Projectile.extraUpdates = 0;//额外刷新，1是每帧刷新2次代码
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.scale = 1f;
            Projectile.alpha = 0;


            // Projectile.aiStyle = 1;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Center().Distance(Projectile.Center) <= 200)
            {
                return true;
            }
            return null;
        }
        public override void OnSpawn(IEntitySource source)
        {
            for (int num570 = 0; num570 < 6; num570++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Fireworks, 0f, 0f, 100, default, 1.5f);
            }
            for (int num571 = 0; num571 < 2; num571++)
            {
                int num573 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Firework_Yellow, 0f, 0f, 100, default, 2.5f);
                Main.dust[num573].noGravity = true;
                Dust dust22 = Main.dust[num573];
                dust22.velocity *= 3f;
                num573 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Firework_Red, 0f, 0f, 100, default, 1.5f);
                dust22 = Main.dust[num573];
                dust22.velocity *= 2f;
            }
            base.OnSpawn(source);
        }

    }
}
