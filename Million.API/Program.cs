using AutoMapper;
using Million.Properties.Application.Mappings;
using Million.Properties.Infrastructure.Startup;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "OpenCorsPolicy",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddAutoMapper(cfg => { cfg.AddProfile<MappingProfile>(); }, AppDomain.CurrentDomain.GetAssemblies());


            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapHealthChecks("/health");
            }

            app.UseHttpsRedirection();
            app.UseCors("OpenCorsPolicy");
            app.UseAuthorization();
            app.MapControllers();
            //BsonDefaults. = GuidRepresentation.Standard;
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            using (var scope = app.Services.CreateScope())
            {
                var init = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
                await init.InitializeAsync();
            }
            app.Run();

        }
        catch (Exception ex)
        {
            Console.WriteLine("Application startup failed:");
            Console.WriteLine(ex.ToString());
            throw;
        }
    }
}
