namespace 爆枪英雄.Buffs
{
    public class 欺盲 : ModBuff//继承modbuff类
    {
        public override string Texture => "爆枪英雄/Buffs/NpcDebuff";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;//为true时就是debuff
            Main.buffNoSave[Type] = false;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = false;//为true时不显示剩余时间		
        }
    }

}