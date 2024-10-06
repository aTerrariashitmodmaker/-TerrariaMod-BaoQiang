using System;
using Terraria.Audio;
using 爆枪英雄.声音;

namespace 爆枪英雄.Projectiles
{
    public class 卡特手持弹幕 : ModProjectile
    {
        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";


        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 24;//弹幕存活时间，帧
            Projectile.tileCollide = false;//是否穿墙，ture为不穿墙
            Projectile.ignoreWater = true;//无视液体 

        }

        public override bool? CanDamage()//注意！因为这是一个只用来发射子弹的武器弹幕，不能造成伤害
        {
            return false;//因此需要返回false
        }
        private void ShootProj(Player player)
        {
            Item item = player.HeldItem;
            Vector2 vel = Main.MouseWorld - player.Center;
            vel.Normalize();
            vel *= 20;
            Vector2 a = vel.RotatedBy(-36.9 / 180 * Math.PI * 0.8f);
            for (int i = 0; i < 10; i++)
            {
                Vector2 b = a.RotatedBy(8.2 / 180 * i * Math.PI * 0.8f);
                Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, b, ProjectileType<卡特巨炮炮弹>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
            }
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.position = player.Center;
            if (Projectile.timeLeft == 16)
            {
                ShootProj(player);
                SoundEngine.PlaySound(声音路径.卡特巨炮声音, player.Center);
            }
            if (Projectile.timeLeft == 8 && Main.rand.NextBool(2))
            {
                ShootProj(player);
                SoundEngine.PlaySound(声音路径.卡特巨炮声音, player.Center);
            }

        }

    }
}
