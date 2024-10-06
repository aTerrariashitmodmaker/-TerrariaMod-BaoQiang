namespace 爆枪英雄.Buffs
{

    public class 致残 : ModBuff//继承modbuff类
    {
        public int c = 0;
        public override string Texture => "爆枪英雄/Buffs/NpcDebuff";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;//为true时就是debuff
            Main.buffNoSave[Type] = false;//为true时退出世界时buff消失
            Main.buffNoTimeDisplay[Type] = false;//为true时不显示剩余时间		
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            c++;
            float x = npc.width;
            float y = npc.height;
            if (c % 2 == 0)
            {
                float w = Main.rand.NextFloat(-x - 8, x + 9);
                float h = Main.rand.NextFloat(-y - 8, y + 9);
                Vector2 pos = new Vector2(w, h);
                Dust.NewDust(npc.Center + pos, 2, 2, DustID.Firefly, 0, 0, 0, Color.Black, 5);
            }
            base.Update(npc, ref buffIndex);
        }
    }

}