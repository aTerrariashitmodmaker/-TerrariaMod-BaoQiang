using System;

namespace 爆枪英雄.Projectiles
{
    public class 迅龙闪电 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 128;
            Projectile.height = 128;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 30;//弹幕存活时间，帧
            Projectile.tileCollide = false;//是否穿墙，ture为不穿墙
            Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
            Projectile.localNPCHitCooldown = 30;//独立无敌帧，每x帧造成一次伤害
            Projectile.extraUpdates = 0;//额外刷新，1是每帧刷新2次代码
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.scale = 1f;
            Projectile.light = 0.6f;
        }
        private void Gif(int allframe, int perf)
        {
            Projectile.frameCounter++;
            double a = Projectile.frameCounter % (allframe * perf) / perf;
            Projectile.frame = (int)Math.Floor(a);
        }//在AI（）使用gif( Main.projFrames[Projectile.type],5);
        public override void AI()
        {
            Gif(Main.projFrames[Projectile.type], 5);

        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.ArmorPenetration += 1f;
            base.ModifyHitNPC(target, ref modifiers);
        }


    }
}
