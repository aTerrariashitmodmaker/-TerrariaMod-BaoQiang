using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.GameContent;
using 爆枪英雄.常用类和结构体;


namespace 爆枪英雄.Projectiles
{
    public class 沙爆弹幕 : ModProjectile
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
            Projectile.height = 8;
            Projectile.penetrate = 1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 120;//弹幕存活时间，帧
            Projectile.tileCollide = true;//是否穿墙，ture为不穿墙
            Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
            Projectile.localNPCHitCooldown = 4;//独立无敌帧，每x帧造成一次伤害
            Projectile.extraUpdates = 0;//额外刷新，1是每帧刷新2次代码
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.ignoreWater = true;
            Projectile.light = 0.8f;
            Projectile.ArmorPenetration = 40;
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
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Center().Distance(Projectile.Center) <= 80f)
            {
                Projectile.Kill();
                return true;
            }
            return base.Colliding(projHitbox, targetHitbox);
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.1f;
            if (Projectile.velocity.Y > 14f) Projectile.velocity.Y = 14f;
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center, Vector2.Zero, ProjectileID.SolarWhipSwordExplosion, Projectile.damage, 0, Main.myPlayer);
        }
        void drawTrail()
        {
            List<Vertex> vertices = new List<Vertex>();
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; i++)
            {
                if (Projectile.oldPos[i] != Vector2.Zero)
                {
                    Color color = new(255, 176, 43);
                    var pos1 = Projectile.oldPos[i] + new Vector2(0, -16f).RotatedBy(Projectile.oldRot[i]) - Main.screenPosition;
                    var pos2 = Projectile.oldPos[i] + new Vector2(0, 16f).RotatedBy(Projectile.oldRot[i]) - Main.screenPosition;
                    vertices.Add(new Vertex(pos1, new Vector3(i / 10f, 0, 0), color));
                    vertices.Add(new Vertex(pos2, new Vector3(i / 10f, 1, 0), color));
                }
            }
            顶点绘制函数.DrawVertices(vertices, "爆枪英雄/Projectiles/Laser");
        }
        public override bool PreDraw(ref Color lightColor)
        {
            drawTrail();
            return false;
        }
    }
}
