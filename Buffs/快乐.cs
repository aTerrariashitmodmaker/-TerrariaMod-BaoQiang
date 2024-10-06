namespace 爆枪英雄.Buffs
{
    public class 快乐 : ModBuff//继承modbuff类
    {

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;//为true时就是debuff
            Main.buffNoSave[Type] = false;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = false;//为true时不显示剩余时间		
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<某些装备效果>().三倍暴击概率 += 15;
            base.Update(player, ref buffIndex);
        }
    }

}