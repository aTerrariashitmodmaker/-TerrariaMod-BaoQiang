namespace 爆枪英雄.Projectiles
{
    public class 闪电矢 : ModProjectile
    {

        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.penetrate = 1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 90;//弹幕存活时间，帧
            Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
            Projectile.localNPCHitCooldown = 10;//独立无敌帧，每x帧造成一次伤害
            Projectile.extraUpdates = 100;//额外刷新，1是每帧刷新2次代码
            Projectile.DamageType = DamageClass.Summon;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.light = 0.45141919810f;


            // Projectile.aiStyle = 1;
        }

        public override void AI()
        {
            Vector2 vector33 = Projectile.position;
            vector33 -= Projectile.velocity * 0.25f;
            int num448 = Dust.NewDust(vector33, 1, 1, DustID.UnusedWhiteBluePurple, 0f, 0f, 0, default, 1.25f);
            Main.dust[num448].position = vector33;
            Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.013f;
            Main.dust[num448].velocity *= 0.1f;
            base.AI();
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {

            if (NPC.downedMoonlord)
            {
                int a = SmMthd.FindMaxDmgItem(Main.player[Projectile.owner]).damage;
                modifiers.FlatBonusDamage += a;
            }
            if (target.boss)
            {
                modifiers.FinalDamage *= 0.2f;
            }
            modifiers.ScalingArmorPenetration += 1f;
            base.ModifyHitNPC(target, ref modifiers);
        }

    }
}
