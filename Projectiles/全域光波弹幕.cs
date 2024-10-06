using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using 爆枪英雄.常用类和结构体;

namespace 爆枪英雄.Projectiles
{
    public class 全域光波弹幕 : ModProjectile
    {
        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 240;//弹幕存活时间，帧

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 240;

            Projectile.tileCollide = false;//是否穿墙，ture为不穿墙
            Projectile.ignoreWater = true;//无视液体 

        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float disX = Math.Abs(Projectile.Center.X - targetHitbox.Center.X);
            float disY = Math.Abs(Projectile.Center.Y - targetHitbox.Center.Y);
            if (disX <= 100f && disY <= 1080f) //disX <= 30f && disY <= 1080f
            {
                return true;
            }

            return false;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.ArmorPenetration += 1f;
            modifiers.SetCrit();
            if (target.boss)
            {
                modifiers.FlatBonusDamage += target.lifeMax * 0.025f;
            }
            else
            {
                if (target.npcSlots > 1.85f)
                {
                    modifiers.FlatBonusDamage += target.lifeMax * 0.2f;
                }
                else
                {
                    modifiers.SetInstantKill();
                }
            }
            base.ModifyHitNPC(target, ref modifiers);
        }
        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            Projectile.position.Y = player.position.Y;
            foreach (Projectile proj in Main.projectile)
            {
                if (!proj.friendly)
                {
                    float disX = Math.Abs(Projectile.Center.X - proj.Center.X);
                    float disY = Math.Abs(Projectile.Center.Y - proj.Center.Y);
                    if (disX <= 300f && disY <= 1080f)
                    {
                        proj.Kill();
                    }
                }
            }

        }
        public override bool PreDraw(ref Color lightColor)
        {
            List<Vertex> vertices = new List<Vertex>();

            for (int i = -30; i <= 30; i++)
            {
                Color color = Color.Lerp(Color.Blue, Color.AliceBlue, i / 60f + 0.5f);
                var pos1 = new Vector2(Projectile.Center.X - 20f, Projectile.Center.Y) - Main.screenPosition + new Vector2(5 * (float)Math.Sin(i * 0.8f), 15 * i);
                var pos2 = new Vector2(Projectile.Center.X + 20f, Projectile.Center.Y ) - Main.screenPosition + new Vector2(5 * (float)Math.Sin(i * 0.8f), 15 * i);
                vertices.Add(new Vertex(pos1, new Vector3(i / 60f + 0.5f, 0, 1), color));
                vertices.Add(new Vertex(pos2, new Vector3(i / 60f + 0.5f, 1, 1), color));
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None,
                RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            Main.graphics.GraphicsDevice.Textures[0] = Request<Texture2D>("爆枪英雄/Projectiles/Lazer").Value;
            if (vertices.Count > 3)
            {
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertices.ToArray(), 0, vertices.Count - 2);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return base.PreDraw(ref lightColor);
        }

    }
}
