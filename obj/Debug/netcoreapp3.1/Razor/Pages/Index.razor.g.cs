#pragma checksum "/Volumes/Cybersoft/authentication_blazor/app_authen/Pages/Index.razor" "{8829d00f-11b8-4213-878b-770e8597ac16}" "76ebed78f67c8a88967d524b89d43d743d96efab7f7da705833c4000a3df2952"
// <auto-generated/>
#pragma warning disable 1591
namespace app_authen.Pages
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/Volumes/Cybersoft/authentication_blazor/app_authen/_Imports.razor"
using System.Net.Http

#nullable disable
    ;
#nullable restore
#line 2 "/Volumes/Cybersoft/authentication_blazor/app_authen/_Imports.razor"
using Microsoft.AspNetCore.Authorization

#nullable disable
    ;
#nullable restore
#line 3 "/Volumes/Cybersoft/authentication_blazor/app_authen/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization

#nullable disable
    ;
#nullable restore
#line 4 "/Volumes/Cybersoft/authentication_blazor/app_authen/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms

#nullable disable
    ;
#nullable restore
#line 5 "/Volumes/Cybersoft/authentication_blazor/app_authen/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing

#nullable disable
    ;
#nullable restore
#line 6 "/Volumes/Cybersoft/authentication_blazor/app_authen/_Imports.razor"
using Microsoft.AspNetCore.Components.Web

#nullable disable
    ;
#nullable restore
#line 7 "/Volumes/Cybersoft/authentication_blazor/app_authen/_Imports.razor"
using Microsoft.JSInterop

#nullable disable
    ;
#nullable restore
#line 8 "/Volumes/Cybersoft/authentication_blazor/app_authen/_Imports.razor"
using app_authen

#nullable disable
    ;
#nullable restore
#line 9 "/Volumes/Cybersoft/authentication_blazor/app_authen/_Imports.razor"
using app_authen.Shared

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Components.RouteAttribute(
    // language=Route,Component
#nullable restore
#line 1 "/Volumes/Cybersoft/authentication_blazor/app_authen/Pages/Index.razor"
      "/"

#line default
#line hidden
#nullable disable
    )]
    #nullable restore
    public partial class Index : global::Microsoft.AspNetCore.Components.ComponentBase
    #nullable disable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Hello, world!</h1>\r\n\r\nWelcome to your new app.\r\n\r\n");
            __builder.OpenComponent<global::app_authen.Shared.SurveyPrompt>(1);
            __builder.AddAttribute(2, "Title", (object)("How is Blazor working for you?"));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
