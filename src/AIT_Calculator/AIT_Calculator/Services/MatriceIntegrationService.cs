namespace AIT_Calculator.Services
{
    public static class MatriceIntegrationService
    {
        public static double[] Integrate(double[] values, double step, double initialValue = 0)
        {
            if (values == null || values.Length == 0)
                return [];

            double[] integral = new double[values.Length];
            integral[0] = initialValue;


            for (int i = 1; i < values.Length; i++)
            {
                double trapezoidArea = (values[i] + values[i - 1]) * step / 2;
                integral[i] = integral[i - 1] + trapezoidArea;
            }

            return integral;
        }
    }
}
