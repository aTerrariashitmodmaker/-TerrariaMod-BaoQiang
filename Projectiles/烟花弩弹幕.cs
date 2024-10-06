using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using Terraria;
using 爆枪英雄.常用类和结构体;

namespace 爆枪英雄.Projectiles
{
    public class 烟花弩弹幕 : ModProjectile
    {
        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;//这一项赋值2可以记录运动轨迹和方向（用于制作拖尾）
            ProjectileID.Sets.TrailCacheLength[Type] = 21;
        }//这一项代表记录的轨迹最多能追溯到多少帧以前(注意最大值取不到) 
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.penetrate = 1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 200;//弹幕存活时间，帧

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;

            Projectile.extraUpdates = 0;
            Projectile.tileCollide = false;//是否穿墙，ture为不穿墙
            Projectile.ignoreWater = true;//无视液体 
            Projectile.light = 0.8f;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float disX = Math.Abs(Projectile.Center.X - targetHitbox.Center.X);
            float disY = Math.Abs(Projectile.Center.Y - targetHitbox.Center.Y);
            if (disX <= 30f && disY <= 30f) //disX <= 30f && disY <= 1080f
            {
                return true;
            }

            return false;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
        }
        void drawW()
        {
            List<Vertex> vertices1 = new List<Vertex>();
            int c = 21;
            for (int i = 0; i <= c - 1; i++)
            {
                if (Projectile.oldPos[i] != Vector2.Zero)
                {
                    Vector2 start = new(32, 0);
                    Vector2 end = new(0, 0);
                    Vector2 vel = Vector2.Lerp(start, end, i * 1f / c);
                    var pos1 = Projectile.oldPos[i] - Main.screenPosition + vel.RotatedBy(Projectile.oldRot[i] - MathHelper.ToRadians(-90));
                    var pos2 = Projectile.oldPos[i] - Main.screenPosition + vel.RotatedBy(Projectile.oldRot[i] - MathHelper.ToRadians(90));
                    vertices1.Add(new Vertex(pos1, new Vector3(Easing.EaseOutQuad(i * 1f / c), 0, 1), Main.DiscoColor));// 1 - (i * 1f / c)
                    vertices1.Add(new Vertex(pos2, new Vector3(Easing.EaseOutQuad(i * 1f / c), 1, 1), Main.DiscoColor));
                }
            }
            顶点绘制函数.DrawVertices(vertices1, "爆枪英雄/Projectiles/Star");
        }
        public override bool PreDraw(ref Color lightColor)
        {
           drawW();
           drawW();
           
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
           
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                float a =- Main.rand.NextFloat(0, MathHelper.Pi);
                float speedFactor = Main.rand.NextFloat(0.83f, 1.22f);
                Vector2 vel= new Vector2((float)Math.Cos(a), (float)Math.Sin(a));
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, vel*speedFactor*6f, ProjectileType<烟花弩子弹幕>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }

            
            base.OnKill(timeLeft);
        }
    }
    public class 烟花弩子弹幕 : ModProjectile
    {
        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;//这一项赋值2可以记录运动轨迹和方向（用于制作拖尾）
            ProjectileID.Sets.TrailCacheLength[Type] = 21;
        }//这一项代表记录的轨迹最多能追溯到多少帧以前(注意最大值取不到) 
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 200;//弹幕存活时间，帧

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;

            Projectile.extraUpdates = 0;
            Projectile.tileCollide = false;//是否穿墙，ture为不穿墙
            Projectile.ignoreWater = true;//无视液体 
            Projectile.light = 0.8f;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float disX = Math.Abs(Projectile.Center.X - targetHitbox.Center.X);
            float disY = Math.Abs(Projectile.Center.Y - targetHitbox.Center.Y);
            if (disX <= 100f && disY <= 100f) //disX <= 30f && disY <= 1080f
            {
                return true;
            }

            return false;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);

            Projectile.velocity.Y += 0.15f;
            if(Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
        }
        void drawW()
        {
            List<Vertex> vertices1 = new List<Vertex>();
            int c =21;
            for (int i = 0; i <= c - 1; i++)
            {
                if (Projectile.oldPos[i] != Vector2.Zero)
                {
                    Vector2 start = new(25, 0);
                    Vector2 end = new(0, 0);
                    Vector2 vel = Vector2.Lerp(start, end, i * 1f / c);
                    var pos1 = Projectile.oldPos[i] - Main.screenPosition + vel.RotatedBy(Projectile.oldRot[i] - MathHelper.ToRadians(-90));
                    var pos2 = Projectile.oldPos[i] - Main.screenPosition + vel.RotatedBy(Projectile.oldRot[i] - MathHelper.ToRadians(90));
                    vertices1.Add(new Vertex(pos1, new Vector3(Easing.EaseOutQuad(i * 1f / c), 0, 1), Main.DiscoColor));// 1 - (i * 1f / c)
                    vertices1.Add(new Vertex(pos2, new Vector3(Easing.EaseOutQuad(i * 1f / c), 1, 1), Main.DiscoColor));
                }
            }
            顶点绘制函数.DrawVertices(vertices1, "爆枪英雄/Projectiles/Star");
        }
        public override bool PreDraw(ref Color lightColor)
        {
            drawW();
            drawW();

            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
            base.OnHitNPC(target, hit, damageDone);
        }
    }
    public class 烟花弩手持弹幕 : ModProjectile
    {
        
        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 20;//弹幕存活时间，帧

            Projectile.tileCollide = false;//是否穿墙，ture为不穿墙
            Projectile.ignoreWater = true;//无视液体 
        }
        public override bool? CanDamage()
        {
            return false;
        }
        float timer = 0;
        void SpawnDust(Player player)
        {
            int dustCount = 36;
            for (int i = 0; i < dustCount; i++)
            {
                Vector2 offset = Vector2.UnitX * Projectile.width * 0.1875f;
                offset = offset.RotatedBy((i - (dustCount / 2 - 1)) * MathHelper.TwoPi / dustCount);
                int dustIdx = Dust.NewDust(player.Center + offset, 0, 0, DustType<蓝白粒子>(), offset.X * 2f, offset.Y * 2f, 100, Color.Purple, 2f);
                Main.dust[dustIdx].noGravity = true;
                Main.dust[dustIdx].noLight = true;
                Main.dust[dustIdx].velocity = Vector2.Normalize(offset) * 4f;
            }
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 baseSpeed = (Main.MouseWorld - player.Center);
            baseSpeed.Normalize();
            if (player.channel)
            {
                timer++;
                player.direction=(Main.MouseWorld-player.Center ).X>0?1:-1;
                if (player.direction == 1)
                {
                    player.itemRotation = (Main.MouseWorld - player.Center).ToRotation();
                }
                else
                {
                    player.itemRotation = (Main.MouseWorld - player.Center).ToRotation() + MathHelper.Pi;
                }
                Projectile.timeLeft = 2;
                player.itemTime=player.itemAnimation=2;
                Projectile.Center = player.Center;

             
                if (timer == 60)
                {
                    SpawnDust(player);
                }
                if (timer == 150)
                {
                    SpawnDust(player);
                }
            }
            else
            {
                if(timer < 60)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, baseSpeed * 6f, ProjectileType<烟花弩弹幕>(), Projectile.damage , Projectile.knockBack);
                }            
                else if (timer >= 60 && timer < 150)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, baseSpeed * 12f, ProjectileType<烟花弩弹幕>(), Projectile.damage * 2, Projectile.knockBack);
                }
                else if (timer >= 150)
                {
                    float sx=( baseSpeed * 20f).X; float sy = (baseSpeed * 20f).Y;
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position,Vector2.Zero , ProjectileType<烟花弩连发弹幕>(), Projectile.damage * 4, Projectile.knockBack,default,sx,sy );
                }
            }
            
            base.AI();
        }
    }
    public class 烟花弩连发弹幕 : ModProjectile
    {
        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 24;//弹幕存活时间，帧

            Projectile.tileCollide = false;//是否穿墙，ture为不穿墙
            Projectile.ignoreWater = true;//无视液体 
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override void AI()
        {
            if(Projectile.timeLeft%8==0)
            {
                Vector2 vel = new Vector2(Projectile.ai[0], Projectile.ai[1]);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position,vel, ProjectileType<烟花弩弹幕>(), Projectile.damage , Projectile.knockBack);
            }
            base.AI();
        }
    }
    public class 烟花弩主动弹幕 : ModProjectile
    {
        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 720;//弹幕存活时间，帧

            Projectile.tileCollide = false;//是否穿墙，ture为不穿墙
            Projectile.ignoreWater = true;//无视液体 
        }
        public override bool? CanDamage()
        {
            return false;
        }
        float timer = 0;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center= player.Center;
            timer++;
            if(timer%5==0)
            {
                float x=player .Center.X+Main.rand.NextFloat (-700,700);
                float y=player.Center.Y-Main.rand.NextFloat(600,700);
                Vector2 pos=new Vector2(x,y);
                Vector2 vel = new Vector2(0, 1);
                float speedFactor = Main.rand.NextFloat(0.87f, 1.13f);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(),pos , vel * 20f * speedFactor, ProjectileType<烟花弩弹幕>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }
    }
}
