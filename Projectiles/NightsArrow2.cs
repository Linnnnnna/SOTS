using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace SOTS.Projectiles 
{    
    public class NightsArrow2 : ModProjectile 
    {	
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night Swipe");
			
		}
		
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 14; 
            projectile.timeLeft = 1000;
            projectile.penetrate = 1; 
            projectile.friendly = true; 
            projectile.hostile = false; 
            projectile.tileCollide = true;;
            projectile.ignoreWater = true; 
            projectile.ranged = true; 
            projectile.aiStyle = 1; //18 is the demon scythe style
			projectile.alpha = 100;
		}
		public override void Kill(int timeLeft)
		{	
				if(projectile.owner == Main.myPlayer)
				{
					Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 10, 10,  521, (int)(projectile.damage * .15), projectile.knockBack, Main.myPlayer);
					Projectile.NewProjectile(projectile.position.X, projectile.position.Y, -10, -10,  521, (int)(projectile.damage * .15), projectile.knockBack, Main.myPlayer);
					Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 10, -10,  521, (int)(projectile.damage * .15), projectile.knockBack, Main.myPlayer);
					Projectile.NewProjectile(projectile.position.X, projectile.position.Y, -10, 10,  521, (int)(projectile.damage * .15), projectile.knockBack, Main.myPlayer);
					Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 10,  521, (int)(projectile.damage * .15), projectile.knockBack, Main.myPlayer);
					Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, -10,  521, (int)(projectile.damage * .15), projectile.knockBack, Main.myPlayer);
					Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 10, 0,  521, (int)(projectile.damage * .15), projectile.knockBack, Main.myPlayer);
					Projectile.NewProjectile(projectile.position.X, projectile.position.Y, -10, 0,  521, (int)(projectile.damage * .15), projectile.knockBack, Main.myPlayer);
					  
			  }
			
		}
		
	}
	
}