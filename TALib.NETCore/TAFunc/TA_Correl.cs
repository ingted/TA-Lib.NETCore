using System;

namespace TALib
{
    public partial class Core
    {
        public static RetCode Correl(int startIdx, int endIdx, double[] inReal0, double[] inReal1, ref int outBegIdx, ref int outNBElement,
            double[] outReal, int optInTimePeriod = 30)
        {
            if (startIdx < 0 || endIdx < 0 || endIdx < startIdx)
            {
                return RetCode.OutOfRangeStartIndex;
            }

            if (inReal0 == null || inReal1 == null || outReal == null || optInTimePeriod < 1 || optInTimePeriod > 100000)
            {
                return RetCode.BadParam;
            }

            int lookbackTotal = optInTimePeriod - 1;
            if (startIdx < lookbackTotal)
            {
                startIdx = lookbackTotal;
            }

            if (startIdx > endIdx)
            {
                outBegIdx = 0;
                outNBElement = 0;
                return RetCode.Success;
            }

            outBegIdx = startIdx;
            int trailingIdx = startIdx - lookbackTotal;

            double sumX, sumY, sumX2, sumY2;
            double sumXY = sumX = sumY = sumX2 = sumY2 = default;
            int today;
            for (today = trailingIdx; today <= startIdx; today++)
            {
                double x = inReal0[today];
                sumX += x;
                sumX2 += x * x;

                double y = inReal1[today];
                sumXY += x * y;
                sumY += y;
                sumY2 += y * y;
            }

            double trailingX = inReal0[trailingIdx];
            double trailingY = inReal1[trailingIdx++];
            double tempReal = (sumX2 - sumX * sumX / optInTimePeriod) * (sumY2 - sumY * sumY / optInTimePeriod);
            if (!TA_IsZeroOrNeg(tempReal))
            {
                outReal[0] = (sumXY - sumX * sumY / optInTimePeriod) / Math.Sqrt(tempReal);
            }
            else
            {
                outReal[0] = 0.0;
            }

            int outIdx = 1;
            while (today <= endIdx)
            {
                sumX -= trailingX;
                sumX2 -= trailingX * trailingX;

                sumXY -= trailingX * trailingY;
                sumY -= trailingY;
                sumY2 -= trailingY * trailingY;

                double x = inReal0[today];
                sumX += x;
                sumX2 += x * x;

                double y = inReal1[today++];
                sumXY += x * y;
                sumY += y;
                sumY2 += y * y;

                trailingX = inReal0[trailingIdx];
                trailingY = inReal1[trailingIdx++];
                tempReal = (sumX2 - sumX * sumX / optInTimePeriod) * (sumY2 - sumY * sumY / optInTimePeriod);
                if (!TA_IsZeroOrNeg(tempReal))
                {
                    outReal[outIdx++] = (sumXY - sumX * sumY / optInTimePeriod) / Math.Sqrt(tempReal);
                }
                else
                {
                    outReal[outIdx++] = 0.0;
                }
            }

            outNBElement = outIdx;

            return RetCode.Success;
        }

        public static RetCode Correl(int startIdx, int endIdx, decimal[] inReal0, decimal[] inReal1, ref int outBegIdx,
            ref int outNBElement, decimal[] outReal, int optInTimePeriod = 30)
        {
            if (startIdx < 0 || endIdx < 0 || endIdx < startIdx)
            {
                return RetCode.OutOfRangeStartIndex;
            }

            if (inReal0 == null || inReal1 == null || outReal == null || optInTimePeriod < 1 || optInTimePeriod > 100000)
            {
                return RetCode.BadParam;
            }

            int lookbackTotal = optInTimePeriod - 1;
            if (startIdx < lookbackTotal)
            {
                startIdx = lookbackTotal;
            }

            if (startIdx > endIdx)
            {
                outBegIdx = 0;
                outNBElement = 0;
                return RetCode.Success;
            }

            outBegIdx = startIdx;
            int trailingIdx = startIdx - lookbackTotal;

            decimal sumX, sumY, sumX2, sumY2;
            decimal sumXY = sumX = sumY = sumX2 = sumY2 = default;
            int today;
            for (today = trailingIdx; today <= startIdx; today++)
            {
                decimal x = inReal0[today];
                sumX += x;
                sumX2 += x * x;

                decimal y = inReal1[today];
                sumXY += x * y;
                sumY += y;
                sumY2 += y * y;
            }

            decimal trailingX = inReal0[trailingIdx];
            decimal trailingY = inReal1[trailingIdx++];
            decimal tempReal = (sumX2 - sumX * sumX / optInTimePeriod) * (sumY2 - sumY * sumY / optInTimePeriod);
            if (!TA_IsZeroOrNeg(tempReal))
            {
                outReal[0] = (sumXY - sumX * sumY / optInTimePeriod) / DecimalMath.Sqrt(tempReal);
            }
            else
            {
                outReal[0] = Decimal.Zero;
            }

            int outIdx = 1;
            while (today <= endIdx)
            {
                sumX -= trailingX;
                sumX2 -= trailingX * trailingX;

                sumXY -= trailingX * trailingY;
                sumY -= trailingY;
                sumY2 -= trailingY * trailingY;

                decimal x = inReal0[today];
                sumX += x;
                sumX2 += x * x;

                decimal y = inReal1[today++];
                sumXY += x * y;
                sumY += y;
                sumY2 += y * y;

                trailingX = inReal0[trailingIdx];
                trailingY = inReal1[trailingIdx++];
                tempReal = (sumX2 - sumX * sumX / optInTimePeriod) * (sumY2 - sumY * sumY / optInTimePeriod);
                if (!TA_IsZeroOrNeg(tempReal))
                {
                    outReal[outIdx++] = (sumXY - sumX * sumY / optInTimePeriod) / DecimalMath.Sqrt(tempReal);
                }
                else
                {
                    outReal[outIdx++] = Decimal.Zero;
                }
            }

            outNBElement = outIdx;

            return RetCode.Success;
        }

        public static int CorrelLookback(int optInTimePeriod = 30)
        {
            if (optInTimePeriod < 1 || optInTimePeriod > 100000)
            {
                return -1;
            }

            return optInTimePeriod - 1;
        }
    }
}
