using System;
using System.Globalization;

namespace NathansWay.Shared
{
	public struct RectangleF
	{
		private float height;
		private float width;
		private float x;
		private float y;


		//
		// Static Fields
		//
		public static readonly RectangleF Empty;

		//
		// Properties
		//
		public float Bottom
		{
			get
			{
				return this.Y + this.Height;
			}
		}

		public float Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return this.Width <= 0 || this.Height <= 0;
			}
		}

		public float Left
		{
			get
			{
				return this.X;
			}
		}

		public PointF Location
		{
			get
			{
				return new PointF(this.X, this.Y);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
			}
		}

		public float Right
		{
			get
			{
				return this.X + this.Width;
			}
		}

		public SizeF Size
		{
			get
			{
				return new SizeF(this.Width, this.Height);
			}
			set
			{
				this.Width = value.Width;
				this.Height = value.Height;
			}
		}

		public float Top
		{
			get
			{
				return this.Y;
			}
		}

		public float Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		//
		// Constructors
		//
		public RectangleF(PointF location, SizeF size)
		{
			this = default(RectangleF);
			this.X = location.X;
			this.Y = location.Y;
			this.Width = size.Width;
			this.Height = size.Height;
		}

		public RectangleF(float x, float y, float width, float height)
		{
			this = default(RectangleF);
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		//
		// Static Methods
		//
		public static RectangleF FromLTRB(float left, float top, float right, float bottom)
		{
			return new RectangleF(left, top, right - left, bottom - top);
		}

		public static RectangleF Inflate(RectangleF rect, float x, float y)
		{
			RectangleF result = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
			result.Inflate(x, y);
			return result;
		}

		public static RectangleF Intersect(RectangleF a, RectangleF b)
		{
			if (!a.IntersectsWithInclusive(b))
			{
				return RectangleF.Empty;
			}
			return RectangleF.FromLTRB(Math.Max(a.Left, b.Left), Math.Max(a.Top, b.Top), Math.Min(a.Right, b.Right), Math.Min(a.Bottom, b.Bottom));
		}

		public static RectangleF Union(RectangleF a, RectangleF b)
		{
			return RectangleF.FromLTRB(Math.Min(a.Left, b.Left), Math.Min(a.Top, b.Top), Math.Max(a.Right, b.Right), Math.Max(a.Bottom, b.Bottom));
		}

		//
		// Methods
		//
		public bool Contains(float x, float y)
		{
			return x >= this.Left && x < this.Right && y >= this.Top && y < this.Bottom;
		}

		public bool Contains(PointF pt)
		{
			return this.Contains(pt.X, pt.Y);
		}

		public bool Contains(RectangleF rect)
		{
			return this.x <= rect.x && this.Right >= rect.Right && this.y <= rect.y && this.Bottom >= rect.Bottom;
		}

		public override bool Equals(object obj)
		{
			return obj is RectangleF && this == (RectangleF)obj;
		}

		public override int GetHashCode()
		{
			return (int)(this.X + this.Y + this.Width + this.Height);
		}

		public void Inflate(float x, float y)
		{
			this.Inflate(new SizeF(x, y));
		}

		public void Inflate(SizeF size)
		{
			this.X -= size.Width;
			this.Y -= size.Height;
			this.Width += size.Width * 2;
			this.Height += size.Height * 2;
		}

		public void Intersect(RectangleF rect)
		{
			this = RectangleF.Intersect(this, rect);
		}

		public bool IntersectsWith(RectangleF rect)
		{
			return this.Left < rect.Right && this.Right > rect.Left && this.Top < rect.Bottom && this.Bottom > rect.Top;
		}

		private bool IntersectsWithInclusive(RectangleF r)
		{
			return this.Left <= r.Right && this.Right >= r.Left && this.Top <= r.Bottom && this.Bottom >= r.Top;
		}

		public void Offset(PointF pos)
		{
			this.Offset(pos.X, pos.Y);
		}

		public void Offset(float x, float y)
		{
			this.X += x;
			this.Y += y;
		}

		public override string ToString()
		{
			return string.Format("{{X={0},Y={1},Width={2},Height={3}}}", new object[]
			{
				this.X,
				this.Y,
				this.Width,
				this.Height
			});
		}

		//
		// Operators
		//
		public static bool operator ==(RectangleF left, RectangleF right)
		{
			return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
		}

		public static implicit operator RectangleF(Rectangle r)
		{
			return new RectangleF((float)r.X, (float)r.Y, (float)r.Width, (float)r.Height);
		}

		public static bool operator !=(RectangleF left, RectangleF right)
		{
			return left.X != right.X || left.Y != right.Y || left.Width != right.Width || left.Height != right.Height;
		}
	}

	public struct Rectangle
	{
		private int height;
		private int width;
		private int x;
		private int y;
		//
		// Static Fields
		//
		public static readonly Rectangle Empty;

		//
		// Properties
		//
		public int Bottom
		{
			get
			{
				return this.Y + this.Height;
			}
		}

		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return this.X == 0 && this.Y == 0 && this.Width == 0 && this.Height == 0;
			}
		}

		public int Left
		{
			get
			{
				return this.X;
			}
		}

		public Point Location
		{
			get
			{
				return new Point(this.X, this.Y);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
			}
		}

		public int Right
		{
			get
			{
				return this.X + this.Width;
			}
		}

		public Size Size
		{
			get
			{
				return new Size(this.Width, this.Height);
			}
			set
			{
				this.Width = value.Width;
				this.Height = value.Height;
			}
		}

		public int Top
		{
			get
			{
				return this.Y;
			}
		}

		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		//
		// Constructors
		//
		public Rectangle(int x, int y, int width, int height)
		{
			this = default(Rectangle);
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		public Rectangle(Point location, Size size)
		{
			this = default(Rectangle);
			this.X = location.X;
			this.Y = location.Y;
			this.Width = size.Width;
			this.Height = size.Height;
		}

		//
		// Static Methods
		//
		public static Rectangle Ceiling(RectangleF value)
		{
			checked
			{
				int num = (int)Math.Ceiling((double)value.X);
				int num2 = (int)Math.Ceiling((double)value.Y);
				int num3 = (int)Math.Ceiling((double)value.Width);
				int num4 = (int)Math.Ceiling((double)value.Height);
				return new Rectangle(num, num2, num3, num4);
			}
		}

		public static Rectangle FromLTRB(int left, int top, int right, int bottom)
		{
			return new Rectangle(left, top, right - left, bottom - top);
		}

		public static Rectangle Inflate(Rectangle rect, int x, int y)
		{
			Rectangle result = new Rectangle(rect.Location, rect.Size);
			result.Inflate(x, y);
			return result;
		}

		public static Rectangle Intersect(Rectangle a, Rectangle b)
		{
			if (!a.IntersectsWithInclusive(b))
			{
				return Rectangle.Empty;
			}
			return Rectangle.FromLTRB(Math.Max(a.Left, b.Left), Math.Max(a.Top, b.Top), Math.Min(a.Right, b.Right), Math.Min(a.Bottom, b.Bottom));
		}

		public static Rectangle Round(RectangleF value)
		{
			checked
			{
				int num = (int)Math.Round((double)value.X);
				int num2 = (int)Math.Round((double)value.Y);
				int num3 = (int)Math.Round((double)value.Width);
				int num4 = (int)Math.Round((double)value.Height);
				return new Rectangle(num, num2, num3, num4);
			}
		}

		public static Rectangle Truncate(RectangleF value)
		{
			checked
			{
				int num = (int)value.X;
				int num2 = (int)value.Y;
				int num3 = (int)value.Width;
				int num4 = (int)value.Height;
				return new Rectangle(num, num2, num3, num4);
			}
		}

		public static Rectangle Union(Rectangle a, Rectangle b)
		{
			return Rectangle.FromLTRB(Math.Min(a.Left, b.Left), Math.Min(a.Top, b.Top), Math.Max(a.Right, b.Right), Math.Max(a.Bottom, b.Bottom));
		}

		//
		// Methods
		//
		public bool Contains(Point pt)
		{
			return this.Contains(pt.X, pt.Y);
		}

		public bool Contains(Rectangle rect)
		{
			return rect == Rectangle.Intersect(this, rect);
		}

		public bool Contains(int x, int y)
		{
			return x >= this.Left && x < this.Right && y >= this.Top && y < this.Bottom;
		}

		public override bool Equals(object obj)
		{
			return obj is Rectangle && this == (Rectangle)obj;
		}

		public override int GetHashCode()
		{
			return this.Height + this.Width ^ this.X + this.Y;
		}

		public void Inflate(int width, int height)
		{
			this.Inflate(new Size(width, height));
		}

		public void Inflate(Size size)
		{
			this.X -= size.Width;
			this.Y -= size.Height;
			this.Width += size.Width * 2;
			this.Height += size.Height * 2;
		}

		public void Intersect(Rectangle rect)
		{
			this = Rectangle.Intersect(this, rect);
		}

		public bool IntersectsWith(Rectangle rect)
		{
			return this.Left < rect.Right && this.Right > rect.Left && this.Top < rect.Bottom && this.Bottom > rect.Top;
		}

		private bool IntersectsWithInclusive(Rectangle r)
		{
			return this.Left <= r.Right && this.Right >= r.Left && this.Top <= r.Bottom && this.Bottom >= r.Top;
		}

		public void Offset(int x, int y)
		{
			this.X += x;
			this.Y += y;
		}

		public void Offset(Point pos)
		{
			this.X += pos.X;
			this.Y += pos.Y;
		}

		public override string ToString()
		{
			return string.Format("{{X={0},Y={1},Width={2},Height={3}}}", new object[]
			{
				this.X,
				this.Y,
				this.Width,
				this.Height
			});
		}

		//
		// Operators
		//
		public static bool operator ==(Rectangle left, Rectangle right)
		{
			return left.Location == right.Location && left.Size == right.Size;
		}

		public static bool operator !=(Rectangle left, Rectangle right)
		{
			return left.Location != right.Location || left.Size != right.Size;
		}
	}

	public struct SizeF
	{
		private float height;
		private float width;
		//
		// Static Fields
		//
		public static readonly SizeF Empty;

		//
		// Properties
		//
		public float Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return (double)this.Width == 0 && (double)this.Height == 0;
			}
		}

		public float Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		//
		// Constructors
		//
		public SizeF(float width, float height)
		{
			this = default(SizeF);
			this.Width = width;
			this.Height = height;
		}

		public SizeF(SizeF size)
		{
			this = default(SizeF);
			this.Width = size.Width;
			this.Height = size.Height;
		}

		public SizeF(PointF pt)
		{
			this = default(SizeF);
			this.Width = pt.X;
			this.Height = pt.Y;
		}

		//
		// Static Methods
		//
		public static SizeF Add(SizeF sz1, SizeF sz2)
		{
			return new SizeF(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		}

		public static SizeF Subtract(SizeF sz1, SizeF sz2)
		{
			return new SizeF(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		}

		//
		// Methods
		//
		public override bool Equals(object obj)
		{
			return obj is SizeF && this == (SizeF)obj;
		}

		public override int GetHashCode()
		{
			return (int)this.Width ^ (int)this.Height;
		}

		public PointF ToPointF()
		{
			return new PointF(this.Width, this.Height);
		}

		public Size ToSize()
		{
			checked
			{
				int num = (int)this.Width;
				int num2 = (int)this.Height;
				return new Size(num, num2);
			}
		}

		public override string ToString()
		{
			return string.Format("{{Width={0}, Height={1}}}", this.Width.ToString(CultureInfo.CurrentCulture), this.Height.ToString(CultureInfo.CurrentCulture));
		}

		//
		// Operators
		//
		public static SizeF operator +(SizeF sz1, SizeF sz2)
		{
			return new SizeF(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		}

		public static bool operator ==(SizeF sz1, SizeF sz2)
		{
			return sz1.Width == sz2.Width && sz1.Height == sz2.Height;
		}

		public static explicit operator PointF(SizeF size)
		{
			return new PointF(size.Width, size.Height);
		}

		public static bool operator !=(SizeF sz1, SizeF sz2)
		{
			return sz1.Width != sz2.Width || sz1.Height != sz2.Height;
		}

		public static SizeF operator -(SizeF sz1, SizeF sz2)
		{
			return new SizeF(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		}
		}

	public struct Size
	{
		private int height;
		private int width;
		//
		// Static Fields
		//
		public static readonly Size Empty;

		//
		// Properties
		//
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return this.Width == 0 && this.Height == 0;
			}
		}

		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		//
		// Constructors
		//
		public Size(int width, int height)
		{
			this = default(Size);
			this.Width = width;
			this.Height = height;
		}

		public Size(Point pt)
		{
			this = default(Size);
			this.Width = pt.X;
			this.Height = pt.Y;
		}

		//
		// Static Methods
		//
		public static Size Add(Size sz1, Size sz2)
		{
			return new Size(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		}

		public static Size Ceiling(SizeF value)
		{
			checked
			{
				int num = (int)Math.Ceiling((double)value.Width);
				int num2 = (int)Math.Ceiling((double)value.Height);
				return new Size(num, num2);
			}
		}

		public static Size Round(SizeF value)
		{
			checked
			{
				int num = (int)Math.Round((double)value.Width);
				int num2 = (int)Math.Round((double)value.Height);
				return new Size(num, num2);
			}
		}

		public static Size Subtract(Size sz1, Size sz2)
		{
			return new Size(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		}

		public static Size Truncate(SizeF value)
		{
			checked
			{
				int num = (int)value.Width;
				int num2 = (int)value.Height;
				return new Size(num, num2);
			}
		}

		//
		// Methods
		//
		public override bool Equals(object obj)
		{
			return obj is Size && this == (Size)obj;
		}

		public override int GetHashCode()
		{
			return this.Width ^ this.Height;
		}

		public override string ToString()
		{
			return string.Format("{{Width={0}, Height={1}}}", this.Width, this.Height);
		}

		//
		// Operators
		//
		public static Size operator +(Size sz1, Size sz2)
		{
			return new Size(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		}

		public static bool operator ==(Size sz1, Size sz2)
		{
			return sz1.Width == sz2.Width && sz1.Height == sz2.Height;
		}

		public static explicit operator Point(Size size)
		{
			return new Point(size.Width, size.Height);
		}

		public static implicit operator SizeF(Size p)
		{
			return new SizeF((float)p.Width, (float)p.Height);
		}

		public static bool operator !=(Size sz1, Size sz2)
		{
			return sz1.Width != sz2.Width || sz1.Height != sz2.Height;
		}

		public static Size operator -(Size sz1, Size sz2)
		{
			return new Size(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		}
	}

	public struct PointF
	{
		private float x;
		private float y;
		//
		// Static Fields
		//
		public static readonly PointF Empty;

		//
		// Properties
		//
		public bool IsEmpty
		{
			get
			{
				return (double)this.X == 0 && (double)this.Y == 0;
			}
		}

		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		//
		// Constructors
		//
		public PointF(float x, float y)
		{
			this = default(PointF);
			this.X = x;
			this.Y = y;
		}

		//
		// Static Methods
		//
		public static PointF Add(PointF pt, SizeF sz)
		{
			return new PointF(pt.X + sz.Width, pt.Y + sz.Height);
		}

		public static PointF Add(PointF pt, Size sz)
		{
			return new PointF(pt.X + (float)sz.Width, pt.Y + (float)sz.Height);
		}

		public static PointF Subtract(PointF pt, Size sz)
		{
			return new PointF(pt.X - (float)sz.Width, pt.Y - (float)sz.Height);
		}

		public static PointF Subtract(PointF pt, SizeF sz)
		{
			return new PointF(pt.X - sz.Width, pt.Y - sz.Height);
		}

		//
		// Methods
		//
		public override bool Equals(object obj)
		{
			return obj is PointF && this == (PointF)obj;
		}

		public override int GetHashCode()
		{
			return (int)this.X ^ (int)this.Y;
		}

		public override string ToString()
		{
			return string.Format("{{X={0}, Y={1}}}", this.X.ToString(CultureInfo.CurrentCulture), this.Y.ToString(CultureInfo.CurrentCulture));
		}

		//
		// Operators
		//
		public static PointF operator +(PointF pt, SizeF sz)
		{
			return new PointF(pt.X + sz.Width, pt.Y + sz.Height);
		}

		public static PointF operator +(PointF pt, Size sz)
		{
			return new PointF(pt.X + (float)sz.Width, pt.Y + (float)sz.Height);
		}

		public static bool operator ==(PointF left, PointF right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		public static bool operator !=(PointF left, PointF right)
		{
			return left.X != right.X || left.Y != right.Y;
		}

		public static PointF operator -(PointF pt, Size sz)
		{
			return new PointF(pt.X - (float)sz.Width, pt.Y - (float)sz.Height);
		}

		public static PointF operator -(PointF pt, SizeF sz)
		{
			return new PointF(pt.X - sz.Width, pt.Y - sz.Height);
		}
	}

	public struct Point
	{
		private int x;
		private int y;
		//
		// Static Fields
		//
		public static readonly Point Empty;

		//
		// Properties
		//
		public bool IsEmpty
		{
			get
			{
				return this.X == 0 && this.Y == 0;
			}
		}

		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		//
		// Constructors
		//
		public Point(int x, int y)
		{
			this = default(Point);
			this.X = x;
			this.Y = y;
		}

		public Point(Size sz)
		{
			this = default(Point);
			this.X = sz.Width;
			this.Y = sz.Height;
		}

		public Point(int dw)
		{
			this = default(Point);
			this.X = dw >> 16;
			this.Y = (dw & 65535);
		}

		//
		// Static Methods
		//
		public static Point Add(Point pt, Size sz)
		{
			return new Point(pt.X + sz.Width, pt.Y + sz.Height);
		}

		public static Point Ceiling(PointF value)
		{
			checked
			{
				int num = (int)Math.Ceiling((double)value.X);
				int num2 = (int)Math.Ceiling((double)value.Y);
				return new Point(num, num2);
			}
		}

		public static Point Round(PointF value)
		{
			checked
			{
				int num = (int)Math.Round((double)value.X);
				int num2 = (int)Math.Round((double)value.Y);
				return new Point(num, num2);
			}
		}

		public static Point Subtract(Point pt, Size sz)
		{
			return new Point(pt.X - sz.Width, pt.Y - sz.Height);
		}

		public static Point Truncate(PointF value)
		{
			checked
			{
				int num = (int)value.X;
				int num2 = (int)value.Y;
				return new Point(num, num2);
			}
		}

		//
		// Methods
		//
		public override bool Equals(object obj)
		{
			return obj is Point && this == (Point)obj;
		}

		public override int GetHashCode()
		{
			return this.X ^ this.Y;
		}

		public void Offset(Point p)
		{
			this.Offset(p.X, p.Y);
		}

		public void Offset(int dx, int dy)
		{
			this.X += dx;
			this.Y += dy;
		}

		public override string ToString()
		{
			return string.Format("{{X={0},Y={1}}}", this.X.ToString(CultureInfo.InvariantCulture), this.Y.ToString(CultureInfo.InvariantCulture));
		}

		//
		// Operators
		//
		public static Point operator +(Point pt, Size sz)
		{
			return new Point(pt.X + sz.Width, pt.Y + sz.Height);
		}

		public static bool operator ==(Point left, Point right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		public static explicit operator Size(Point p)
		{
			return new Size(p.X, p.Y);
		}

		public static implicit operator PointF(Point p)
		{
			return new PointF((float)p.X, (float)p.Y);
		}

		public static bool operator !=(Point left, Point right)
		{
			return left.X != right.X || left.Y != right.Y;
		}

		public static Point operator -(Point pt, Size sz)
		{
			return new Point(pt.X - sz.Width, pt.Y - sz.Height);
		}
	}
}

