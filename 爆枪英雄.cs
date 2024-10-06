global using Microsoft.Xna.Framework;
global using Terraria;
global using Terraria.ID;
global using Terraria.ModLoader;
global using 爆枪英雄.Buffs;
global using 爆枪英雄.Dusts;
global using 爆枪英雄.Items.装备;
global using 爆枪英雄.Projectiles;
global using 爆枪英雄.常用函数;
global using static Terraria.ModLoader.ModContent;

namespace 爆枪英雄
{
    public class 爆枪英雄 : Mod
    {
        internal static ModKeybind YinShenKey;
        internal static ModKeybind cykey;
        internal static ModKeybind shizhua;
        internal static ModKeybind kuangbao;
        internal static ModKeybind jingangzuan;
        internal static ModKeybind dianlizheshe;
        internal static ModKeybind LeiShe;
        internal static ModKeybind BaoQiangUI;
        internal static ModKeybind FanJi;
        internal static ModKeybind QuanYu;

        public override void Load()
        {
            YinShenKey = KeybindLoader.RegisterKeybind(this, "隐身", "N");
            cykey = KeybindLoader.RegisterKeybind(this, "万弹归宗", "L");
            shizhua = KeybindLoader.RegisterKeybind(this, "嗜爪", "L");
            kuangbao = KeybindLoader.RegisterKeybind(this, "狂暴", "L");
            jingangzuan = KeybindLoader.RegisterKeybind(this, "金刚钻", "L");
            dianlizheshe = KeybindLoader.RegisterKeybind(this, "电离折射", "L");
            LeiShe = KeybindLoader.RegisterKeybind(this, "镭射穿梭器", "L");
            BaoQiangUI = KeybindLoader.RegisterKeybind(this, "爆枪英雄UI（未实现）", "L");
            FanJi = KeybindLoader.RegisterKeybind(this, "反击", "L");
            QuanYu = KeybindLoader.RegisterKeybind(this, "全域光波", "L");
        }
        public override void Unload()
        {
            YinShenKey = null;
            cykey = null;
            shizhua = null;
            kuangbao = null;
            jingangzuan = null;
            dianlizheshe = null;
            LeiShe = null;
            BaoQiangUI = null;
            FanJi = null;
            QuanYu = null;
        }

    }
}