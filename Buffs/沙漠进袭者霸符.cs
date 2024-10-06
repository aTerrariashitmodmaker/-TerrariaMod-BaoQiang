using 爆枪英雄.Items.载具;

namespace 爆枪英雄.Buffs
{
    public class 沙漠进袭者霸符 : ModBuff//继承modbuff类
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;//为true时就是debuff
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            //player.buffTime[buffIndex] = 10;
            player.mount.SetMount(MountType<DesertRaider>(), player);
            player.GetModPlayer<技能效果>().InVehicle = true;
            base.Update(player, ref buffIndex);
        }
    }

}