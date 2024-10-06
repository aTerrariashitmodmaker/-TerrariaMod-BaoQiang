using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI;

namespace 爆枪英雄
{
    public class 副手相关 : ModPlayer
    {
        public bool HoldIt = false;
        public int rage = 0;
        public override void ResetEffects()
        {
            HoldIt = false;
            rage++;
            if (rage > 1000) rage = 1000;
            if (rage < 0) rage = 0;
            base.ResetEffects();
        }
        public override bool ConsumableDodge(Player.HurtInfo info)
        {
            if (Player.HasBuff<识破成功>())
            {
                ConsumableDodgeEffects();
                return true;
            }
            return false;
        }
        public void ConsumableDodgeEffects()
        {
            rage += 200;
            Player.SetImmuneTimeForAllTypes(150);

            // Some sound and visual effects
            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Player.Center + speed * 20, DustID.Ichor, speed * 5, Scale: 1.5f);
                d.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Shatter with { Pitch = 0.5f });

            // The visual and sound effects happen on all clients, but the code below only runs for the dodging player 
            if (Player.whoAmI != Main.myPlayer)
            {
                return;
            }

            // Clearing the buff and assigning the cooldown time
            Player.ClearBuff(BuffType<识破成功>());

        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            modifiers.ModifyHurtInfo += ModifyHurtInfo_ChaoMo;
            base.ModifyHurt(ref modifiers);
        }
        private void ModifyHurtInfo_ChaoMo(ref Player.HurtInfo info)
        {
            if (Player.HasBuff<识破中>())
            {
                Player.AddBuff(BuffType<识破成功>(), 100);
                Player.AddBuff(BuffType<识破成功剑气>(), 100);
            }
        }
    }
    public class 怒气条 : UIState
    {
        private UIText text;
        private UIElement area;
        private UIImage barFrame;
        public override void OnInitialize()
        {
            area = new UIElement();
            area.Left.Set(-50, 0.5f); // Place the resource bar to the left of the hearts.
            area.Top.Set(-48, 0.5f); // Placing it just a bit below the top of the screen.
            area.Width.Set(100, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
            area.Height.Set(17, 0f);

            barFrame = new UIImage(Request<Texture2D>("爆枪英雄/Items/武器/RageBar")); // Frame of our resource bar
            barFrame.Left.Set(0, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(100, 0f);
            barFrame.Height.Set(17, 0f);

            text = new UIText("0/0", 0.8f); // text to show stat
            text.Width.Set(100, 0f);
            text.Height.Set(17, 0f);
            text.Top.Set(-15, 0f);
            text.Left.Set(0, 0f);

            area.Append(text);
            area.Append(barFrame);
            Append(area);

            base.OnInitialize();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            // This prevents drawing unless we are using an ExampleCustomResourceWeapon
            if (!Main.LocalPlayer.GetModPlayer<副手相关>().HoldIt)
                return;

            base.Draw(spriteBatch);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var modPlayer = Main.LocalPlayer.GetModPlayer<副手相关>();
            // Calculate quotient
            float quotient = (float)modPlayer.rage / 1000; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.创建一个商，表示 currentResource 与 maximumResource 的差值，从而产生 0-1f 的浮点数。
            quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.将其夹紧到 0-1f，这样它就不会超过这个温度。

            // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.在这里，我们得到 barFrame 元素的屏幕尺寸，然后调整生成的矩形，以在 barFrame 纹理中得出一个矩形，我们将绘制渐变。这些值是在绘图程序中测量的。
            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 2;
            hitbox.Width -= 4;
            hitbox.Y += 2;
            hitbox.Height -= 4;

            // Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.现在，使用这个命中框，我们通过绘制垂直线来绘制渐变，同时在两种颜色之间缓慢插值。
            int left = hitbox.Left;
            int right = hitbox.Right;
            int steps = (int)((right - left) * quotient);
            for (int i = 0; i < steps; i += 1)
            {
                // float percent = (float)i / steps; // Alternate Gradient Approach               
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Yellow);
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (!Main.LocalPlayer.GetModPlayer<副手相关>().HoldIt)
                return;

            var modPlayer = Main.LocalPlayer.GetModPlayer<副手相关>();
            // Setting the text per tick to update and show our resource values.
            int a = modPlayer.rage / 200;
            int b = 5;
            text.SetText("怒气值" + a.ToString() + "/" + b.ToString());
            base.Update(gameTime);
        }
    }
    internal class 怒气条系统 : ModSystem
    {
        private UserInterface 怒气条接口;
        private 怒气条 怒气条实例;

        public static LocalizedText ExampleResourceText { get; private set; }

        public override void Load()
        {
            怒气条实例 = new();
            怒气条接口 = new();
            怒气条接口.SetState(怒气条实例);

            //  string category = "UI";
            //  ExampleResourceText ??= Mod.GetLocalization($"{category}.ExampleResource");
            ExampleResourceText ??= Mod.GetLocalization("怒气条");
        }

        public override void UpdateUI(GameTime gameTime)
        {
            怒气条接口?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "爆枪英雄: 载具生命条",
                    delegate
                    {
                        怒气条接口.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
