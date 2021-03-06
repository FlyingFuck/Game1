﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roleplay
{
    public enum Facing { L, R, N }

    public class MagicTexture
    {
        Facing face;
        Texture2D source;
        Rectangle sourceRect;
        Rectangle frame;
        int frameCount, frameCounter;
        float frameTime, frameTimer;

        public MagicTexture(Texture2D source_, Rectangle sourceRect_, Facing face_)
        {
            face = face_;
            source = source_;
            sourceRect = sourceRect_;
        }
        public MagicTexture(Texture2D source_, Rectangle sourceRect_, Facing face_, int frameCount_, float frameTime_)
        {
            face = face_;
            source = source_;
            sourceRect = sourceRect_;
            frameCount = frameCount_;
            frameTime = frameTime_;
        }

        public void Update(GameTime gt_)
        {
            frameTimer -= (float)gt_.ElapsedGameTime.TotalSeconds;

            if (frameTimer <= 0) { frameTimer = frameTime; frameCounter++; }
            if (frameCounter >= frameCount) { frameCounter = 0; }
        }

        public void Draw(SpriteBatch sb_, Vector2 pos_)
        {
            frame = new Rectangle(sourceRect.X + frameCounter * sourceRect.Width, sourceRect.Y, sourceRect.Width, sourceRect.Height);
            sb_.Draw(source, sourceRectangle: frame, position: pos_);
        }
    }
}
