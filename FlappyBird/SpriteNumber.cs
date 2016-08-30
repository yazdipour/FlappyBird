﻿using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    public class SpriteNumber
    {
        private const float DigitSpace = 2f;

        private readonly Sprite[] digits;
        private readonly Sprite period;

        private int value;

        public SpriteNumber(IEnumerable<Sprite> digits, Sprite period = null)
        {
            this.digits = digits.ToArray();
            this.period = period;

            DigitWidth = this.digits.Max(d => d.Size.Width);
            DigitHeight = this.digits.Max(d => d.Size.Height);
            Value = 0;
        }

        public int Value
        {
            get { return value; }
            set
            {
                this.value = Math.Max(0, value);

                var text = this.value.ToString("0");
                var sprites = new Sprite[text.Length];
                for (int i = 0; i < sprites.Length; i++)
                {
                    var number = int.Parse(text[i].ToString());
                    sprites[i] = digits[number];
                }
                DigitSprites = sprites;
            }
        }

        public float DigitWidth { get; private set; }

        public float DigitHeight { get; private set; }

        public float Width => DigitSprites.Length * (DigitWidth + DigitSpace);

        public float Height => DigitHeight;

        public Sprite[] DigitSprites { get; private set; }

        public void Draw(SKCanvas canvas, float x, float y)
        {
            var offset = 0f;
            foreach (var sprite in DigitSprites)
            {
                var left = x + offset + (DigitWidth - sprite.Size.Width);
                var top = y + (DigitHeight - sprite.Size.Height);
                sprite.Draw(canvas, left, top);
                offset += DigitWidth + DigitSpace;
            }
        }
    }
}
