using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.GameContent;
using 爆枪英雄.常用类和结构体;


namespace 爆枪英雄.Projectiles
{
    public class 青蜂弹幕 : ModProjectile
    {
        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;//这一项赋值2可以记录运动轨迹和方向（用于制作拖尾）
            ProjectileID.Sets.TrailCacheLength[Type] = 10;
        }//这一项代表记录的轨迹最多能追溯到多少帧以前(注意最大值取不到) 

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 3;
            Projectile.height = 3;
            Projectile.penetrate = 3;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 200;//弹幕存活时间，帧
            Projectile.tileCollide = true;//是否穿墙，ture为不穿墙
            Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
            Projectile.localNPCHitCooldown = 15;//独立无敌帧，每x帧造成一次伤害
            Projectile.extraUpdates = 2;//额外刷新，1是每帧刷新2次代码
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.ignoreWater = false;
            Projectile.light = 0.8f;

            // Projectile.aiStyle = 1;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
        }
        void drawTrail()
        {
            List<Vertex> vertices = new List<Vertex>();
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; i++)
            {
                if (Projectile.oldPos[i] != Vector2.Zero)
                {
                    Color color = new(82, 88, 213);
                    var pos1 = Projectile.oldPos[i] + new Vector2(0, -1.5f).RotatedBy(Projectile.oldRot[i]) - Main.screenPosition;
                    var pos2 = Projectile.oldPos[i] + new Vector2(0, 1.5f).RotatedBy(Projectile.oldRot[i]) - Main.screenPosition;
                    vertices.Add(new Vertex(pos1, new Vector3(i / 10f, 0, 0), color));
                    vertices.Add(new Vertex(pos2, new Vector3(i / 10f, 1, 0), color));
                }
            }
            顶点绘制函数.DrawVertices(vertices, "爆枪英雄/Projectiles/LaserT");
        }
        public override bool PreDraw(ref Color lightColor)
        {
            drawTrail();
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(5))
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), target.Center, Projectile.velocity * 0.7f, ProjectileType<飞镰弹幕>(), Projectile.damage * 3, 3);
            }
        }

    }
}
