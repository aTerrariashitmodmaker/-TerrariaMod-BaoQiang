//using System;
//using Terraria.ID;

//namespace 爆枪英雄.Projectiles
//{
//    public class AuralisBullet:ModProjectile
//    {
//        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
//        public static readonly Color blueColor = new Color(0, 77, 255);
//        public static readonly Color greenColor = new Color(0, 255, 77);
//        public static Color ColorSwap(Color firstColor, Color secondColor, float seconds)
//        {
//            double timeMult = (double)(MathHelper.TwoPi / seconds);
//            float colorMePurple = (float)((Math.Sin(timeMult * Main.GlobalTimeWrappedHourly) + 1) * 0.5f);
//            return Color.Lerp(firstColor, secondColor, colorMePurple);
//        }
//        public override void SetDefaults()
//        {
//            Projectile.width = 4;
//            Projectile.height = 4;
//            Projectile.friendly = true;
//            Projectile.DamageType = DamageClass.Ranged;
//            Projectile.penetrate = 5;
//            Projectile.alpha = 255;
//            Projectile.timeLeft = 200;
//            Projectile.extraUpdates = 10;
//            Projectile.usesLocalNPCImmunity = true;
//            Projectile.localNPCHitCooldown = 10;

//        }
//        public override void AI()
//        {
//            Projectile.ai[0] += 1f;
//            if (Projectile.ai[0] > 6f)
//            {
//                for (int d = 0; d < 5; d++)
//                {
//                    Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Vortex, Projectile.velocity.X, Projectile.velocity.Y, 100, ColorSwap(blueColor, greenColor, 1f), 1f)];
//                    dust.velocity = Vector2.Zero;
//                    dust.position -= Projectile.velocity / 5f * d;
//                    dust.noGravity = true;
//                    dust.scale = 0.65f;
//                    dust.noLight = true;
//                    dust.color = ColorSwap(blueColor, greenColor, 1f);
//                }
//            }
//        }
//    }
//}
