namespace 爆枪英雄.Buffs
{
    public class 分身 : ModBuff//继承modbuff类
    {
        public override void SetStaticDefaults()
        {

            Main.debuff[Type] = true;//为true时就是debuff
            Main.buffNoSave[Type] = false;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = true;//为true时不显示剩余时间		
        }
    }

}