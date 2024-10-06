using System;


namespace 爆枪英雄.Projectiles
{
    public class 飞镰弹幕 : ModProjectile
    {


        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.penetrate = 5;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 200;//弹幕存活时间，帧
            Projectile.tileCollide = true;//是否穿墙，ture为不穿墙
            Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
            Projectile.localNPCHitCooldown = 15;//独立无敌帧，每x帧造成一次伤害
            Projectile.extraUpdates = 0;//额外刷新，1是每帧刷新2次代码
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.ignoreWater = false;
            Projectile.light = 0.8f;
            // Projectile.aiStyle = 1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.position.X = Projectile.position.X + Projectile.velocity.X;
                Projectile.velocity.X = 0f - oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.position.Y = Projectile.position.Y + Projectile.velocity.Y;
                Projectile.velocity.Y = 0f - oldVelocity.Y;
            }
            return false;
        }
        public override void AI()
        {

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.velocity = -Projectile.velocity;
        }

    }
}
