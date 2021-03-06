using System;

namespace TALib
{
    public partial class Core
    {
        public static RetCode Mfi(int startIdx, int endIdx, double[] inHigh, double[] inLow, double[] inClose, double[] inVolume,
            ref int outBegIdx, ref int outNBElement, double[] outReal, int optInTimePeriod = 14)
        {
            if (startIdx < 0 || endIdx < 0 || endIdx < startIdx)
            {
                return RetCode.OutOfRangeStartIndex;
            }

            if (inHigh == null || inLow == null || inClose == null || inVolume == null || outReal == null || optInTimePeriod < 2 || optInTimePeriod > 100000)
            {
                return RetCode.BadParam;
            }

            outBegIdx = 0;
            outNBElement = 0;
            int lookbackTotal = optInTimePeriod + (int) Globals.UnstablePeriod[(int) FuncUnstId.Mfi];
            if (startIdx < lookbackTotal)
            {
                startIdx = lookbackTotal;
            }

            if (startIdx > endIdx)
            {
                return RetCode.Success;
            }

            int outIdx = default;

            var moneyFlow = new (double negative, double positive)[optInTimePeriod];

            int mflowIdx = default;
            var maxIdxMflow = optInTimePeriod - 1;

            int today = startIdx - lookbackTotal;
            double prevValue = (inHigh[today] + inLow[today] + inClose[today]) / 3.0;

            double posSumMF = default;
            double negSumMF = default;
            today++;
            for (int i = optInTimePeriod; i > 0; i--)
            {
                double tempValue1 = (inHigh[today] + inLow[today] + inClose[today]) / 3.0;
                double tempValue2 = tempValue1 - prevValue;
                prevValue = tempValue1;
                tempValue1 *= inVolume[today++];
                if (tempValue2 < 0.0)
                {
                    moneyFlow[mflowIdx].negative = tempValue1;
                    negSumMF += tempValue1;
                    moneyFlow[mflowIdx].positive = 0.0;
                }
                else if (tempValue2 > 0.0)
                {
                    moneyFlow[mflowIdx].positive = tempValue1;
                    posSumMF += tempValue1;
                    moneyFlow[mflowIdx].negative = 0.0;
                }
                else
                {
                    moneyFlow[mflowIdx].positive = 0.0;
                    moneyFlow[mflowIdx].negative = 0.0;
                }

                if (++mflowIdx > maxIdxMflow)
                {
                    mflowIdx = 0;
                }
            }

            if (today > startIdx)
            {
                double tempValue1 = posSumMF + negSumMF;
                if (tempValue1 < 1.0)
                {
                    outReal[outIdx++] = 0.0;
                }
                else
                {
                    outReal[outIdx++] = 100.0 * (posSumMF / tempValue1);
                }
            }
            else
            {
                while (today < startIdx)
                {
                    posSumMF -= moneyFlow[mflowIdx].positive;
                    negSumMF -= moneyFlow[mflowIdx].negative;
                    double tempValue1 = (inHigh[today] + inLow[today] + inClose[today]) / 3.0;
                    double tempValue2 = tempValue1 - prevValue;
                    prevValue = tempValue1;
                    tempValue1 *= inVolume[today++];
                    if (tempValue2 < 0.0)
                    {
                        moneyFlow[mflowIdx].negative = tempValue1;
                        negSumMF += tempValue1;
                        moneyFlow[mflowIdx].positive = 0.0;
                    }
                    else if (tempValue2 > 0.0)
                    {
                        moneyFlow[mflowIdx].positive = tempValue1;
                        posSumMF += tempValue1;
                        moneyFlow[mflowIdx].negative = 0.0;
                    }
                    else
                    {
                        moneyFlow[mflowIdx].positive = 0.0;
                        moneyFlow[mflowIdx].negative = 0.0;
                    }

                    if (++mflowIdx > maxIdxMflow)
                    {
                        mflowIdx = 0;
                    }
                }
            }

            while (today <= endIdx)
            {
                posSumMF -= moneyFlow[mflowIdx].positive;
                negSumMF -= moneyFlow[mflowIdx].negative;
                double tempValue1 = (inHigh[today] + inLow[today] + inClose[today]) / 3.0;
                double tempValue2 = tempValue1 - prevValue;
                prevValue = tempValue1;
                tempValue1 *= inVolume[today++];
                if (tempValue2 < 0.0)
                {
                    moneyFlow[mflowIdx].negative = tempValue1;
                    negSumMF += tempValue1;
                    moneyFlow[mflowIdx].positive = 0.0;
                }
                else if (tempValue2 > 0.0)
                {
                    moneyFlow[mflowIdx].positive = tempValue1;
                    posSumMF += tempValue1;
                    moneyFlow[mflowIdx].negative = 0.0;
                }
                else
                {
                    moneyFlow[mflowIdx].positive = 0.0;
                    moneyFlow[mflowIdx].negative = 0.0;
                }

                tempValue1 = posSumMF + negSumMF;
                if (tempValue1 < 1.0)
                {
                    outReal[outIdx++] = 0.0;
                }
                else
                {
                    outReal[outIdx++] = 100.0 * (posSumMF / tempValue1);
                }

                if (++mflowIdx > maxIdxMflow)
                {
                    mflowIdx = 0;
                }
            }

            outBegIdx = startIdx;
            outNBElement = outIdx;

            return RetCode.Success;
        }

