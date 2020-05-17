#pragma checksum "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "07c51ace7e9b0ed0ec12bc141e2d20120153ca1b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_OrderDetails), @"mvc.1.0.view", @"/Views/Admin/OrderDetails.cshtml")]
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
#line 6 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
using HurtowniaReptiGood.Models.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"07c51ace7e9b0ed0ec12bc141e2d20120153ca1b", @"/Views/Admin/OrderDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6834015aa91ceb0ba53bc76759ac2bb4cb41557b", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_OrderDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<OrderDetailsAndDpdTrackingStatusViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("EditDetail"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
  
    Layout = "~/Views/Shared/_LayoutAdmin.cshtml";
    ViewData["Title"] = "OrderDetails";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<h3>Szczegóły zamówienia</h3>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "07c51ace7e9b0ed0ec12bc141e2d20120153ca1b4632", async() => {
                WriteLiteral(@"
    <div>

        <table class=""table table-bordered"">
            <thead>
                <tr>
                    <th scope=""col"" class=""text-center"">Symbol</th>
                    <th scope=""col"" class=""text-center"">Nazwa</th>
                    <th scope=""col"" class=""text-center"">Sztuk</th>
                    <th scope=""col"" class=""text-center"">Cena</th>
                    <th scope=""col"" class=""text-center"">Wartość</th>                    
                    <th scope=""col"" class=""text-center"">Edytuj</th>
                </tr>
            <thead>
            <tbody>
");
#nullable restore
#line 27 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
                 foreach (OrderDetailViewModel detail in Model.OrderDetails.OrderDetailList)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <tr class=\"text-center\">\r\n                        <td>");
#nullable restore
#line 30 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
                       Write(detail.ProductSymbol);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 31 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
                       Write(detail.ProductName);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 32 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
                       Write(detail.Quantity);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 33 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
                       Write(detail.Price);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 34 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
                       Write(detail.Value);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                        <td>\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "07c51ace7e9b0ed0ec12bc141e2d20120153ca1b7333", async() => {
                    WriteLiteral("\r\n                                <input type=\"hidden\" name=\"OrderDetailId\"");
                    BeginWriteAttribute("value", " value=\"", 1428, "\"", 1457, 1);
#nullable restore
#line 37 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
WriteAttributeValue("", 1436, detail.OrderDetailId, 1436, 21, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(">\r\n                                <input type=\"submit\" value=\"Edytuj\" />\r\n                            ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 42 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
                }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                <tr>
                    <th scope=""col"" class=""text-center"">Numer przesyłki</th>
                    <th colspan=""2"" scope=""col"" class=""text-center"">Status</th>
                    <th colspan=""2"" scope=""col"" class=""text-center"">Data</th>
                    <th scope=""col"" class=""text-center"">Miejsce</th>
                </tr>
                <tr>
");
#nullable restore
#line 50 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
                     foreach (DpdTrackingStatusViewModel status in Model.DpdTrackingStatusList.TrackingList)
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <td scope=\"col\" class=\"text-center\">");
#nullable restore
#line 52 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
                                                   Write(status.TrackingNumber);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                    <td colspan=\"2\" scope=\"col\" class=\"text-center\">");
#nullable restore
#line 53 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
                                                               Write(status.Event);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                    <td colspan=\"2\" scope=\"col\" class=\"text-center\">");
#nullable restore
#line 54 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
                                                               Write(status.EventDateTime);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                    <td scope=\"col\" class=\"text-center\">");
#nullable restore
#line 55 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
                                                   Write(status.EventPlace);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n");
#nullable restore
#line 56 "C:\Users\mojeq\source\repos\HurtowniaReptiGood\Views\Admin\OrderDetails.cshtml"
                    }

#line default
#line hidden
#nullable disable
                WriteLiteral("                </tr>\r\n            <tbody>\r\n        </table>\r\n    </div>\r\n");
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
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<OrderDetailsAndDpdTrackingStatusViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
