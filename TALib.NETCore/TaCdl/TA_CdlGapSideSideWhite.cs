using System;

namespace TALib
{
    public partial class Core
    {
        public static RetCode CdlGapSideSideWhite(int startIdx, int endIdx, double[] inOpen, double[] inHigh, double[] inLow,
            double[] inClose, ref int outBegIdx, ref int outNBElement, int[] outInteger)
        {
            if (startIdx < 0 || endIdx < 0 || endIdx < startIdx)
            {
                return RetCode.OutOfRangeStartIndex;
            }

            if (inOpen == null || inHigh == null || inLow == null || inClose == null || outInteger == null)
            {
                return RetCode.BadParam;
            }

            int lookbackTotal = CdlGapSideSideWhiteLookback();
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

            double nearPeriodTotal = default;
            double equalPeriodTotal = default;
            int nearTrailingIdx = startIdx - TA_CandleAvgPeriod(CandleSettingType.Near);
            int equalTrailingIdx = startIdx - TA_CandleAvgPeriod(CandleSettingType.Equal);
            int i = nearTrailingIdx;
            while (i < startIdx)
            {
                nearPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.Near, i - 1);
                i++;
            }

            i = equalTrailingIdx;
            while (i < startIdx)
            {
                equalPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.Equal, i - 1);
                i++;
            }

            i = startIdx;

            int outIdx = default;
            do
            {
                if (( // upside or downside gap between the 1st candle and both the next 2 candles
                        TA_RealBodyGapUp(inOpen, inClose, i - 1, i - 2) && TA_RealBodyGapUp(inOpen, inClose, i, i - 2)
                        ||
                        TA_RealBodyGapDown(inOpen, inClose, i - 1, i - 2) && TA_RealBodyGapDown(inOpen, inClose, i, i - 2)
                    ) &&
                    TA_CandleColor(inClose, inOpen, i - 1) && // 2nd: white
                    TA_CandleColor(inClose, inOpen, i) && // 3rd: white
                    TA_RealBody(inClose, inOpen, i) >= TA_RealBody(inClose, inOpen, i - 1) -
                    TA_CandleAverage(inOpen, inHigh, inLow, inClose, CandleSettingType.Near, nearPeriodTotal, i - 1) && // same size 2 and 3
                    TA_RealBody(inClose, inOpen, i) <= TA_RealBody(inClose, inOpen, i - 1) +
                    TA_CandleAverage(inOpen, inHigh, inLow, inClose, CandleSettingType.Near, nearPeriodTotal, i - 1) &&
                    inOpen[i] >= inOpen[i - 1] -
                    TA_CandleAverage(inOpen, inHigh, inLow, inClose, CandleSettingType.Equal, equalPeriodTotal,
                        i - 1) && // same open 2 and 3
                    inOpen[i] <= inOpen[i - 1] +
                    TA_CandleAverage(inOpen, inHigh, inLow, inClose, CandleSettingType.Equal, equalPeriodTotal, i - 1))
                {
                    outInteger[outIdx++] = TA_RealBodyGapUp(inOpen, inClose, i - 1, i - 2) ? 100 : -100;
                }
                else
                {
                    outInteger[outIdx++] = 0;
                }

                /* add the current range and subtract the first range: this is done after the pattern recognition
                 * when avgPeriod is not 0, that means "compare with the previous candles" (it excludes the current candle)
                 */
                nearPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.Near, i - 1) -
                                   TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.Near, nearTrailingIdx - 1);
                equalPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.Equal, i - 1) -
                                    TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.Equal, equalTrailingIdx - 1);
                i++;
                nearTrailingIdx++;
                equalTrailingIdx++;
            } while (i <= endIdx);

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }

        public static RetCode CdlGapSideSideWhite(int startIdx, int endIdx, decimal[] inOpen, decimal[] inHigh, decimal[] inLow,
            decimal[] inClose, ref int outBegIdx, ref int outNBElement, int[] outInteger)
        {
            if (startIdx < 0 || endIdx < 0 || endIdx < startIdx)
            {
                return RetCode.OutOfRangeStartIndex;
            }

            if (inOpen == null || inHigh == null || inLow == null || inClose == null || outInteger == null)
            {
                return RetCode.BadParam;
            }

            int lookbackTotal = CdlGapSideSideWhiteLookback();
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

            decimal nearPeriodTotal = default;
            decimal equalPeriodTotal = default;
            int nearTrailingIdx = startIdx - TA_CandleAvgPeriod(CandleSettingType.Near);
            int equalTrailingIdx = startIdx - TA_CandleAvgPeriod(CandleSettingType.Equal);
            int i = nearTrailingIdx;
            while (i < startIdx)
            {
                nearPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.Near, i - 1);
                i++;
            }

            i = equalTrailingIdx;
            while (i < startIdx)
            {
                equalPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.Equal, i - 1);
                i++;
            }

            i = startIdx;

            int outIdx = default;
            do
            {
                if (( // upside or downside gap between the 1st candle and both the next 2 candles
                        TA_RealBodyGapUp(inOpen, inClose, i - 1, i - 2) && TA_RealBodyGapUp(inOpen, inClose, i, i - 2)
                        ||
                        TA_RealBodyGapDown(inOpen, inClose, i - 1, i - 2) && TA_RealBodyGapDown(inOpen, inClose, i, i - 2)
                    ) &&
                    TA_CandleColor(inClose, inOpen, i - 1) && // 2nd: white
                    TA_CandleColor(inClose, inOpen, i) && // 3rd: white
                    TA_RealBody(inClose, inOpen, i) >= TA_RealBody(inClose, inOpen, i - 1) -
                    TA_CandleAverage(inOpen, inHigh, inLow, inClose, CandleSettingType.Near, nearPeriodTotal, i - 1) && // same size 2 and 3
                    TA_RealBody(inClose, inOpen, i) <= TA_RealBody(inClose, inOpen, i - 1) +
                    TA_CandleAverage(inOpen, inHigh, inLow, inClose, CandleSettingType.Near, nearPeriodTotal, i - 1) &&
                    inOpen[i] >= inOpen[i - 1] -
                    TA_CandleAverage(inOpen, inHigh, inLow, inClose, CandleSettingType.Equal, equalPeriodTotal,
                        i - 1) && // same open 2 and 3
                    inOpen[i] <= inOpen[i - 1] +
                    TA_CandleAverage(inOpen, inHigh, inLow, inClose, CandleSettingType.Equal, equalPeriodTotal, i - 1))
                {
                    outInteger[outIdx++] = TA_RealBodyGapUp(inOpen, inClose, i - 1, i - 2) ? 100 : -100;
                }
                else
                {
                    outInteger[outIdx++] = 0;
                }

                /* add the current range and subtract the first range: this is done after the pattern recognition
                 * when avgPeriod is not 0, that means "compare with the previous candles" (it excludes the current candle)
                 */
                nearPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.Near, i - 1) -
                                   TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.Near, nearTrailingIdx - 1);
                equalPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.Equal, i - 1) -
                                    TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.Equal, equalTrailingIdx - 1);
                i++;
                nearTrailingIdx++;
                equalTrailingIdx++;
            } while (i <= endIdx);

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }

        public static int CdlGapSideSideWhiteLookback()
        {
            return Math.Max(TA_CandleAvgPeriod(CandleSettingType.Near), TA_CandleAvgPeriod(CandleSettingType.Equal)) + 2;
        }
    }
}
