namespace 爆枪英雄.Buffs
{
    public class 反击效果 : ModBuff//继承modbuff类
    {
        public int t = 0;
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;//为true时就是debuff
            Main.buffNoSave[Type] = false;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = false;//为true时不显示剩余时间
        }
        public override void Update(Player player, ref int buffIndex)
        {
            float x = player.width;
            float y = player.height;
            if (t % 5 == 0)
            {
                float w = Main.rand.NextFloat(-x - 8, x + 9);
                float h = Main.rand.NextFloat(-y - 8, y + 9);
                Vector2 pos = new Vector2(w, h);
                Dust.NewDust(player.Center + pos, 1, 1, DustID.FireflyHit, 0, 0, 0, Color.Orange, 1.5f);
            }
            base.Update(player, ref buffIndex);
        }
    }

}