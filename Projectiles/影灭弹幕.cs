using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.GameContent;
using 爆枪英雄.常用类和结构体;


namespace 爆枪英雄.Projectiles
{
    public class 影灭弹幕 : ModProjectile
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
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 120;//弹幕存活时间，帧
            Projectile.tileCollide = true;//是否穿墙，ture为不穿墙
            Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
            Projectile.localNPCHitCooldown = 30;//独立无敌帧，每x帧造成一次伤害
            Projectile.extraUpdates = 0;//额外刷新，1是每帧刷新2次代码
            Projectile.DamageType = DamageClass.Ranged;
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
                return true;
            }
            return base.Colliding(projHitbox, targetHitbox);
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
            int TarWho = SmMthd.GetTarWho(Projectile.Center, this, 800f);
            if (TarWho != -1)
            {
                NPC npc = Main.npc[TarWho];
                Vector2 tarVel = npc.Center - Projectile.Center;
                float speed = 0.4f;
                float dis = tarVel.Length();
                // 越近越快
                if (dis < 600f)
                {
                    speed = 0.5f;
                }
                if (dis < 300f)
                {
                    speed = 0.7f;
                }
                if (dis > Math.Min(npc.Size.Length(), 80f))
                {
                    // 弹幕速度加上朝着npc的单位向量*追击速度系数*1.5f
                    Projectile.velocity += Vector2.Normalize(tarVel) * speed * 2f;
                    //如果追踪方向和速度方向夹角过大，减速(向量点乘你们自己研究，绝对值越大两向量夹角越大，范围是正负1)
                    if (Vector2.Dot(Projectile.velocity, tarVel) < 0.25f)
                    {
                        Projectile.velocity *= 0.8f;
                    }
                }
                if (Projectile.velocity.Length() > 15f)
                {
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 15f;
                }
            }

        }
        void drawTrail()
        {
            List<Vertex> vertices = new List<Vertex>();
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; i++)
            {
                if (Projectile.oldPos[i] != Vector2.Zero)
                {
                    Color color = new(82, 88, 213);
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
