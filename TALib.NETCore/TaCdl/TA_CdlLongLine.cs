using System;

namespace TALib
{
    public partial class Core
    {
        public static RetCode CdlLongLine(int startIdx, int endIdx, double[] inOpen, double[] inHigh, double[] inLow, double[] inClose,
            ref int outBegIdx, ref int outNBElement, int[] outInteger)
        {
            if (startIdx < 0 || endIdx < 0 || endIdx < startIdx)
            {
                return RetCode.OutOfRangeStartIndex;
            }

            if (inOpen == null || inHigh == null || inLow == null || inClose == null || outInteger == null)
            {
                return RetCode.BadParam;
            }

            int lookbackTotal = CdlLongLineLookback();
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

            double bodyPeriodTotal = default;
            int bodyTrailingIdx = startIdx - TA_CandleAvgPeriod(CandleSettingType.BodyLong);
            double shadowPeriodTotal = default;
            int shadowTrailingIdx = startIdx - TA_CandleAvgPeriod(CandleSettingType.ShadowShort);
            int i = bodyTrailingIdx;
            while (i < startIdx)
            {
                bodyPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.BodyLong, i);
                i++;
            }

            i = shadowTrailingIdx;
            while (i < startIdx)
            {
                shadowPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.ShadowShort, i);
                i++;
            }

            int outIdx = default;
            do
            {
                if (TA_RealBody(inClose, inOpen, i) >
                    TA_CandleAverage(inOpen, inHigh, inLow, inClose, CandleSettingType.BodyLong, bodyPeriodTotal, i) &&
                    TA_UpperShadow(inHigh, inClose, inOpen, i) < TA_CandleAverage(inOpen, inHigh, inLow, inClose,
                        CandleSettingType.ShadowShort, shadowPeriodTotal, i) &&
                    TA_LowerShadow(inClose, inOpen, inLow, i) < TA_CandleAverage(inOpen, inHigh, inLow, inClose,
                        CandleSettingType.ShadowShort, shadowPeriodTotal, i))
                {
                    outInteger[outIdx++] = Convert.ToInt32(TA_CandleColor(inClose, inOpen, i)) * 100;
                }
                else
                {
                    outInteger[outIdx++] = 0;
                }

                /* add the current range and subtract the first range: this is done after the pattern recognition
                 * when avgPeriod is not 0, that means "compare with the previous candles" (it excludes the current candle)
                 */
                bodyPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.BodyLong, i) -
                                   TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.BodyLong, bodyTrailingIdx);
                shadowPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.ShadowShort, i) -
                                     TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.ShadowShort, shadowTrailingIdx);
                i++;
                bodyTrailingIdx++;
                shadowTrailingIdx++;
            } while (i <= endIdx);

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }

        public static RetCode CdlLongLine(int startIdx, int endIdx, decimal[] inOpen, decimal[] inHigh, decimal[] inLow, decimal[] inClose,
            ref int outBegIdx, ref int outNBElement, int[] outInteger)
        {
            if (startIdx < 0 || endIdx < 0 || endIdx < startIdx)
            {
                return RetCode.OutOfRangeStartIndex;
            }

            if (inOpen == null || inHigh == null || inLow == null || inClose == null || outInteger == null)
            {
                return RetCode.BadParam;
            }

            int lookbackTotal = CdlLongLineLookback();
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

            decimal bodyPeriodTotal = default;
            int bodyTrailingIdx = startIdx - TA_CandleAvgPeriod(CandleSettingType.BodyLong);
            decimal shadowPeriodTotal = default;
            int shadowTrailingIdx = startIdx - TA_CandleAvgPeriod(CandleSettingType.ShadowShort);
            int i = bodyTrailingIdx;
            while (i < startIdx)
            {
                bodyPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.BodyLong, i);
                i++;
            }

            i = shadowTrailingIdx;
            while (i < startIdx)
            {
                shadowPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.ShadowShort, i);
                i++;
            }

            int outIdx = default;
            do
            {
                if (TA_RealBody(inClose, inOpen, i) >
                    TA_CandleAverage(inOpen, inHigh, inLow, inClose, CandleSettingType.BodyLong, bodyPeriodTotal, i) &&
                    TA_UpperShadow(inHigh, inClose, inOpen, i) < TA_CandleAverage(inOpen, inHigh, inLow, inClose,
                        CandleSettingType.ShadowShort, shadowPeriodTotal, i) &&
                    TA_LowerShadow(inClose, inOpen, inLow, i) < TA_CandleAverage(inOpen, inHigh, inLow, inClose,
                        CandleSettingType.ShadowShort, shadowPeriodTotal, i))
                {
                    outInteger[outIdx++] = Convert.ToInt32(TA_CandleColor(inClose, inOpen, i)) * 100;
                }
                else
                {
                    outInteger[outIdx++] = 0;
                }

                /* add the current range and subtract the first range: this is done after the pattern recognition
                 * when avgPeriod is not 0, that means "compare with the previous candles" (it excludes the current candle)
                 */
                bodyPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.BodyLong, i) -
                                   TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.BodyLong, bodyTrailingIdx);
                shadowPeriodTotal += TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.ShadowShort, i) -
                                     TA_CandleRange(inOpen, inHigh, inLow, inClose, CandleSettingType.ShadowShort, shadowTrailingIdx);
                i++;
                bodyTrailingIdx++;
                shadowTrailingIdx++;
            } while (i <= endIdx);

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }

        public static int CdlLongLineLookback()
        {
            return Math.Max(TA_CandleAvgPeriod(CandleSettingType.BodyLong), TA_CandleAvgPeriod(CandleSettingType.ShadowShort));
        }
    }
}
