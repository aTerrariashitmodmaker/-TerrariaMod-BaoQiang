using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace 爆枪英雄.常用函数
{
    public class SmMthd
    {
        public static bool Percented(float a)
        {
            return Main.rand.Next(0, 101) < a;
        }
        public static Item FindMaxDmgItem(Player player)
        {
            Item[] inventory = player.inventory;
            int maxdmg = 0;
            Item item = null;
            for (int k = 0; k < inventory.Length; k++)
            {
                if (inventory[k].damage > maxdmg)
                {
                    maxdmg = inventory[k].damage;
                    item = inventory[k];
                }
            }
            return item;
        }
        public static void BurnDusts(Player player,int dustID)
        {
            if (Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 10, player.height + 10, dustID, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default, 2.4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
                if (Main.rand.NextBool(4))
                {
                    Main.dust[dust].noGravity = false;
                    Main.dust[dust].scale *= 0.5f;
                }
            }

        }
        public static void BurnDusts2(Player player, int dustID)
        {
            if (Main.rand.Next(5) < 4)
            {
                int dust = Dust.NewDust(player.Center - new Vector2(2f), player.width + 4, player.height + 4, dustID, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default, 1.1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.75f;
                Main.dust[dust].velocity.X = Main.dust[dust].velocity.X * 0.75f;
                Main.dust[dust].velocity.Y = Main.dust[dust].velocity.Y - 3f;
                if (Main.rand.NextBool(4))
                {
                    Main.dust[dust].noGravity = false;
                    Main.dust[dust].scale *= 0.3f;
                }
            }
        }
        public static bool FindTar(Player player, float maxDistance)
        {
            bool homeIn = false;//数组内是否有目标
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].CanBeChasedBy(player, false) && Main.npc[i].townNPC == false)
                {
                    float extraDistance = (Main.npc[i].width / 2) + (Main.npc[i].height / 2);//额外距离
                    if (player.WithinRange(Main.npc[i].Center, maxDistance + extraDistance))
                    {
                        homeIn = true;
                        break;
                    }
                }
            }
            return homeIn;
        }

        //weightedAver加权平均，数值越大路径越准确，但轨迹越不柔和
        //类叶绿弹追踪
        public static void VanillaChase<T>(Projectile proj, T attacker)
        {
            float num75 = (float)Math.Sqrt(proj.velocity.X * proj.velocity.X + proj.velocity.Y * proj.velocity.Y);//求出弹幕速度的模
            float num76 = proj.localAI[0];
            if (num76 == 0f)
            {
                proj.localAI[0] = num75;
                num76 = num75;
            }
            float num77 = proj.position.X;//弹幕水平位置
            float num78 = proj.position.Y;//弹幕竖直位置，这俩玩意应该是npc的位置
            float num79 = 800f;//追击范围
            bool flag4 = false;//是否追击
            int num81 = 0;
            if (proj.ai[1] == 0f)//ai【1】是甚么东西
            {
                for (int num82 = 0; num82 < 200; num82++)
                {
                    if (Main.npc[num82].CanBeChasedBy(attacker) && (proj.ai[1] == 0f || proj.ai[1] == (float)(num82 + 1)))
                    {
                        float num83 = Main.npc[num82].position.X + (float)(Main.npc[num82].width / 2);
                        float num84 = Main.npc[num82].position.Y + (float)(Main.npc[num82].height / 2);//这俩是npc的center
                        float num85 = Math.Abs(proj.position.X + (float)(proj.width / 2) - num83) + Math.Abs(proj.position.Y + (float)(proj.height / 2) - num84);
                        //这是弹幕和目标横坐标距离加纵坐标距离，为啥不直接是这俩玩意的距离啊
                        if (num85 < num79 && Collision.CanHit(new Vector2(proj.position.X + (float)(proj.width / 2), proj.position.Y + (float)(proj.height / 2))//弹幕的中心
        , 1, 1, Main.npc[num82].position, Main.npc[num82].width, Main.npc[num82].height))//判断击中语句
                        {
                            num79 = num85;//这应该是最小追击距离
                            num77 = num83;
                            num78 = num84;//真抽象，把弹幕的位置赋值
                            flag4 = true;
                            num81 = num82;//记录前一个npc
                        }
                    }
                }//距离最近的npc的位置
                if (flag4)
                {
                    proj.ai[1] = num81 + 1;
                }
                flag4 = false;
            }
            if (proj.ai[1] > 0f)// ai【1】也许是记录npc的罢
            {
                int num86 = (int)(proj.ai[1] - 1f);
                if (Main.npc[num86].active && Main.npc[num86].CanBeChasedBy(attacker, ignoreDontTakeDamage: true) && !Main.npc[num86].dontTakeDamage)
                {
                    //我可以这么假设，前一个npc存活且可以被追击且此npc能造成伤害，简单说就是此npc是敌方npc

                    float num87 = Main.npc[num86].position.X + (float)(Main.npc[num86].width / 2);
                    float num88 = Main.npc[num86].position.Y + (float)(Main.npc[num86].height / 2);//这俩玩意是npc的center
                    if (Math.Abs(proj.position.X + (float)(proj.width / 2) - num87) + Math.Abs(proj.position.Y + (float)(proj.height / 2) - num88) < 1000f)
                    {//又抽象了，应该是弹幕和npc的距离小于1000
                        flag4 = true;
                        num77 = Main.npc[num86].position.X + (float)(Main.npc[num86].width / 2);
                        num78 = Main.npc[num86].position.Y + (float)(Main.npc[num86].height / 2);//npc的位置
                    }
                }
                else
                {
                    proj.ai[1] = 0f;
                }
            }
            if (!proj.friendly)//如果是敌方弹幕
            {
                flag4 = false;
            }
            if (flag4)
            {
                float num224 = num76;
                Vector2 vector16 = new(proj.position.X + (float)proj.width * 0.5f, proj.position.Y + (float)proj.height * 0.5f);//弹幕的center
                float num91 = num77 - vector16.X;
                float num93 = num78 - vector16.Y;
                float num95 = (float)Math.Sqrt(num91 * num91 + num93 * num93);//弹幕此时与npc的距离
                num95 = num224 / num95;//此处为弹幕速度大小除以弹幕与npc的距离
                num91 *= num95;
                num93 *= num95;
                int num96 = 8;
                proj.velocity.X = (proj.velocity.X * (float)(num96 - 1) + num91) / (float)num96;
                proj.velocity.Y = (proj.velocity.Y * (float)(num96 - 1) + num93) / (float)num96;
            }
        }
        private static Vector3 GetTarPos<T>(Projectile proj, T attacker)
        {
            float tarX = proj.position.X;
            float tarY = proj.position.Y;
            float chaseRange = 2000f;//追击范围

            //获取目标XY坐标，若无目标则为弹幕本身
            #region                                  
            //求出弹幕速度的模(float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y)
            float projLen = proj.localAI[0];
            if (projLen == 0f)
            {
                proj.localAI[0] = proj.velocity.Length();
            }

            int flag4 = 0;//是否追击
            int tarWho = 0;
            if (proj.ai[1] == 0f)//ai【1】是甚么东西
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].CanBeChasedBy(attacker) && (proj.ai[1] == 0f || proj.ai[1] == i + 1))
                    {
                        float npcCenterX = Main.npc[i].Center.X;
                        float npcCenterY = Main.npc[i].Center.Y;//这俩是npc的center
                        float disXANDdisY = Math.Abs(proj.Center.X - npcCenterX) + Math.Abs(proj.Center.Y - npcCenterY);
                        //Main.npc[tarWhoAmI].position.X + Main.npc[tarWhoAmI].width / 2
                        if (disXANDdisY < chaseRange && Collision.CanHit(proj.Center//new Vector2(Projectile.position.X + Projectile.width / 2, Projectile.position.Y + Projectile.height / 2)弹幕的中心
        , 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))//判断击中语句
                        {
                            chaseRange = disXANDdisY;//这应该是最小追击距离
                            tarX = npcCenterX;
                            tarY = npcCenterY;//真抽象，把弹幕的位置赋值
                            flag4 = 1;
                            tarWho = i;//记录前一个npc
                        }
                    }
                }//距离最近的npc的位置
                if (flag4 == 1)
                {
                    proj.ai[1] = tarWho + 1;
                }
                flag4 = 0;
            }
            if (proj.ai[1] > 0f)
            {
                int tarWhoAmI = (int)(proj.ai[1] - 1);
                if (Main.npc[tarWhoAmI].active && Main.npc[tarWhoAmI].CanBeChasedBy(attacker, ignoreDontTakeDamage: true) && !Main.npc[tarWhoAmI].dontTakeDamage)
                {
                    float tarCenterX = Main.npc[tarWhoAmI].Center.X;//Main.npc[tarWhoAmI].position.X + Main.npc[tarWhoAmI].width / 2
                    float tarCenterY = Main.npc[tarWhoAmI].Center.Y;
                    if (Math.Abs(proj.Center.X - tarCenterX) + Math.Abs(proj.Center.Y - tarCenterY) < 1000f)
                    {
                        flag4 = 1;
                        tarX = tarCenterX;
                        tarY = tarCenterY;
                    }
                }
                else
                {
                    proj.ai[1] = 0f;
                }
            }
            #endregion
            return new(tarX, tarY, flag4);
        }
        public static void ChaseEffect<T>(Projectile proj, T attacker)
        {
            Vector3 param = GetTarPos(proj, attacker);
            if (param.Z == 1)
            {
                Vector2 targetPos = new(param.X, param.Y);
                // 需要移动到的位置，加权平均
                var pos = 0.9f * proj.Center + 0.1f * targetPos;
                // 然后假装自己是用速度实现的
                proj.velocity = pos - proj.Center;
            }
        }
        public static void ChaseEffect2<T>(Projectile proj, T attacker)
        {
            Vector3 param = GetTarPos(proj, attacker);
            if (param.Z == 1)
            {
                Vector2 targetPos = new Vector2(param.X, param.Y);
                Vector2 targetVel = Vector2.Normalize(targetPos - proj.Center);
                targetVel *= 7f;
                // X分量的加速度
                float accX = 0.5f;
                // Y分量的加速度
                float accY = 0.5f;
                proj.velocity.X += (proj.velocity.X < targetVel.X ? 1 : -1) * accX;
                proj.velocity.Y += (proj.velocity.Y < targetVel.Y ? 1 : -1) * accY;
            }
        }
        //类星云烈焰追踪
        public static int GetTarWho<T>(Vector2 pos, T attacker, float range)
        {
            //找到距离弹幕最近目标
            int TarWho = -1;
            float minDis = range;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active && Main.npc[i].CanBeChasedBy(attacker))
                {
                    Vector2 NPCcenter = Main.npc[i].Center;
                    float Dis = Vector2.Distance(NPCcenter, pos);
                    if (Dis < minDis && TarWho == -1 && Collision.CanHitLine(pos, 1, 1, NPCcenter, 1, 1))
                    {
                        minDis = Dis;
                        TarWho = i;
                    }
                }
            }
            return TarWho;
            //
            //if (minDis < 20f)
            //{
            //    proj.Kill();
            //    return;
            //}

        }

        public static void ChaseEffect3<T>(Projectile proj, T attacker, float ChasePeriod, float range, float speedFactor)
        {
            //无目标时让周期在1到5之间
            if (proj.ai[1] == 0f)
            {
                proj.ai[1] = 1f;
            }
            else if (proj.ai[1] == 1f && proj.owner == Main.myPlayer)
            {
                int TarWho = GetTarWho(proj.Center, attacker, range);//如果有目标，让ai1跳出周期之外执行追踪
                if (TarWho != -1)
                {
                    proj.ai[1] = ChasePeriod + 1f;
                    proj.ai[0] = TarWho;//ai0一般储存目标编号
                    proj.netUpdate = true;
                }

            }
            //跳出周期外执行追踪
            else if (proj.ai[1] > ChasePeriod)
            {
                //传递目标编号
                proj.ai[1] += 1f;
                int TarWho = (int)proj.ai[0];
                //如果目标死亡或不可追踪（无敌），刷新ai0和1
                if (!Main.npc[TarWho].active || !Main.npc[TarWho].CanBeChasedBy(attacker))
                {
                    proj.ai[1] = 1f;
                    proj.ai[0] = 0f;
                    proj.netUpdate = true;
                }
                else
                {
                    proj.velocity.ToRotation();
                    Vector2 Vel = Main.npc[TarWho].Center - proj.Center;
                    if (Vel.Length() < 20f)
                    {
                        proj.Kill();
                        return;
                    }
                    if (Vel != Vector2.Zero)
                    {
                        Vel.Normalize();
                        Vel *= speedFactor;
                    }
                    float weightAver = 30f;
                    proj.velocity = (proj.velocity * (weightAver - 1f) + Vel) / weightAver;
                }
            }
            if (proj.ai[1] >= 1f && proj.ai[1] < ChasePeriod)
            {
                proj.ai[1] += 1f;
                if (proj.ai[1] == ChasePeriod)
                {
                    proj.ai[1] = 1f;
                }
            }
        }
    }
}
