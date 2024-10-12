using Nancy;

namespace DENMAP_SERVER.Modules
{
    public class CorsMiddleware : NancyModule
    {
        public CorsMiddleware()
        {
            After.AddItemToEndOfPipeline(AddCorsHeaders);
        }

        private void AddCorsHeaders(NancyContext context)
        {
            context.Response.WithHeader("Access-Control-Allow-Origin", "*")
                            .WithHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS")
                            .WithHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
            
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.StatusCode = HttpStatusCode.OK;
            }
        }
    }

}
