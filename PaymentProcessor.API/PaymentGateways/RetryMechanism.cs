using System;

namespace PaymentProcessor.API.PaymentGateways
{
    public class RetryMechanism
    {
        public static bool RetryWithAlternate(Action methodToRetry, Action onFailure)
        {
            try
            {
                methodToRetry();
                return true;
            }
            catch (Exception)
            {
                try
                {
                    onFailure();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool RetryWithSame(Action methodToRetry,int retryCount=3)
        {
            try
            {
                methodToRetry();
                return true;
            }
            catch(Exception)
            {
                if (retryCount-1 > 0)
                {
                    return RetryWithSame(methodToRetry, --retryCount);
                }
                else
                    return false;
            }
        }
    }
}
