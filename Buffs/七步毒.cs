namespace 爆枪英雄.Buffs
{
    public class 七步毒 : ModBuff//继承modbuff类
    {

        public override void SetStaticDefaults()
        {


            Main.debuff[Type] = false;//为true时就是debuff
            Main.buffNoSave[Type] = false;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = true;//为true时不显示剩余时间

            //以下为debuff不可被护士去除
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            //以下为专家模式Debuff持续时间是否延长
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)//这个函数是当玩家获得BUFF时每帧执行一次，玩家大多数属性每帧重置
        {
            player.GetModPlayer<技能效果>().七步毒 = true;
            player.GetModPlayer<技能效果>().神级技能 = true;
        }
    }
}