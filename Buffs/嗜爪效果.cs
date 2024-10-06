using System;

namespace 爆枪英雄.Buffs
{
    public class 嗜爪效果 : ModBuff//继承modbuff类
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;//为true时就是debuff
            Main.buffNoSave[Type] = false;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = false;//为true时不显示剩余时间
        }
        float t = 0;
        public override void Update(Player player, ref int buffIndex)
        {
            t++;
            if (t % 3 == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    double dd = Math.PI / 4 * i;
                    float heng = (float)Math.Cos(dd);
                    float zong = (float)Math.Sin(dd);
                    Vector2 vvel = new(heng, zong);
                    Dust.NewDust(player.Center + vvel * 35f, default, default, DustID.Firework_Red, 0, 2f);
                }
            }
          
        }
    }

}