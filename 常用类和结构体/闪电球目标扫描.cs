namespace 爆枪英雄.常用函数
{
    public static class 闪电球目标扫描
    {
        public static bool FindEnemy(Projectile projectile, float distanceRequired, float projectileTimer)
        {
            // Only shoot once every N frames.
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] > projectileTimer)
            {
                projectile.localAI[1] = 0f;

                // Only search for targets if projectiles could be fired.
                float maxDistance = distanceRequired;//寻敌距离
                bool homeIn = false;//数组内是否有目标
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].CanBeChasedBy(projectile, false) && Main.npc[i].townNPC == false)//如果目标可以被追踪
                    {
                        float extraDistance = (Main.npc[i].width / 2) + (Main.npc[i].height / 2);//额外距离
                        bool canHit = true;
                        if (extraDistance < maxDistance)
                            canHit = Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1);

                        if (projectile.WithinRange(Main.npc[i].Center, maxDistance + extraDistance)) //&& canHit
                        {
                            homeIn = true;
                            break;
                        }
                    }
                }
                return homeIn;
            }
            else
            {
                return false;
            }

        }
        public static int[] enemyArray(Projectile projectile, float distanceRequired, int maxTargets)
        {
            float maxDistance = distanceRequired;//寻敌距离           
            int[] targetArray = new int[maxTargets];//定义一个最大寻敌个数的数组
            int targetArrayIndex = 0;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].CanBeChasedBy(projectile, false) && Main.npc[i].townNPC == false)//如果目标可以被追踪
                {
                    float extraDistance = (Main.npc[i].width / 2) + (Main.npc[i].height / 2);//额外距离
                    bool canHit = true;
                    if (extraDistance < maxDistance)
                        canHit = Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1);

                    if (projectile.WithinRange(Main.npc[i].Center, maxDistance + extraDistance))
                    {
                        if (targetArrayIndex < maxTargets)
                        {
                            targetArray[targetArrayIndex] = i;
                            targetArrayIndex++;
                        }
                        else
                            break;
                    }
                }
            }
            return targetArray;
        }
        public static void MagnetSphereHitscan(Projectile projectile, float distanceRequired, float homingVelocity, float projectileTimer, int maxTargets, int spawnedProjectile, double damageMult = 1D, bool attackMultiple = false)
        {
            // Only shoot once every N frames.
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] > projectileTimer)
            {
                projectile.localAI[1] = 0f;

                // Only search for targets if projectiles could be fired.
                float maxDistance = distanceRequired;//寻敌距离
                bool homeIn = false;//数组内是否有目标
                int[] targetArray = new int[maxTargets];//定义一个最大寻敌个数的数组
                int targetArrayIndex = 0;

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].CanBeChasedBy(projectile, false))//如果目标可以被追踪
                    {
                        float extraDistance = (Main.npc[i].width / 2) + (Main.npc[i].height / 2);//额外距离
                        //bool canHit = true;
                        //if (extraDistance < maxDistance)
                        //    canHit = Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1);

                        if (projectile.WithinRange(Main.npc[i].Center, maxDistance + extraDistance)) //&& canHit
                        {
                            if (targetArrayIndex < maxTargets)
                            {
                                targetArray[targetArrayIndex] = i;
                                targetArrayIndex++;
                                homeIn = true;
                            }
                            else
                                break;
                        }
                    }
                }

                // If there is anything to actually shoot at, pick targets at random and fire.
                if (homeIn)
                {
                    int randomTarget = Main.rand.Next(targetArrayIndex);
                    randomTarget = targetArray[randomTarget];

                    projectile.localAI[1] = 0f;
                    Vector2 spawnPos = projectile.Center + projectile.velocity * 4f;
                    Vector2 velocity = Vector2.Normalize(Main.npc[randomTarget].Center - spawnPos) * homingVelocity;

                    if (attackMultiple)
                    {
                        for (int i = 0; i < targetArrayIndex; i++)
                        {
                            velocity = Vector2.Normalize(Main.npc[targetArray[i]].Center - spawnPos) * homingVelocity;

                            if (projectile.owner == Main.myPlayer)
                            {
                                int projectile2 = Projectile.NewProjectile(projectile.GetSource_FromThis(), spawnPos, velocity, spawnedProjectile, (int)(projectile.damage * damageMult), projectile.knockBack, projectile.owner, 0f, 0f);

                                //if (projectile.type == ProjectileType<EradicatorProjectile>())
                                //    if (projectile2.WithinBounds(Main.maxProjectiles))
                                //        Main.projectile[projectile2].DamageType = RogueDamageClass.Instance;
                            }
                        }

                        return;
                    }

                    //if (projectile.type == ProjectileType<GodsGambitYoyo>())
                    //{
                    //    velocity.Y += Main.rand.Next(-30, 31) * 0.05f;
                    //    velocity.X += Main.rand.Next(-30, 31) * 0.05f;
                    //}

                    //if (projectile.owner == Main.myPlayer)
                    //{
                    //    int projectile2 = Projectile.NewProjectile(projectile.GetSource_FromThis(), spawnPos, velocity, spawnedProjectile, (int)(projectile.damage * damageMult), projectile.knockBack, projectile.owner, 0f, 0f);

                    //    //if (projectile.type == ProjectileType<GodsGambitYoyo>() || projectile.type == ProjectileType<ShimmersparkYoyo>())
                    //    //    if (projectile2.WithinBounds(Main.maxProjectiles))
                    //    //        Main.projectile[projectile2].DamageType = DamageClass.MeleeNoSpeed;
                    //}
                }
            }
        }
    }
}
