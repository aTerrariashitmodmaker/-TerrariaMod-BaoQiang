namespace 爆枪英雄.Buffs
{
    public abstract class 爆枪冷却 : ModBuff//继承modbuff类
    {
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
            player.GetModPlayer<技能效果>().冷却标记=true;
          
            base.Update(player, ref buffIndex);
        }
    }
    public class 嗜爪冷却 : 爆枪冷却//继承modbuff类
    {
        public override void SetStaticDefaults()
        {

            Main.debuff[Type] = true;//为true时就是debuff
            Main.buffNoSave[Type] = true;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = false;//为true时不显示剩余时间
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
    }
    public class 万弹归宗冷却 : 爆枪冷却//继承modbuff类
    {
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
        //我们做一个让NPC扣血的debuff    
    }
    public class 隐身冷却 : 爆枪冷却//继承modbuff类
    {
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
        //我们做一个让NPC扣血的debuff    
    }
    public class 狂暴冷却 : 爆枪冷却//继承modbuff类
    {
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
        //我们做一个让NPC扣血的debuff    
    }
    public class 金刚钻冷却 : 爆枪冷却//继承modbuff类
    {
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
    public class 电离折射冷却 : 爆枪冷却//继承modbuff类
    {
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
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] += time;
            return base.ReApply(player, time, buffIndex);
        }
    }
    public class 镭射穿梭器冷却 : 爆枪冷却//继承modbuff类
    {
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
    public class 反击冷却 : 爆枪冷却//继承modbuff类
    {
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
    public class 载具冷却 : 爆枪冷却//继承modbuff类
    {
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
        //public override void Update(Player player, ref int buffIndex)
        //{
        //    player.ClearBuff(BuffType<沙漠进袭者霸符>());
        //    player.buffImmune[BuffType<沙漠进袭者霸符>()]=true;
        //    base.Update(player, ref buffIndex);
        //}
    }
    public class 全域光波冷却 : 爆枪冷却//继承modbuff类
    {
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
    public class 烟花弩冷却 : 爆枪冷却   //继承modbuff类
    {
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
}
