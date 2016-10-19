using System;
using System.Text.RegularExpressions;
using CoreGraphics;

namespace NathansWay.iOS.Numeracy
{
	public static class HelperFunctions
	{

		public static CGRect StringToRectF(string _strRect)
		{
			// x, y, width, height
			var strArray = _strRect.Split (',');
			CGRect tmpRect = 
				new CGRect (
					nfloat.Parse (strArray [0]),
					nfloat.Parse (strArray [1]),
					nfloat.Parse (strArray [2]),
					nfloat.Parse (strArray [3])
				);
			return tmpRect;

		}

		public static string RectFToString(CGRect _rctString)
		{
			//Convert a RectF to Text (csv)
			string tmpStrRect = 
				_rctString.X.ToString () + ',' +
				_rctString.Y.ToString () + ',' +
				_rctString.Width.ToString () + ',' +
				_rctString.Height.ToString ();
			return tmpStrRect;

		}

        public static Fraction RealToFraction(double value, double error)
        {
            if (error <= 0.0 || error >= 1.0)
            {
                throw new ArgumentOutOfRangeException("error", "Must be between 0 and 1 (exclusive).");
            }

            int sign = Math.Sign(value);

            if (sign == -1)
            {
                value = Math.Abs(value);
            }

            if (sign != 0)
            {
                // error is the maximum relative error; convert to absolute
                error *= value;
            }

            int n = (int)Math.Floor(value);
            value -= n;

            if (value < error)
            {
                return new Fraction(sign * n, 1);
            }

            if (1 - error < value)
            {
                return new Fraction(sign * (n + 1), 1);
            }

            // The lower fraction is 0/1
            int lower_n = 0;
            int lower_d = 1;

            // The upper fraction is 1/1
            int upper_n = 1;
            int upper_d = 1;

            while (true)
            {
                // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
                int middle_n = lower_n + upper_n;
                int middle_d = lower_d + upper_d;

                if (middle_d * (value + error) < middle_n)
                {
                    // real + error < middle : middle is our new upper
                    upper_n = middle_n;
                    upper_d = middle_d;
                }
                else if (middle_n < (value - error) * middle_d)
                {
                    // middle < real - error : middle is our new lower
                    lower_n = middle_n;
                    lower_d = middle_d;
                }
                else
                {
                    // Middle is our best fraction
                    return new Fraction((n * middle_d + middle_n) * sign, middle_d);
                }
            }
        }

        public struct Fraction
        {
            public Fraction(int n, int d)
            {
                N = n;
                D = d;
            }

            public int N { get; private set; }
            public int D { get; private set; }
        }

        public static string DoubleToFraction(double num, double epsilon = 0.0001, int maxIterations = 20)
        {
            double[] d = new double[maxIterations + 2];
            d[1] = 1;
            double z = num;
            double n = 1;
            int t = 1;

            int wholeNumberPart = (int)num;
            double decimalNumberPart = num - Convert.ToDouble(wholeNumberPart);

            while (t < maxIterations && Math.Abs(n / d[t] - num) > epsilon)
            {
                t++;
                z = 1 / (z - (int)z);
                d[t] = d[t - 1] * (int)z + d[t - 2];
                n = (int)(decimalNumberPart * d[t] + 0.5);
            }

            return string.Format((wholeNumberPart > 0 ? wholeNumberPart.ToString() + " " : "") + "{0}/{1}",
                                 n.ToString(),
                                 d[t].ToString()
                                );
        }

        //public static void DecimalToFraction(decimal value, ref decimal sign, ref decimal numerator, ref decimal denominator)
        //{
        //    const decimal maxValue = decimal.MaxValue / 10.0M;

        //    // e.g. .25/1 = (.25 * 100)/(1 * 100) = 25/100 = 1/4
        //    var tmpSign = value < decimal.Zero ? -1 : 1;
        //    var tmpNumerator = Math.Abs(value);
        //    var tmpDenominator = decimal.One;

        //    // While numerator has a decimal value
        //    while ((tmpNumerator - Math.Truncate(tmpNumerator)) > 0 &&
        //        tmpNumerator < maxValue && tmpDenominator < maxValue)
        //    {
        //        tmpNumerator = tmpNumerator * 10;
        //        tmpDenominator = tmpDenominator * 10;
        //    }

        //    tmpNumerator = Math.Truncate(tmpNumerator); // Just in case maxValue boundary was reached.
        //    //ReduceFraction(ref tmpNumerator, ref tmpDenominator);
        //    sign = tmpSign;
        //    numerator = tmpNumerator;
        //    denominator = tmpDenominator;
        //}

        //public static string DecimalToFraction(decimal value)
        //{
        //    var sign = decimal.One;
        //    var numerator = decimal.One;
        //    var denominator = decimal.One;
        //    DecimalToFraction(value, ref sign, ref numerator, ref denominator);
        //    decimal d = Convert.ToDecimal(sign * numerator);
        //    return string.Format("{0}/{1}", Math.Truncate(d).ToString(),
        //                         Math.Truncate(denominator).ToString());
        //}

        static readonly Regex FractionalExpression = new Regex(@"^(?<sign>[-])?(?<numerator>\d+)(/(?<denominator>\d+))?$");

        public static decimal? FractionToDecimal(string fraction)
        {
            var match = FractionalExpression.Match(fraction);
            if (match.Success)
            {
                // var sign = Int32.Parse(match.Groups["sign"].Value + "1");
                var numerator = Int32.Parse(match.Groups["sign"].Value + match.Groups["numerator"].Value);
                int denominator;
                if (Int32.TryParse(match.Groups["denominator"].Value, out denominator))
                    return denominator == 0 ? (decimal?)null : (decimal)numerator / denominator;
                if (numerator == 0 || numerator == 1)
                    return numerator;
            }
            return null;
        }

    }
}

