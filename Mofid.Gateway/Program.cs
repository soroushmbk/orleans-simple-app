using Orleans.Clustering.Kubernetes;
using Orleans.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.UseOrleansClient(options =>
{
    //for localhost clustering
    // options.UseLocalhostClustering();
    
    options.UseKubeGatewayListProvider();
    options.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "mofid-oms-cluster";
        options.ServiceId = "mofid-oms";
    });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();
app.Run();
