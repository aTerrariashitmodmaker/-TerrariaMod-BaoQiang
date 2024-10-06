using Microsoft.Build.Execution;
using Microsoft.Xna.Framework.Graphics;
using SteelSeries.GameSense;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;
using 爆枪英雄.Items.载具;

namespace 爆枪英雄.Items.武器

{
    public class 超模刀 : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage =3;
            Item.DamageType = DamageClass.Generic;
            Item.width = 60;
            Item.height = 60;
            Item.useTime = 40;
            Item.useAnimation = 40;          
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.value = 10000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.crit = 12;
            Item.useTurn = true;
            Item.shoot = ProjectileType<超模刀本体弹幕>();
            Item.shootSpeed = 15f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<副手相关>().HoldIt = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return player.HasBuff<可识破>(); 
        }
        void modifyDamage()
        {
            if (NPC.downedSlimeKing)
            {
                Item.damage = 6;
            }
            if (NPC.downedBoss1)
            {
                Item.damage = 10;
            }
            if (NPC.downedBoss2)
            {
                Item.damage = 17;
            }
            if (NPC.downedQueenBee)
            {
                Item.damage = 25;
            }
            if (NPC.downedBoss3)
            {
                Item.damage = 28;
            }
            if (NPC.downedDeerclops)
            {
                Item.damage = 32;
            }
            if (Main.hardMode)
            {
                Item.damage = 35;
            }
            if (NPC.downedMechBossAny)
            {
                Item.damage =54;
            }           
            if (NPC.downedPlantBoss)
            {
                Item.damage = 68;
            }
            if (NPC.downedGolemBoss)
            {
                Item.damage = 75;
            }
            if (NPC.downedAncientCultist)
            {
                Item.damage = 100;
            }
            if (NPC.downedMoonlord)
            {
                Item.damage = 300;
            }
        }
        public override bool CanUseItem(Player player)
        {
            modifyDamage();
            if (player.altFunctionUse == 2)
            {                               
                player.AddBuff(BuffType<识破中>(), 90);
                return false;
            }
            if (player.HasBuff<识破成功剑气>()|| player.HasBuff<识破成功>())
            {
                return false;
            }
            if (player.GetModPlayer<副手相关>().rage >= 200)
            {
                player.GetModPlayer<副手相关>().rage-=200;
                return true;
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ProjectileType<超模刀剑气>(), damage/2, knockback, player.whoAmI);
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
   
}