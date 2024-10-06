namespace 爆枪英雄.Buffs
{
    public class 远视效果 : ModBuff//继承modbuff类
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;//为true时就是debuff
            Main.buffNoSave[Type] = false;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = true;//为true时不显示剩余时间
        }
        public override void Update(Player player, ref int buffIndex)
        {

            player.GetCritChance(DamageClass.Generic) += 5;
            if (Main.hardMode && player.velocity.Length() == 0f)
            {
                player.GetDamage(DamageClass.Generic) += 0.15f;
            }
        }
    }

}