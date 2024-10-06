namespace 爆枪英雄.Projectiles
{
    public class 赤鼬导弹发射器 : ModProjectile
    {
        public float a = 0;
        public float singleTime = 0;
        public float attackCD = 0;
        public int fireCount = 0;

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 80 * 2;
            Projectile.height = 80 * 2;
            Projectile.penetrate = -1;//可命中敌人数，-1为无限穿透
            Projectile.timeLeft = 60;//弹幕存活时间，帧
            Projectile.tileCollide = false;//是否穿墙，ture为不穿墙
            Projectile.scale = 0.5f;
            Projectile.ignoreWater = true;//无视液体 
        }

        public override bool? CanDamage()//注意！因为这是一个只用来发射子弹的武器弹幕，不能造成伤害
        {
            return false;//因此需要返回false
        }

        void ShootProj2()
        {
            if (singleTime <= 60)
            {
                if ((attackCD == 0 || attackCD >= Projectile.ai[0]) && fireCount < Projectile.ai[1])
                {
                    Vector2 vel = Main.MouseWorld - Projectile.Center;
                    vel.Normalize();
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, vel * 12.5f, ProjectileType<赤鼬导弹>(), Projectile.damage, 0);
                    attackCD = 0;
                    fireCount++;
                }
                attackCD++;
            }
            else
            {
                singleTime = 0;
                attackCD = 0;
                fireCount = 0;
            }
            singleTime++;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            int dir = 1;
            if (player.direction == 1)//如果玩家朝着右边
            {
                Projectile.direction = Projectile.spriteDirection = 1;
                dir = -2;
            }
            else
            {
                Projectile.direction = Projectile.spriteDirection = -1;
            }
            Projectile.position = new Vector2(player.Center.X + 50f * dir, player.Center.Y - 100f);

            ShootProj2();

        }
    }
}
