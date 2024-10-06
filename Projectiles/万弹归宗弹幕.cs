using System;

namespace 爆枪英雄.Projectiles
{
    public class 万弹归宗弹幕 : ModProjectile
    {
        public override string Texture => "爆枪英雄/Projectiles/InvisibleProj";
        public float a = 0;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 30;//弹幕存活时间，帧
            Projectile.tileCollide = false;//是否穿墙，ture为不穿墙
            Projectile.ignoreWater = true;//无视液体 

        }

        void ShootProj()
        {
            Player player = Main.player[Projectile.owner];
            for (int i = 0; i < 12; i++)
            {
                float x = Main.rand.Next(0, (int)(Math.PI * 200) + 1);
                Vector2 vel = new((float)Math.Cos(x), (float)Math.Sin(x));
                vel.Normalize();
                Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, vel * 15f, ProjectileType<小导弹>(), Projectile.damage, 0, player.whoAmI);
            }
            foreach (var npc in Main.npc)
            {
                if (npc.townNPC && Projectile.Distance(npc.Center) <= 1800f)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        float x = Main.rand.Next(0, (int)(Math.PI * 200) + 1);
                        Vector2 vel = new((float)Math.Cos(x), (float)Math.Sin(x));
                        vel.Normalize();
                        Projectile.NewProjectile(player.GetSource_FromAI(), npc.Center, vel * 15f, ProjectileType<小导弹>(), Projectile.damage, 0, player.whoAmI);
                    }
                }
            }
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 30) ShootProj();
            if (Projectile.timeLeft == 15 && SmMthd.Percented(40)) ShootProj();
            if (Projectile.timeLeft == 0 && SmMthd.Percented(20)) ShootProj();
        }

    }
}
