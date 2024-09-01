namespace AssetsManagementSystem.Others.Middlewares.ExceptionMiddleware
{
    public static class ExceptionsHandleConfiguration
    {
        public static void ExceptionHandleConfiguration(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
