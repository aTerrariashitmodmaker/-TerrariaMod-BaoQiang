using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace 爆枪英雄.Items.载具
{
    public class 生命条 : UIState
    {
        private UIText text;
        private UIElement area;
        private UIImage barFrame;
        private Color gradientA;
        private Color gradientB;

        public override void OnInitialize()
        {
            area = new UIElement();
            area.Left.Set(-138/2, 0.5f); // Place the resource bar to the left of the hearts.
            area.Top.Set(48, 0.5f); // Placing it just a bit below the top of the screen.
            area.Width.Set(138, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
            area.Height.Set(34, 0f);

            barFrame = new UIImage(Request<Texture2D>("爆枪英雄/Items/载具/载具生命条")); // Frame of our resource bar
            barFrame.Left.Set(0, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(138, 0f);
            barFrame.Height.Set(34, 0f);

            text = new UIText("0/0", 0.8f); // text to show stat
            text.Width.Set(138, 0f);
            text.Height.Set(34, 0f);
            text.Top.Set(10, 0f);
            text.Left.Set(0, 0f);

            gradientA = new Color(123, 25, 138); // A dark purple
            gradientB = new Color(187, 91, 201); // A light purple

            area.Append(text);
            area.Append(barFrame);
            Append(area);

            base.OnInitialize();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            // This prevents drawing unless we are using an ExampleCustomResourceWeapon
            if (!Main.LocalPlayer.GetModPlayer<技能效果>().InVehicle)
                return;

            base.Draw(spriteBatch);
        }
        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var modPlayer = Main.LocalPlayer.GetModPlayer<护盾机制>();
            // Calculate quotient
            float quotient = (float)modPlayer.当前载具耐久 / modPlayer.最大载具耐久; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.创建一个商，表示 currentResource 与 maximumResource 的差值，从而产生 0-1f 的浮点数。
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
                float percent = (float)i / (right - left);
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!Main.LocalPlayer.GetModPlayer<技能效果>().InVehicle)
                return;

            var modPlayer = Main.LocalPlayer.GetModPlayer<护盾机制>();
            // Setting the text per tick to update and show our resource values.
            int a = modPlayer.当前载具耐久;
            int b = modPlayer.最大载具耐久;
            text.SetText(a.ToString() + "/" + b.ToString());
            base.Update(gameTime);
        }
    }
    internal class 生命条系统 : ModSystem
    {
        private UserInterface 生命条接口;
        private 生命条 生命条实例;

        public static LocalizedText ExampleResourceText { get; private set; }

        public override void Load()
        {
            生命条实例 = new();
            生命条接口 = new();
            生命条接口.SetState(生命条实例);

            //  string category = "UI";
            //  ExampleResourceText ??= Mod.GetLocalization($"{category}.ExampleResource");
            ExampleResourceText ??= Mod.GetLocalization("载具条");
        }

        public override void UpdateUI(GameTime gameTime)
        {
            生命条接口?.Update(gameTime);
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
                        生命条接口.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
