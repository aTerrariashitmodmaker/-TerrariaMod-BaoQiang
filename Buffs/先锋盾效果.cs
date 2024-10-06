namespace 爆枪英雄.Buffs
{
    public class 先锋盾效果 : ModBuff//继承modbuff类
    {
        public int t = 0;
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;//为true时就是debuff
            Main.buffNoSave[Type] = false;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = true;//为true时不显示剩余时间
        }
        public override void Update(Player player, ref int buffIndex)
        {
            float c = player.statLife;
            float m = player.statLifeMax2;
            float p = c / m;
            player.lifeRegen += 6;
            player.statDefense += 6;
            if (p >= 0.75f)
            {
                player.GetModPlayer<技能效果>().MaxHitByDmg = (int)(m * 0.2f);
                player.endurance += 0.35f;
                player.GetDamage(DamageClass.Generic) -= 0.1f;
                player.noKnockback = true;
            }
            if (p <= 0.35f)
            {
                if (t % 30 == 0)
                {
                    player.Heal(3 + (int)(m * 0.025f));
                }
                t++;
            }


        }
    }

}