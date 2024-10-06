using System;
using Terraria.DataStructures;

namespace 爆枪英雄.Projectiles;
public class 屠夫弹幕 : ModProjectile
{

    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 9;
        ProjectileID.Sets.TrailingMode[Type] = 0;//赋值2用于记录轨迹和方向
        ProjectileID.Sets.TrailCacheLength[Type] = 5;//这一项代表记录的轨迹最多能追溯到多少帧以前(注意最大值取不到)
                                                     //ProjectileID.Sets.DrawScreenCheckFluff[Type] = 2000;//这一项代表弹幕超过屏幕外多少距离以内可以绘制
                                                     //用于长条形弹幕绘制 
        base.SetStaticDefaults();
    }

    public override void SetDefaults()
    {
        Projectile.width = 500;
        Projectile.height = 500;
        Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
        Projectile.timeLeft = 36;//弹幕存活时间，帧
        Projectile.tileCollide = false;//是否穿墙，ture为不穿墙
        Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
        Projectile.localNPCHitCooldown = 18;//独立无敌帧，每x帧造成一次伤害
        Projectile.extraUpdates = 0;//额外刷新，1是每帧刷新2次代码
        Projectile.DamageType = DamageClass.Generic;
        Projectile.scale = 1f;
        Projectile.friendly = true;
        base.SetDefaults();
    }
    private void Gif(int allframe, int perf)
    {
        Projectile.frameCounter++;
        double a = Projectile.frameCounter % (allframe * perf) / perf;
        Projectile.frame = (int)Math.Floor(a);
    }//在AI（）使用gif( Main.projFrames[Projectile.type],5);
    public override void OnSpawn(IEntitySource source)
    {
        Player player = Main.player[Projectile.owner];
        if (player.direction == 1)
        {
            Projectile.direction = Projectile.spriteDirection = -1;
        }
    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        Gif(Main.projFrames[Projectile.type], 4);
        Projectile.Center = player.position;

    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {

        base.OnHitNPC(target, hit, damageDone);
    }

}
