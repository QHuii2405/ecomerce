using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi;
using System.Text.Json.Serialization;
using Infrastructure.BackgroundServices;
using Domain.Interfaces;
using Infrastructure.Persistence.Repositories;
using Application.Interfaces;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);
// 1. Đăng ký Database Context với SQL Server [cite: 49]
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký Unit of Work và Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IPaymentGatewayService, PaymentGatewayService>();
builder.Services.AddHttpClient<IChatService, ChatService>();

// 2. Đăng ký các dịch vụ Controller để hỗ trợ Web API 
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Lệnh này giúp trình Serializer bỏ qua các vòng lặp vô tận
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// 3. Cấu hình Swagger/Scalar để xem tài liệu API (thay cho AddOpenApi mặc định) [cite: 49]
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce API", Version = "v1" });

    // Định nghĩa cách thức bảo mật (Bearer Token)
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Nhập Token theo định dạng: Bearer {your_token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Yêu cầu Swagger sử dụng định nghĩa bảo mật trên cho các API
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", document, null),
            new List<string>()
        }
    });
});
// Đăng ký Background Service để tự động hủy đơn hàng quá hạn [cite: 88]
builder.Services.AddHostedService<OrderExpirationService>();

// Lấy thông tin từ appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);

// JWT Authentication: Cấu hình để bảo vệ API bằng token [cite: 54, 109]
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(); // Kích hoạt phân quyền

// Cấu hình Redis Cache để lưu trữ dữ liệu tạm thời, như thông tin phiên đăng nhập hoặc dữ liệu tạm thời khác [cite: 88]
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "Ecommerce_";
});

// Cấu hình CORS để cho phép frontend (React) truy cập API [cite: 49]
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174", "http://localhost:5175") // Hỗ trợ nhiều port phòng khi 5173 bị kẹt
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 4. Cấu hình Pipeline xử lý Request
app.UseCors("AllowReactApp");

app.UseAuthentication(); // Phải đặt TRƯỚC UseAuthorization
app.UseAuthorization();
// 4. Cấu hình Pipeline xử lý Request
if (app.Environment.IsDevelopment())
{
    // Bật giao diện Swagger để bạn có thể test API trên trình duyệt [cite: 49]
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 5. Quan trọng: Map các Controller vào hệ thống [cite: 129, 131]
app.MapControllers();

app.Run();
