namespace 爆枪英雄.Projectiles
{
    public class 碰撞弹幕 : ModProjectile
    {
        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 45;//弹幕存活时间，帧
            Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
            Projectile.localNPCHitCooldown = 12;//独立无敌帧，每x帧造成一次伤害
            Projectile.extraUpdates = 0;//额外刷新，1是每帧刷新2次代码
            Projectile.DamageType = DamageClass.Summon;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            // Projectile.aiStyle = 1;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 startingPosition;
            if (targetHitbox.Center().Distance(Projectile.Center) <= 175)
            {
                if (targetHitbox.Center().Distance(Projectile.Center) <= 100)
                {
                    startingPosition = targetHitbox.Center();
                }
                else
                {
                    Vector2 n = targetHitbox.Center() - Projectile.Center;
                    n.Normalize();
                    startingPosition = Projectile.Center + n * 100f;
                }
                int dustCount = 36;
                for (int i = 0; i < dustCount; i++)
                {
                    Vector2 offset = Vector2.UnitX * Projectile.width * 0.1875f;
                    offset = offset.RotatedBy((i - (dustCount / 2 - 1)) * MathHelper.TwoPi / dustCount);
                    int dustIdx = Dust.NewDust(startingPosition + offset, 0, 0, DustType<蓝白粒子>(), offset.X * 2f, offset.Y * 2f, 100, default, 3.4f);
                    Main.dust[dustIdx].noGravity = true;
                    Main.dust[dustIdx].noLight = true;
                    Main.dust[dustIdx].velocity = Vector2.Normalize(offset) * 4f;
                }
                return true;
            }
            return null;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center;
            base.AI();
        }
    }
}
