using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.Shaders;
using Terraria;

namespace 爆枪英雄.Buffs
{
    public class 可识破: ModBuff
    {
        public override string Texture => "爆枪英雄/Buffs/InvisibleBuff";
        public override void SetStaticDefaults()
        {

            Main.debuff[Type] = true;//为true时就是debuff
            Main.buffNoSave[Type] = true;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = false;//为true时不显示剩余时间

            //以下为debuff不可被护士去除
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            //以下为专家模式Debuff持续时间是否延长
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }
       public override void Update(Player player, ref int buffIndex)
       {
           SmMthd.BurnDusts(player, DustID.BlueFlare);
       }
    }
    public class 识破中 : ModBuff
    {
        public override string Texture => "爆枪英雄/Buffs/InvisibleBuff";
        public override void SetStaticDefaults()
        {

            Main.debuff[Type] = true;//为true时就是debuff
            Main.buffNoSave[Type] = true;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = false;//为true时不显示剩余时间

            //以下为debuff不可被护士去除
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            //以下为专家模式Debuff持续时间是否延长
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SmMthd.BurnDusts(player, DustID.SolarFlare);
            player.buffImmune[BuffType<可识破>()] = true;
            base.Update(player, ref buffIndex);
        }

    }
    public class 识破成功 : ModBuff
    {
        public override string Texture => "爆枪英雄/Buffs/InvisibleBuff";
       
        public override void SetStaticDefaults()
        {

            Main.debuff[Type] = true;//为true时就是debuff
            Main.buffNoSave[Type] = true;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = false;//为true时不显示剩余时间

            //以下为debuff不可被护士去除
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            //以下为专家模式Debuff持续时间是否延长
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }
      
    }
    public class 识破成功剑气 : ModBuff
    {
        public override string Texture => "爆枪英雄/Buffs/InvisibleBuff";
        int a = 0;
        public override void SetStaticDefaults()
        {

            Main.debuff[Type] = true;//为true时就是debuff
            Main.buffNoSave[Type] = true;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = false;//为true时不显示剩余时间

            //以下为debuff不可被护士去除
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            //以下为专家模式Debuff持续时间是否延长
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SmMthd.BurnDusts(player, DustID.CursedTorch);
            a++;
            if (a % 20 == 0)
            {
                var vel=Main.MouseWorld - player.Center;
                vel.Normalize();
                Projectile.NewProjectile(player.GetSource_FromAI(), player.Center, vel * 15f, ProjectileType<超模刀剑气>(), player.HeldItem.damage, 5f, player.whoAmI);
            }
                                 
        }
    }
  
}
