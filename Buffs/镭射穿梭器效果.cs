namespace 爆枪英雄.Buffs
{
    public class 镭射穿梭器效果 : ModBuff//继承modbuff类
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
            if (t % 2 == 0)
            {
                float posX = Main.rand.Next(-57, 58);
                float posY = Main.rand.Next(-57, 58);
                Vector2 pos = new Vector2(posX, posY);
                int num2 = Dust.NewDust(player.Center + pos, default, default, DustID.UltraBrightTorch, 0, 0, default, default, 2.34f);
                Main.dust[num2].noGravity = true;
            }
           
        }
    }

}