namespace 爆枪英雄.Dusts
{
    public class 蓝白粒子 : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.color = default;
            dust.velocity *= 0.5f;
            dust.noGravity = true;
            dust.noLight = true;
            dust.alpha = 50;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.scale *= 0.94f;
            dust.velocity *= 0.90f;
            Lighting.AddLight(dust.position, 0.114f, 0.255f, 0.255f);
            if (dust.scale < 0.4f)
            {
                dust.active = false;
            }
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor) => new Color(Color.White.R, Color.White.G, Color.White.B, dust.alpha);
    }
}
