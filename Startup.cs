using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using app_authen.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace app_authen
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // 1. Đọc key, issuer và audience từ appsettings.json
            var key = Configuration["Jwt:Key"];           // Khóa bí mật để ký token
            var issuer = Configuration["Jwt:Issuer"];     // Issuer (bên phát hành token)
            var audience = Configuration["Jwt:Audience"]; // Audience (người nhận token)

            // 2. Cấu hình Authentication sử dụng JWT Bearer
            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Xác thực key bí mật của token
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

                        // Xác thực Issuer
                        ValidateIssuer = true,
                        ValidIssuer = issuer, // Phải khớp với Issuer trong token

                        // Xác thực Audience
                        ValidateAudience = true,
                        ValidAudience = audience, // Phải khớp với Audience trong token

                        // Xác thực thời gian hết hạn của token
                        ValidateLifetime = true,

                        // Bỏ qua độ trễ thời gian giữa server và client (ngăn lỗi thời gian)
                        ClockSkew = TimeSpan.Zero,
                        RoleClaimType = "role", // Ánh xạ claim role,
                        NameClaimType = "unique_name",


                    };

                    // **Bổ sung thêm xử lý lỗi xác thực**
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("Authentication failed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("Token validated successfully.");
                            return Task.CompletedTask;
                        }
                    };
                });

            // 3. Cấu hình Authorization (Phân quyền theo Role)
            services.AddAuthorization(options =>
            {
                // Chính sách chỉ cho phép Admin truy cập
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                // Chính sách chỉ cho phép User truy cập
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            });


            services.AddScoped<ProductStoreService, ProductStoreService>();

            // 4. Đăng ký các dịch vụ cho Blazor Server
            services.AddServerSideBlazor(); // Hỗ trợ ứng dụng Blazor Server

            // 5. Đăng ký AuthenticationStateProvider tùy chỉnh
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            // 6. Thêm AuthorizationCore để sử dụng trong Blazor Components
            services.AddAuthorizationCore();

            // 7. Đăng ký dịch vụ xử lý JWT tùy chỉnh
            services.AddScoped<JwtAuthService>(sp => new JwtAuthService(key)); // Xử lý tạo/giải mã token

            // 8. Đăng ký Blazored LocalStorage để lưu trữ token
            services.AddBlazoredLocalStorage();

            // 9. Đăng ký Razor Pages (hỗ trợ thêm các trang Razor ngoài Blazor)
            services.AddRazorPages();

            // 10. Đăng ký một dịch vụ ví dụ
            services.AddSingleton<WeatherForecastService>(); // Dịch vụ mẫu cho Blazor

            services.AddHttpClient("apisv", client =>
            {
                client.BaseAddress = new Uri("https://svcy.myclass.vn");
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            });


            // Bật logging vào console và debug
            services.AddLogging(builder =>
            {
                builder.AddConsole();  // Log ra console
                builder.AddDebug();    // Log ra cửa sổ debug
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Bật xác thực và phân quyền
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
