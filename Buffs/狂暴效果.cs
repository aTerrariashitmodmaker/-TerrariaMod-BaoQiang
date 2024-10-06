using System;

namespace 爆枪英雄.Buffs
{
    public class 狂暴效果 : ModBuff//继承modbuff类
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
                for (int i = 0; i < 6; i++)
                {
                    double dd = Math.PI / 3 * i;
                    float heng = (float)Math.Cos(dd);
                    float zong = (float)Math.Sin(dd);
                    Vector2 vvel = new(heng, zong);
                    Dust.NewDust(player.Center + vvel * 40f, default, default, DustID.OrangeTorch, 0, 2f);
                }
            }
            float a = 0.75f;
            if (Main.hardMode)
            {
                a = 1.1f;
            }
            player.GetAttackSpeed(DamageClass.Generic) += a;
            player.GetDamage(DamageClass.Summon) += (a + 0.1f) / 2;

        }
    }

}