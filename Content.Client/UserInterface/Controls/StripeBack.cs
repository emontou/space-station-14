using System.Numerics;
using Robust.Client.Graphics;
using Robust.Client.UserInterface.Controls;

namespace Content.Client.UserInterface.Controls
{
    [Virtual]
    public class StripeBack : Container
    {
        public float PadSize { get; set; } = 4;
        public float EdgeSize { get; set; } = 2;
        public Color EdgeColor { get; set; } = Color.FromHex("#525252ff");

        private bool _hasTopEdge = true;
        private bool _hasBottomEdge = true;
        private bool _hasMargins = true;

        public const string StylePropertyBackground = "background";

        public bool HasTopEdge
        {
            get => _hasTopEdge;
            set
            {
                _hasTopEdge = value;
                InvalidateMeasure();
            }
        }

        public bool HasBottomEdge
        {
            get => _hasBottomEdge;
            set
            {
                _hasBottomEdge = value;
                InvalidateMeasure();
            }
        }

        public bool HasMargins
        {
            get => _hasMargins;
            set
            {
                _hasMargins = value;
                InvalidateMeasure();
            }
        }

        protected override Vector2 MeasureOverride(Vector2 availableSize)
        {
            var padSize = HasMargins ? PadSize : 0;
            var padSizeTotal = 0f;

            if (HasBottomEdge)
                padSizeTotal += padSize + EdgeSize;
            if (HasTopEdge)
                padSizeTotal += padSize + EdgeSize;

            var size = Vector2.Zero;

            availableSize.Y -= padSizeTotal;

            foreach (var child in Children)
            {
                child.Measure(availableSize);
                size = Vector2.Max(size, child.DesiredSize);
            }

            return size + new Vector2(0, padSizeTotal);
        }

        protected override Vector2 ArrangeOverride(Vector2 finalSize)
        {
            var box = new UIBox2(Vector2.Zero, finalSize);

            var padSize = HasMargins ? PadSize : 0;

            if (HasTopEdge)
            {
                box += (0, padSize + EdgeSize, 0, 0);
            }

            if (HasBottomEdge)
            {
                box += (0, 0, 0, -(padSize + EdgeSize));
            }

            foreach (var child in Children)
            {
                child.Arrange(box);
            }

            return finalSize;
        }


        protected override void Draw(DrawingHandleScreen handle)
        {
            UIBox2 centerBox = PixelSizeBox;

            var padSize = HasMargins ? PadSize : 0;

            if (HasTopEdge)
            {
                centerBox += (0, (padSize + EdgeSize) * UIScale, 0, 0);
                handle.DrawRect(new UIBox2(0, padSize * UIScale, PixelWidth, centerBox.Top), EdgeColor);
            }

            if (HasBottomEdge)
            {
                centerBox += (0, 0, 0, -((padSize + EdgeSize) * UIScale));
                handle.DrawRect(new UIBox2(0, centerBox.Bottom, PixelWidth, PixelHeight - padSize * UIScale),
                    EdgeColor);
            }

            GetActualStyleBox()?.Draw(handle, centerBox, UIScale);
        }

        private StyleBox? GetActualStyleBox()
        {
            return TryGetStyleProperty(StylePropertyBackground, out StyleBox? box) ? box : null;
        }
    }
}
