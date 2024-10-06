namespace 爆枪英雄.Projectiles
{
    public class 蝙蝠接触伤害 : ModProjectile
    {
        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = Projectile.height = 3;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 25;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 25;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.light = 0.8f;
            Projectile.ArmorPenetration = 100;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Center().Distance(Projectile.Center) <= 40f)
            {
                return true;
            }
            return base.Colliding(projHitbox, targetHitbox);
        }
        public override void AI()
        {
            NPC owner = Main.npc[(int)Projectile.ai[0]];
            Projectile.Center = owner.Center;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            NPC owner = Main.npc[(int)Projectile.ai[0]];
            int healLife = damageDone * 5;

            if (owner.life + healLife > owner.lifeMax)
            {
                healLife = owner.lifeMax - owner.life;
            }
            if (healLife > 0)
            {
                owner.life += healLife;
                owner.HealEffect(healLife);
            }

            base.OnHitNPC(target, hit, damageDone);
        }
    }
}
