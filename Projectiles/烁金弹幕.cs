using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using 爆枪英雄.常用类和结构体;

namespace 爆枪英雄.Projectiles
{
    public class 烁金弹幕 : ModProjectile
    {
        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;//这一项赋值2可以记录运动轨迹和方向（用于制作拖尾）
            ProjectileID.Sets.TrailCacheLength[Type] = 9;
        }//这一项代表记录的轨迹最多能追溯到多少帧以前(注意最大值取不到) 
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 120;//弹幕存活时间，帧

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;

            Projectile.extraUpdates = 1;
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
        }
        public override bool PreDraw(ref Color lightColor)
        {
            List<Vertex> vertices = new List<Vertex>();
            int c = 9;
            for (int i = 0; i <= c - 1; i++)
            {
                if (Projectile.oldPos[i] != Vector2.Zero)
                {
                    Color color = Color.Lerp(Color.Red, Color.Yellow, i * 1f / c);
                    Vector2 vel = new Vector2(c - i, 0);
                    var pos1 = Projectile.oldPos[i] - Main.screenPosition + 2.3f*vel.RotatedBy(Projectile.oldRot[i] - MathHelper.ToRadians(-90));
                    var pos2 = Projectile.oldPos[i] - Main.screenPosition + 2.3f*vel.RotatedBy(Projectile.oldRot[i] - MathHelper.ToRadians(90));
                    vertices.Add(new Vertex(pos1, new Vector3(i * 1f / c, 0, 1 - (i * 1f / c)), color));
                    vertices.Add(new Vertex(pos2, new Vector3(i * 1f / c, 1, 1 - (i * 1f / c)), color));
                }
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None,
                RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            Main.graphics.GraphicsDevice.Textures[0] = Request<Texture2D>("爆枪英雄/Projectiles/烁金").Value;
            if (vertices.Count > 3)
            {
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertices.ToArray(), 0, vertices.Count - 2);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 5 * 60);
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}
