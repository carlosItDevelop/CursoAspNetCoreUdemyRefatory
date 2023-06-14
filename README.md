### Repositório Oficial do Projeto MedicalManagement-Sys - Refatory

### Curso de Asp.Net Core - Do Zero ao Ninja

---

__Quer conhecer nosso projeto? Acesse o curso na Udemy:  **[Acesse aqui](https://www.udemy.com/course/curso-de-aspnet-core-mvc-2-2-do-zero-ao-ninja/?referralCode=41B345D11D74CEDD7E57)**.__



![Tela Inicial do Projeto MedicalManagenet-Sys](https://user-images.githubusercontent.com/1213751/71663844-87052780-2d35-11ea-95c0-623a74885ebc.png "Antes do Dashboard")


> __Validação global contra Ataque CSRF, prevenindo-se da ausências nas Actions;__

---


```CSHARP
using Cooperchip.ITDeveloper.Mvc.Extentions.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Cooperchip.ITDeveloper.Mvc.Configurations
{
    public static class MvcAndRazorConfig
    {
        public static IServiceCollection AddMvcAndRazor(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(AuditoriaIloggerFilter));
                
                // Todo: Passar na aula esta Validação global contra CSRF;
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

            }); //.SetCompatibilityVersion(CompatibilityVersion.Version_3_1);  (OUT in the version)

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();

            return services;
        }
    }
}
```