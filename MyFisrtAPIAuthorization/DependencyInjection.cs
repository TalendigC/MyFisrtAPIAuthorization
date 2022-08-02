using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Authentication;

namespace MyFisrtAPIAuthorization
{
    public static class DependencyInjection
    {
        public static IServiceCollection CustomAuthentication(this IServiceCollection services)
        {
            string key = "1234651321safdf12342345";

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSingleton<MyFisrtAPIAuthorization.Services.IAutheticationService>
                (new MyFisrtAPIAuthorization.Services.AuthenticationService(key));

            return services;
        }
    }
}
