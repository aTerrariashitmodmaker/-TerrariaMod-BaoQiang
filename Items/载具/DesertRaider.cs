namespace 爆枪英雄.Items.载具
{
    public class DesertRaider : ModMount
    {
        public override void SetStaticDefaults()
        {
            MountData.spawnDust = DustID.Smoke;
            MountData.spawnDustNoGravity = true;
            MountData.buff = BuffType<沙漠进袭者霸符>();
            MountData.heightBoost = 20; //8
            MountData.fallDamage = 0.3f;
            MountData.runSpeed = 7.5f;
            MountData.flightTimeMax = 0;
            MountData.jumpHeight = 18;
            MountData.acceleration = 0.21f;
            MountData.jumpSpeed = 7f;
            MountData.swimSpeed = 3.5f;
            MountData.totalFrames = 1;
            //关键
            int[] array = new int[MountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 0;
            }
            MountData.playerYOffsets = array;
            //
            MountData.xOffset = -6;
            MountData.bodyFrame = 0;
            MountData.yOffset = 8; //done
            MountData.playerHeadOffset = 0;

            MountData.standingFrameCount = 0;
            MountData.standingFrameDelay = 12;
            MountData.standingFrameStart = 0;

            MountData.runningFrameCount = MountData.standingFrameCount;
            MountData.runningFrameDelay = 36; //36
            MountData.runningFrameStart = MountData.standingFrameCount;

            //MountData.inAirFrameCount = 1;
            //MountData.inAirFrameDelay = MountData.standingFrameDelay;
            //MountData.inAirFrameStart = MountData.standingFrameDelay;

            //MountData.idleFrameCount = MountData.standingFrameCount; //done
            //MountData.idleFrameDelay = MountData.standingFrameDelay; //done
            //MountData.idleFrameStart = MountData.standingFrameStart;
            //MountData.idleFrameLoop = true;

            MountData.swimFrameCount = MountData.inAirFrameCount;
            MountData.swimFrameDelay = MountData.inAirFrameDelay;
            MountData.swimFrameStart = MountData.inAirFrameStart;

            if (Main.netMode != NetmodeID.Server)
            {
                MountData.textureWidth = MountData.frontTexture.Width();
                MountData.textureHeight = MountData.frontTexture.Height();

            }
            base.SetStaticDefaults();
        }
        float a = 0;
        public override void UpdateEffects(Player player)
        {
            Vector2 nvel = Main.MouseWorld - player.Center;
            nvel.Normalize();
            Vector2 pos;
            if (player.direction == 1) pos = player.Center + new Vector2(25, -16);
            else pos = player.Center + new Vector2(-25, -16);

            int extraDmg = SmMthd.FindMaxDmgItem(player).damage;
            if (a % 45 == 0)
            {
                Projectile.NewProjectile(player.GetSource_FromAI(), pos, nvel * 17, ProjectileType<炮弹>(), (int)(50 + extraDmg * 1.5f), 6, player.whoAmI);
                Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, Vector2.Zero, ProjectileType<碰撞弹幕>(), (int)(30 + extraDmg * 0.7f), 6, player.whoAmI);
            }
            if (a % 10 == 0)
            {
                Projectile.NewProjectile(player.GetSource_FromAI(), pos, nvel * 17, ProjectileID.Bullet, (int)(15 + extraDmg * 0.5f), 6, player.whoAmI);
            }
            a++;

            base.UpdateEffects(player);
        }

    }
}
