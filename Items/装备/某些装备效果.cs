using Terraria.Audio;

namespace 爆枪英雄.Items.装备
{
    public class 某些装备效果 : ModPlayer
    {
        public bool 白刘海 = false;
        public bool 丛林衣 = false;
        public bool 装备闪避 = false;
        public bool 鬼爵套 = false;
        public float 分身冷却 = 0f;
        public float 闪避概率 = 0f;
        public float 三倍暴击概率 = 0f;

        public override void ResetEffects()
        {
            白刘海 = false;
            丛林衣 = false;
            鬼爵套 = false;
            if (分身冷却 > 0) 分身冷却--;
            闪避概率 = 0f;
            三倍暴击概率 = 0f;
            base.ResetEffects();
        }
        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (装备闪避)
            {
                装备闪避 = false;
                Player.SetImmuneTimeForAllTypes(Player.longInvince ? 120 : 80);
                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(Player.Center + speed * 16, DustID.YellowTorch, speed * 5, Scale: 1.5f);
                    d.noGravity = true;
                }
                SoundEngine.PlaySound(SoundID.Shatter with { Pitch = 0.5f });
                return true;
            }
            return base.FreeDodge(info);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (白刘海)
            {
                target.SimpleStrikeNPC(5, default, Main.rand.NextBool(4), 0, default, false, 0);
            }
            base.OnHitNPC(target, hit, damageDone);

        }
        public void TripleCrit(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.CritDamage += 1f;
            modifiers.SetCrit();
            Rectangle rectangle = new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height);
            CombatText.NewText(rectangle, Color.MediumPurple, "TripleCrit!", true, true);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (SmMthd.Percented(三倍暴击概率))
            {
                TripleCrit(target, ref modifiers);
            }
            base.ModifyHitNPC(target, ref modifiers);
        }
        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (丛林衣 && (npc.type == NPCID.TurtleJungle || npc.type == NPCID.Derpling))
            {
                modifiers.FinalDamage *= 0.4f;
            }
            base.ModifyHitByNPC(npc, ref modifiers);
        }
        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            if ((proj.type == ProjectileID.Stinger || proj.type == ProjectileID.QueenBeeStinger || proj.type == ProjectileID.Spike || proj.type == ProjectileID.JungleSpike || proj.type == ProjectileID.RollingCactusSpike) && 丛林衣)
            {
                modifiers.FinalDamage *= 0.4f;
            }
            base.ModifyHitByProjectile(proj, ref modifiers);
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            modifiers.ModifyHurtInfo += Dodge_ModifyHurtInfo;
            base.ModifyHurt(ref modifiers);
        }

        private void Dodge_ModifyHurtInfo(ref Player.HurtInfo info)
        {
            if (SmMthd.Percented(闪避概率))
            {
                装备闪避 = true;
            }
        }
    }
}
