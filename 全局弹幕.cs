using Terraria.DataStructures;

namespace 爆枪英雄
{
    public class 全局弹幕 : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            Player player = Main.player[projectile.owner];
            if (player.GetModPlayer<技能效果>().C形枪管 && projectile.friendly)
            {
                projectile.netUpdate = true;
                Vector2 spos = player.Center - projectile.position;
                projectile.position = player.Center + spos;
                float roat = Main.rand.NextFloat(-0.08f, 0.09f);
                projectile.velocity = -projectile.velocity.RotatedBy(roat);
            }
            base.OnSpawn(projectile, source);
        }
        public override bool? Colliding(Projectile projectile, Rectangle projHitbox, Rectangle targetHitbox)
        {
           
            if (projectile.type == ProjectileType<电离折射弹幕>())
            {
                if (targetHitbox.Center().Distance(projectile.Center) <= 180)
                {
                    return true;
                }
            }
            return null;
        }
        public override void AI(Projectile projectile)
        {
            Player player = Main.LocalPlayer;
            if (player.HasBuff(BuffType<金刚钻效果>()))
            {
                if (projectile.friendly)
                {
                    projectile.tileCollide = false;
                }
            }
            if (player.HasBuff(BuffType<镭射穿梭器效果>()))
            {
                if (projectile.friendly)
                {
                    projectile.tileCollide = false;
                }
            }
        }
    }
}
