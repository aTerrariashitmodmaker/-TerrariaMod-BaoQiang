using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.UI;

namespace 爆枪英雄.用户界面
{
    public class 我的用户界面 : UIState
    {
        public Player player => Main.LocalPlayer;
        public static bool 可见 = false;
        public UIPanel panel;
        public override void OnInitialize()
        {
            UIPanel panel = new();
            panel.Width.Set(350f, 0f);
            panel.Height.Set(400f, 0f);
            panel.Left.Set(-175f, 0.5f);
            panel.Top.Set(-200f, 0.5f);
            //panel.OnLeftMouseDown+= new MouseEvent (DragStart);
            //panel.OnLeftMouseUp += new MouseEvent(DragEnd);
            Append(panel);

            UIImageButton closebutton = new UIImageButton(Request<Texture2D>("爆枪英雄/用户界面/关闭按钮"));
            closebutton.Width.Set(22f, 0f);
            closebutton.Height.Set(22f, 0f);
            closebutton.Left.Set(0f, 0.91f);
            closebutton.Top.Set(0f, 0f);
            closebutton.OnLeftClick += Closebutton_OnLeftClick;
            panel.Append(closebutton);

            UIImageButton button1 = new UIImageButton(Request<Texture2D>("爆枪英雄/用户界面/button_1"));
            button1.Width.Set(24f, 0f);
            button1.Height.Set(12f, 0f);
            button1.Left.Set(0f, 0.09f);
            button1.Top.Set(0f, 0.05f);
            button1.OnLeftClick+=Button1_OnLeftClick;
            panel.Append(button1);
            //Item[] i=new Item[7];
            //UIItemSlot testItemSlot = new UIItemSlot(i, 7, 1);
            //testItemSlot.Left.Set(-50f, 0.5f);
            //testItemSlot.Top.Set(-50f, 0.5f);
            //panel.Append(testItemSlot);

            base.OnInitialize();
        }

        private void Closebutton_OnLeftClick(UIMouseEvent evt, UIElement listeningElement)
        {
            可见 = false;
        }
        private void Button1_OnLeftClick(UIMouseEvent evt, UIElement listeningElement)
        {
            player.AddBuff(BuffID.OnFire, 60 * 5);
        }
        ////
        //Vector2 offset;
        //public bool dragging = false;
        //private void DragStart(UIMouseEvent evt, UIElement listeningElement)
        //{
        //    //在开始拖动时，该控制板的左上角指向鼠标位置的向量
        //    offset = new Vector2(evt.MousePosition.X - panel.Left.Pixels, evt.MousePosition.Y - panel.Top.Pixels);
        //    dragging = true;
        //}

        //private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
        //{
        //    //拖动结束松开鼠标的位置
        //    Vector2 end = evt.MousePosition;
        //    dragging = false;
        //    //-offset就是开始拖动时鼠标指向面板左上角的向量，此处代码把面板左上角设置为鼠标当前位置加上之前鼠标指向面板左上角位置
        //    panel.Left.Set(end.X - offset.X, 0f);
        //    panel.Top.Set(end.Y - offset.Y, 0f);
        //    Recalculate();
        //}
        //protected override void DrawSelf(SpriteBatch spriteBatch)
        //{
        //    Vector2 MousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);
        //    if (panel.ContainsPoint(MousePosition))
        //    {
        //        Main.LocalPlayer.mouseInterface = true;
        //    }
        //    if (dragging)
        //    {
        //        panel.Left.Set(MousePosition.X - offset.X, 0f);
        //        panel.Top.Set(MousePosition.Y - offset.Y, 0f);
        //        Recalculate();
        //    }
        //}
        ////
        ///
      
    }
    public class 用户界面系统 : ModSystem
    {
        internal 我的用户界面 我的用户界面实例;
        internal UserInterface userInterface;
        public override void Load()
        {
            我的用户界面实例 = new 我的用户界面();
            我的用户界面实例.Activate();
            userInterface = new UserInterface();
            userInterface.SetState(我的用户界面实例);
            base.Load();
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (我的用户界面.可见)
                userInterface?.Update(gameTime);
            base.UpdateUI(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (MouseTextIndex != -1)
            {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                   "爆枪英雄:我的用户界面",
                   delegate
                   {
                       if (我的用户界面.可见)
                       {
                           我的用户界面实例.Draw(Main.spriteBatch);
                       }
                       return true;
                   },
                   InterfaceScaleType.UI)
               );
            }
            base.ModifyInterfaceLayers(layers);
        }
    }
    public class 用户控制 : ModPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (爆枪英雄.BaoQiangUI.JustPressed)
            {
                我的用户界面.可见 = true;
            }
            base.ProcessTriggers(triggersSet);
        }
    }
}
