namespace 爆枪英雄.Buffs
{
    public class 七步毒敌 : ModBuff//继承modbuff类
    {
        public override void SetStaticDefaults()
        {

            Main.debuff[Type] = true;//为true时就是debuff
            Main.buffNoSave[Type] = false;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = false;//为true时不显示剩余时间

            //以下为debuff不可被护士去除
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            //以下为专家模式Debuff持续时间是否延长
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }
        //我们做一个让NPC扣血的debuff
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.color = Microsoft.Xna.Framework.Color.Green;
            npc.GetGlobalNPC<全局NPC>().qibudu = true;
        }
    }

}