using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.GameContent;
using 爆枪英雄.常用类和结构体;


namespace 爆枪英雄.Projectiles
{
    public class 迅龙弹幕 : ModProjectile
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
            Projectile.width = 120;
            Projectile.height = 2;
            Projectile.penetrate = 3;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 100;//弹幕存活时间，帧
            Projectile.tileCollide = true;//是否穿墙，ture为不穿墙
            Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
            Projectile.localNPCHitCooldown = 15;//独立无敌帧，每x帧造成一次伤害
            Projectile.extraUpdates = 5;//额外刷新，1是每帧刷新2次代码
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
        void drawTrail()
        {
            List<Vertex> vertices = new List<Vertex>();
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; i++)
            {
                if (Projectile.oldPos[i] != Vector2.Zero)
                {
                    Color color = new(255, 86, 49);
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
            Vector2 vel;
            if (Projectile.ai[0] != -1) vel = Main.npc[(int)Projectile.ai[0]].velocity;
            else vel = Main.player[Projectile.owner].velocity;
            float v = (float)Math.Sqrt(Math.Pow(vel.X, 2) + Math.Pow(vel.Y, 2));
            if (v / 10 > 0)
            {
                for (int i = 0; i < v / 10; i++)
                {
                    Random random = new();
                    float m = random.Next(-128, 129);
                    float n = random.Next(-128, 129);
                    Vector2 velo = new(m, n);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center + velo, Vector2.Zero, ProjectileType<迅龙闪电>(), Projectile.damage, 6, default);
                }
            }
        }

    }
}