        public static RetCode Mfi(int startIdx, int endIdx, decimal[] inHigh, decimal[] inLow, decimal[] inClose, decimal[] inVolume,
            ref int outBegIdx, ref int outNBElement, decimal[] outReal, int optInTimePeriod = 14)
        {
            if (startIdx < 0 || endIdx < 0 || endIdx < startIdx)
            {
                return RetCode.OutOfRangeStartIndex;
            }

            if (inHigh == null || inLow == null || inClose == null || inVolume == null || outReal == null || optInTimePeriod < 2 || optInTimePeriod > 100000)
            {
                return RetCode.BadParam;
            }

            outBegIdx = 0;
            outNBElement = 0;
            int lookbackTotal = optInTimePeriod + (int) Globals.UnstablePeriod[(int) FuncUnstId.Mfi];
            if (startIdx < lookbackTotal)
            {
                startIdx = lookbackTotal;
            }

            if (startIdx > endIdx)
            {
                return RetCode.Success;
            }

            int outIdx = default;

            var moneyFlow = new (decimal negative, decimal positive)[optInTimePeriod];

            int mflowIdx = default;
            var maxIdxMflow = optInTimePeriod - 1;

            int today = startIdx - lookbackTotal;
            decimal prevValue = (inHigh[today] + inLow[today] + inClose[today]) / 3m;

            decimal posSumMF = default;
            decimal negSumMF = default;
            today++;
            for (int i = optInTimePeriod; i > 0; i--)
            {
                decimal tempValue1 = (inHigh[today] + inLow[today] + inClose[today]) / 3m;
                decimal tempValue2 = tempValue1 - prevValue;
                prevValue = tempValue1;
                tempValue1 *= inVolume[today++];
                if (tempValue2 < Decimal.Zero)
                {
                    moneyFlow[mflowIdx].negative = tempValue1;
                    negSumMF += tempValue1;
                    moneyFlow[mflowIdx].positive = Decimal.Zero;
                }
                else if (tempValue2 > Decimal.Zero)
                {
                    moneyFlow[mflowIdx].positive = tempValue1;
                    posSumMF += tempValue1;
                    moneyFlow[mflowIdx].negative = Decimal.Zero;
                }
                else
                {
                    moneyFlow[mflowIdx].positive = Decimal.Zero;
                    moneyFlow[mflowIdx].negative = Decimal.Zero;
                }

                if (++mflowIdx > maxIdxMflow)
                {
                    mflowIdx = 0;
                }
            }

            if (today > startIdx)
            {
                decimal tempValue1 = posSumMF + negSumMF;
                if (tempValue1 < Decimal.One)
                {
                    outReal[outIdx++] = Decimal.Zero;
                }
                else
                {
                    outReal[outIdx++] = 100m * (posSumMF / tempValue1);
                }
            }
            else
            {
                while (today < startIdx)
                {
                    posSumMF -= moneyFlow[mflowIdx].positive;
                    negSumMF -= moneyFlow[mflowIdx].negative;
                    decimal tempValue1 = (inHigh[today] + inLow[today] + inClose[today]) / 3m;
                    decimal tempValue2 = tempValue1 - prevValue;
                    prevValue = tempValue1;
                    tempValue1 *= inVolume[today++];
                    if (tempValue2 < Decimal.Zero)
                    {
                        moneyFlow[mflowIdx].negative = tempValue1;
                        negSumMF += tempValue1;
                        moneyFlow[mflowIdx].positive = Decimal.Zero;
                    }
                    else if (tempValue2 > Decimal.Zero)
                    {
                        moneyFlow[mflowIdx].positive = tempValue1;
                        posSumMF += tempValue1;
                        moneyFlow[mflowIdx].negative = Decimal.Zero;
                    }
                    else
                    {
                        moneyFlow[mflowIdx].positive = Decimal.Zero;
                        moneyFlow[mflowIdx].negative = Decimal.Zero;
                    }

                    if (++mflowIdx > maxIdxMflow)
                    {
                        mflowIdx = 0;
                    }
                }
            }

            while (today <= endIdx)
            {
                posSumMF -= moneyFlow[mflowIdx].positive;
                negSumMF -= moneyFlow[mflowIdx].negative;
                decimal tempValue1 = (inHigh[today] + inLow[today] + inClose[today]) / 3m;
                decimal tempValue2 = tempValue1 - prevValue;
                prevValue = tempValue1;
                tempValue1 *= inVolume[today++];
                if (tempValue2 < Decimal.Zero)
                {
                    moneyFlow[mflowIdx].negative = tempValue1;
                    negSumMF += tempValue1;
                    moneyFlow[mflowIdx].positive = Decimal.Zero;
                }
                else if (tempValue2 > Decimal.Zero)
                {
                    moneyFlow[mflowIdx].positive = tempValue1;
                    posSumMF += tempValue1;
                    moneyFlow[mflowIdx].negative = Decimal.Zero;
                }
                else
                {
                    moneyFlow[mflowIdx].positive = Decimal.Zero;
                    moneyFlow[mflowIdx].negative = Decimal.Zero;
                }

                tempValue1 = posSumMF + negSumMF;
                if (tempValue1 < Decimal.One)
                {
                    outReal[outIdx++] = Decimal.Zero;
                }
                else
                {
                    outReal[outIdx++] = 100m * (posSumMF / tempValue1);
                }

                if (++mflowIdx > maxIdxMflow)
                {
                    mflowIdx = 0;
                }
            }

            outBegIdx = startIdx;
            outNBElement = outIdx;

            return RetCode.Success;
        }

        public static int MfiLookback(int optInTimePeriod = 14)
        {
            if (optInTimePeriod < 2 || optInTimePeriod > 100000)
            {
                return -1;
            }

            return optInTimePeriod + (int) Globals.UnstablePeriod[(int) FuncUnstId.Mfi];
        }
    }
}
