#pragma checksum "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\UpdateStock.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "de783167306ae81472aa9bcd874e0ef038c77de3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_UpdateStock), @"mvc.1.0.view", @"/Views/Admin/UpdateStock.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\_ViewImports.cshtml"
using HurtowniaReptiGood;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\_ViewImports.cshtml"
using HurtowniaReptiGood.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\UpdateStock.cshtml"
using HurtowniaReptiGood.Models.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"de783167306ae81472aa9bcd874e0ef038c77de3", @"/Views/Admin/UpdateStock.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6834015aa91ceb0ba53bc76759ac2bb4cb41557b", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_UpdateStock : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UpdateProductsStockStatusViewModel>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\UpdateStock.cshtml"
  
    Layout = "~/Views/Shared/_LayoutAdmin.cshtml";
    ViewData["Title"] = "GetProducts";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "de783167306ae81472aa9bcd874e0ef038c77de33653", async() => {
                WriteLiteral("\r\n    <div>\r\n        <button class=\"myButton\">");
#nullable restore
#line 12 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\UpdateStock.cshtml"
                            Write(Html.ActionLink("Importuj stan magazynowy produktów z SubiektGT", "UpdateProductsStock", "Admin"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</button>\r\n    </div>\r\n    <div> \r\n        <p><span>");
#nullable restore
#line 15 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\UpdateStock.cshtml"
            Write(Status());

#line default
#line hidden
#nullable disable
                WriteLiteral("</span></p>\r\n    </div>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
        }
        #pragma warning restore 1998
#nullable restore
#line 19 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\UpdateStock.cshtml"
           
    string UpdateStatus;

    string Status()
    {
        if (Model.UpdateStatus=="")
        {
            Model.UpdateStatus = "Uruchom pobieranie stanu magazynowego";
        }

        return Model.UpdateStatus;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UpdateProductsStockStatusViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591