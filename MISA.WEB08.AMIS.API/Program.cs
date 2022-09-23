using MISA.WEB08.AMIS.CORE.Interfaces.Infrastructure;
using MISA.WEB08.AMIS.CORE.Interfaces.Services;
using MISA.WEB08.AMIS.CORE.Services;
using MISA.WEB08.AMIS.INFRASTRUCTURE.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Xử lý Dependences Invert theo Nguyên Tắc SOLID
// Bằng các Dependences Inject
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped(typeof(IBaseServices<>), typeof(BaseServices<>));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
