namespace 爆枪英雄.Items.载具
{
    public class 护盾机制 : ModPlayer
    {
        public bool 有护盾 = false;
        public bool 盾免疫伤害 = false;
        public int 当前载具耐久 = 0;
        public int 最大载具耐久 = 0;


        public override void ResetEffects()
        {
            //if (!Player.GetModPlayer<技能效果>().InVehicle) 载具耐久 = 0;
            base.ResetEffects();
        }
        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (盾免疫伤害)
            {
                盾免疫伤害 = false;
                Player.SetImmuneTimeForAllTypes(Player.longInvince ? 100 : 60);
                return true;
            }
            return base.FreeDodge(info);
        }
        public override bool ConsumableDodge(Player.HurtInfo info)
        {
            return base.ConsumableDodge(info);
        }
        private void ModifyHurtInfo_BaoQiang(ref Player.HurtInfo info)
        {
            //载具：分类讨论. 1.若伤害高于耐久，则载具爆掉进入冷却，且耐久为0，但玩家不受此伤害。2.若伤害小于耐久，则耐久减去此伤害，玩家依旧不受到此伤害
            if (Player.GetModPlayer<技能效果>().InVehicle && 当前载具耐久 > 0)
            {
                if (当前载具耐久 <= info.Damage)
                {
                    //载具爆,耐久为0
                    当前载具耐久 = 0;
                    最大载具耐久 = 0;
                }
                else
                {
                    当前载具耐久 -= info.Damage;
                }
                盾免疫伤害 = true;
            }
        }
        private void ClearCarrier(Player.HurtInfo hurtInfo)
        {
            if (当前载具耐久 <= hurtInfo.Damage)
            {
                Player.ClearBuff(BuffType<沙漠进袭者霸符>());
            }
        }
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            ClearCarrier(hurtInfo);
            base.OnHitByNPC(npc, hurtInfo);
        }
        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            ClearCarrier(hurtInfo);
            base.OnHitByProjectile(proj, hurtInfo);
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            modifiers.ModifyHurtInfo += ModifyHurtInfo_BaoQiang;
            base.ModifyHurt(ref modifiers);
        }
    }
}
