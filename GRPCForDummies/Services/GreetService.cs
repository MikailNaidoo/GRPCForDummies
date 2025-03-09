using Grpc.Core;

namespace GRPCForDummies.Services
{
    public class GreetService : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = $"Hello, {request.Name}!"
            });
        }
    }
}
