#pragma checksum "C:\Users\Proye\Desktop\ERacuni\Client\Shared\LoginTailwind.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c42b3ed2d7241b5860d1ab0b7d709aee0bb501fa"
// <auto-generated/>
#pragma warning disable 1591
namespace ERacuniNovi.Client.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Proye\Desktop\ERacuni\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Proye\Desktop\ERacuni\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Proye\Desktop\ERacuni\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Proye\Desktop\ERacuni\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Proye\Desktop\ERacuni\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Proye\Desktop\ERacuni\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Proye\Desktop\ERacuni\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Proye\Desktop\ERacuni\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Proye\Desktop\ERacuni\Client\_Imports.razor"
using ERacuniNovi.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Proye\Desktop\ERacuni\Client\_Imports.razor"
using ERacuniNovi.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\Proye\Desktop\ERacuni\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\Proye\Desktop\ERacuni\Client\_Imports.razor"
using Grpc;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\Proye\Desktop\ERacuni\Client\_Imports.razor"
using Grpc.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\Proye\Desktop\ERacuni\Client\_Imports.razor"
using MatBlazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Proye\Desktop\ERacuni\Client\Shared\LoginTailwind.razor"
using ERacuniNovi.Shared.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Proye\Desktop\ERacuni\Client\Shared\LoginTailwind.razor"
using ERacuniProtoNameSpace;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.LayoutAttribute(typeof(LogInLayout))]
    [Microsoft.AspNetCore.Components.RouteAttribute("/login")]
    public partial class LoginTailwind : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "w-full flex flex-wrap");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "w-full md:w-1/2 flex flex-col");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "flex flex-col justify-center md:justify-start my-auto pt-8 md:pt-0 px-8 md:px-24 lg:px-32");
            __builder.AddMarkupContent(6, "<p class=\"text-center text-3xl\">Dobro došli.</p>\r\n            ");
            __builder.OpenElement(7, "form");
            __builder.AddAttribute(8, "class", "flex flex-col pt-3 md:pt-8");
            __builder.AddAttribute(9, "onsubmit", "event.preventDefault();");
            __builder.OpenElement(10, "div");
            __builder.AddAttribute(11, "class", "flex flex-col pt-4");
            __builder.AddMarkupContent(12, "<label for=\"text\" class=\"text-lg\">Korisničko ime</label>\r\n                    ");
            __builder.OpenElement(13, "input");
            __builder.AddAttribute(14, "type", "text");
            __builder.AddAttribute(15, "id", "text");
            __builder.AddAttribute(16, "placeholder", "Vaše korisničko ime");
            __builder.AddAttribute(17, "class", "shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mt-1 leading-tight focus:outline-none focus:shadow-outline");
            __builder.AddAttribute(18, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 19 "C:\Users\Proye\Desktop\ERacuni\Client\Shared\LoginTailwind.razor"
                                                                                                                                                                                                                                         userLogIn.UserName

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(19, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => userLogIn.UserName = __value, userLogIn.UserName));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(20, "\r\n\r\n                ");
            __builder.OpenElement(21, "div");
            __builder.AddAttribute(22, "class", "flex flex-col pt-4");
            __builder.AddMarkupContent(23, "<label for=\"password\" class=\"text-lg\">Šifra</label>\r\n                    ");
            __builder.OpenElement(24, "input");
            __builder.AddAttribute(25, "type", "password");
            __builder.AddAttribute(26, "id", "password");
            __builder.AddAttribute(27, "placeholder", "Šifra");
            __builder.AddAttribute(28, "class", "shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mt-1 leading-tight focus:outline-none focus:shadow-outline");
            __builder.AddAttribute(29, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 24 "C:\Users\Proye\Desktop\ERacuni\Client\Shared\LoginTailwind.razor"
                                                                                                                                                                                                                                   passwordLogIn

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(30, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => passwordLogIn = __value, passwordLogIn));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(31, "\r\n\r\n                ");
            __builder.OpenElement(32, "input");
            __builder.AddAttribute(33, "type", "submit");
            __builder.AddAttribute(34, "value", "Log In");
            __builder.AddAttribute(35, "class", "bg-black text-white font-bold text-lg hover:bg-gray-700 p-2 mt-8");
            __builder.AddAttribute(36, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 27 "C:\Users\Proye\Desktop\ERacuni\Client\Shared\LoginTailwind.razor"
                                                                                                                                       LogIn

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 29 "C:\Users\Proye\Desktop\ERacuni\Client\Shared\LoginTailwind.razor"
             if (ShowRegistration)
            {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(37, "<div class=\"text-center pt-12 pb-12\"><p>Nemate nalog? <a href=\"/registration\" class=\"underline font-semibold\">Registrujte se ovde.</a></p></div>");
#nullable restore
#line 34 "C:\Users\Proye\Desktop\ERacuni\Client\Shared\LoginTailwind.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(38, "\r\n\r\n    \r\n    ");
            __builder.AddMarkupContent(39, "<div class=\"w-1/2 shadow-2xl\"><img class=\"object-cover w-full h-screen hidden md:block\" src=\"images\\login_screen.jpg\"></div>");
            __builder.CloseElement();
            __builder.AddMarkupContent(40, "\r\n\r\n");
            __builder.OpenComponent<MatBlazor.MatSnackbar>(41);
            __builder.AddAttribute(42, "Timeout", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 46 "C:\Users\Proye\Desktop\ERacuni\Client\Shared\LoginTailwind.razor"
                                                     3000

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(43, "IsOpen", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 46 "C:\Users\Proye\Desktop\ERacuni\Client\Shared\LoginTailwind.razor"
                            snackBarIsOpen

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(44, "IsOpenChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Boolean>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Boolean>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => snackBarIsOpen = __value, snackBarIsOpen))));
            __builder.AddAttribute(45, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<MatBlazor.MatSnackbarContent>(46);
                __builder2.AddAttribute(47, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(48, "<b>Pogrešno korisničko ime ili password</b>");
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 50 "C:\Users\Proye\Desktop\ERacuni\Client\Shared\LoginTailwind.razor"
       
    private User userLogIn = new User();
    private string passwordLogIn = string.Empty;
    private string porukaZaPrikaz = string.Empty;
    bool snackBarIsOpen;
    bool ShowRegistration;


    protected override async Task OnInitializedAsync()
    {
        var response = await ProtoServis.ProveriKorisnikaAsync(new EmptyMsg());
        ShowRegistration = response.Success;
        StateHasChanged();

    }

    private async void LogIn()
    {
        RegistrationMsg logInMsg = new RegistrationMsg();
        userLogIn.UserName ??= string.Empty;
        passwordLogIn ??= string.Empty;
        logInMsg.Username = userLogIn.UserName;
        logInMsg.Password = passwordLogIn;
        logInMsg.Login = true;
        var Response = await ProtoServis.LogInAsync(logInMsg);
        porukaZaPrikaz = Response.Error;
        if (Response.Success)
        {
            _navigationManager.NavigateTo("/posta", true);
        }
        else
        {
            snackBarIsOpen = true;
        }
        StateHasChanged();
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ERacuniProtoNameSpace.ERacuniProtoServis.ERacuniProtoServisClient ProtoServis { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager _navigationManager { get; set; }
    }
}
#pragma warning restore 1591
