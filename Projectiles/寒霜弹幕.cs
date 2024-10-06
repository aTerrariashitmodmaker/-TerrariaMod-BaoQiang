namespace 爆枪英雄.Projectiles
{
    public class 寒霜弹幕 : ModProjectile
    {

        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
        public override void SetDefaults()
        {
            Projectile.tileCollide = true;//是否穿墙，ture为不穿墙
            Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
            Projectile.localNPCHitCooldown = 50;//独立无敌帧，每x帧造成一次伤害

            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.MagicSummonHybrid;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 3;
            Projectile.timeLeft = 100;

        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 53f / 255f, 195f / 255f, 234f / 255f);
            if (Projectile.timeLeft > 100)
            {
                Projectile.timeLeft = 100;
            }
            if (Projectile.ai[0] > 7f)
            {
                float num296 = 1f;
                if (Projectile.ai[0] == 8f)
                {
                    num296 = 0.25f;
                }
                else if (Projectile.ai[0] == 9f)
                {
                    num296 = 0.5f;
                }
                else if (Projectile.ai[0] == 10f)
                {
                    num296 = 0.75f;
                }
                Projectile.ai[0] += 1f;

                for (int num298 = 0; num298 < 3; num298++)
                {
                    int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.FrostStaff, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1.05f);
                    Dust dust = Main.dust[num299];
                    if (Main.rand.NextBool(3))
                    {
                        dust.noGravity = true;
                        dust.scale *= 2f;
                        dust.velocity.X *= 2f;
                        dust.velocity.Y *= 2f;
                    }
                    else
                    {
                        dust.scale *= 0.75f;
                    }
                    dust.velocity.X *= 1.2f;
                    dust.velocity.Y *= 1.2f;
                    dust.scale *= num296;
                    dust.velocity += Projectile.velocity;
                    if (!dust.noGravity)
                    {
                        dust.velocity *= 0.5f;
                    }
                }
            }
            else
            {
                Projectile.ai[0] += 1f;
            }
            Projectile.rotation += 0.3f * Projectile.direction;

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 4 * 60);
        }

    }
}
