using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using 爆枪英雄.Items.武器;
using 爆枪英雄.常用类和结构体;

namespace 爆枪英雄.Projectiles
{
    public class 超模刀本体弹幕:ModProjectile
    {
        Player player => Main.player[Projectile.owner];//获取玩家
        public override void SetStaticDefaults()//以下照抄
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;//这一项赋值2可以记录运动轨迹和方向（用于制作拖尾）
            ProjectileID.Sets.TrailCacheLength[Type] = 15;//这一项代表记录的轨迹最多能追溯到多少帧以前
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 310;
            Projectile.height = 50;
            Projectile.friendly = true;//友方弹幕
            Projectile.tileCollide = false;//穿墙
            Projectile.aiStyle = -1;//不使用原版AI
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;//无限穿透
            Projectile.ignoreWater = true;//无视液体

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;

            //或者让它不死 一直转(
            Projectile.timeLeft = 12;//弹幕 趋势 的时间

            base.SetDefaults();
        }     
        int a = 1;
        int b;
        public override void AI()//模拟"刀"的挥舞逻辑
        {
            player.itemTime = player.itemAnimation = 20;//防止弹幕没有 趋势 玩家就又可以使用武器了
            Projectile.Center = player.Center;//绑定玩家和弹幕的位置
           // Projectile.velocity = new Vector2(0, -10).RotatedBy(Projectile.rotation);//给弹幕一个速度 仅仅用于击退方向           
            if (Projectile.timeLeft == 12)
            {
                b=Projectile.timeLeft;           
                if(Main.MouseWorld.X > player.Center.X)
                {
                    Projectile.rotation = -MathHelper.Pi * 2 / 3;
                }
                else
                {
                    Projectile.rotation = -MathHelper.Pi  / 3;
                    a=-1;
                }
                                            
            }
            Projectile.spriteDirection = Projectile.direction = a;
            Projectile.rotation += (MathHelper.Pi * 5 / 6 / b)*a;//弹幕旋转角度
            base.AI();
        }
        public override bool ShouldUpdatePosition()
        {
            return false;//让弹幕位置不受速度影响
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            var r1 = projHitbox.Width / 2 + projHitbox.Height / 2+100;
            var r2 = targetHitbox.Width / 2 + targetHitbox.Height / 2;
            var distance =Vector2.Distance(projHitbox.Center.ToVector2(), targetHitbox.Center.ToVector2());
            if (distance < r1 + r2)
            {
                return true;
            }
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            int rageCount = player.GetModPlayer<副手相关>().rage;
            float times=rageCount/200;
            modifiers.FlatBonusDamage += target.life *( 0.03f+0.02f*times);
            modifiers.FinalDamage += times / 2f;
            base.ModifyHitNPC(target, ref modifiers);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            //画出这把 剑 的样子
            Main.spriteBatch.Draw(TextureAssets.Projectile[Type].Value,
                           player.Center + new Vector2(0, 0) - Main.screenPosition,
                           null,
                           lightColor,
                           Projectile.rotation,
                           new Vector2(50, 25),
                           1.3f,
                           Projectile.direction == a ? SpriteEffects.None : SpriteEffects.FlipVertically,
                           0);

            List<Vertex> vertices = new List<Vertex>();
            for (int i = 0; i <= 14; i++)
            {
                if (Projectile.oldPos[i] != Vector2.Zero)
                {
                    float f=float.Lerp(40, 310, i / 15f);
                    var pos1 = Projectile.Center + new Vector2(0, f).RotatedBy(Projectile.oldRot[i] - MathHelper.ToRadians(90)) - Main.screenPosition;
                    var pos2 = Projectile.Center + new Vector2(0, 340).RotatedBy(Projectile.oldRot[i] - MathHelper.ToRadians(90)) - Main.screenPosition;
                    vertices.Add(new Vertex(pos1, new Vector3(0, i / 15f, 1), Color.Gold));
                    vertices.Add(new Vertex(pos2, new Vector3(1, i / 15f, 1), Color.Gold));
                }
                    
            }
            顶点绘制函数.DrawVertices(vertices, "爆枪英雄/Projectiles/NoToB");
            return false;
        }
       public override void OnKill(int timeLeft)
       {
            player.AddBuff(BuffType<可识破>(), 90);
       }

    }
    public class 超模刀剑气 : ModProjectile
    {
        Player player => Main.player[Projectile.owner];//获取玩家
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;//友方弹幕
            Projectile.tileCollide = false;//穿墙
            Projectile.aiStyle = 1;//不使用原版AI
            AIType = ProjectileID.DD2SquireSonicBoom;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate =5;//无限穿透
            Projectile.ignoreWater = true;//无视液体

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;

            //或者让它不死 一直转(
            Projectile.timeLeft = 200;//弹幕 趋势 的时间
            Projectile.scale = 2f;
            base.SetDefaults();
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            int rageCount = player.GetModPlayer<副手相关>().rage;
            float times = rageCount / 200;
            modifiers.FlatBonusDamage += target.life * (0.02f+0.01f * times );
            modifiers.FinalDamage += times / 5f;
            base.ModifyHitNPC(target, ref modifiers);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            var r1 = projHitbox.Width / 2 + projHitbox.Height / 2 + 100;
            var r2 = targetHitbox.Width / 2 + targetHitbox.Height / 2;
            var distance = Vector2.Distance(projHitbox.Center.ToVector2(), targetHitbox.Center.ToVector2());
            if (distance < r1 + r2)
            {
                return true;
            }
            return false;
        }
        //public override void AI()
        //{
        //    Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
        //}
        //void dra()
        //{
        //    List<Vertex> vertices = new List<Vertex>();
        //    for (int i = 0; i <= 14; i++)
        //    {
        //        if (Projectile.oldPos[i] != Vector2.Zero)
        //        {
        //            var pos1 = Projectile.oldPos[i] + new Vector2(0,-50).RotatedBy(Projectile.oldRot[i]) - Main.screenPosition;
        //            var pos2 = Projectile.oldPos[i] + new Vector2(0,50).RotatedBy(Projectile.oldRot[i]) - Main.screenPosition;
        //            vertices.Add(new Vertex(pos1, new Vector3(i / 9f, 0, 1), Color.Yellow));
        //            vertices.Add(new Vertex(pos2, new Vector3(i / 9f, 1, 1), Color.Yellow));
        //        }
        //    }
        //    顶点绘制函数.DrawVertices(vertices, "爆枪英雄/Projectiles/EdgeLight");
        //}
        //public override bool PreDraw(ref Color lightColor)
        //{
        //    dra();
        //    dra();
        //    return false;
        //}
    }
}
