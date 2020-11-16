#pragma checksum "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cfaa0ff0f7a0dd07d7edc6984bb789e675390249"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cart_Cart), @"mvc.1.0.view", @"/Views/Cart/Cart.cshtml")]
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
#line 1 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\_ViewImports.cshtml"
using HurtowniaReptiGood;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\_ViewImports.cshtml"
using HurtowniaReptiGood.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
using HurtowniaReptiGood.Models.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cfaa0ff0f7a0dd07d7edc6984bb789e675390249", @"/Views/Cart/Cart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6834015aa91ceb0ba53bc76759ac2bb4cb41557b", @"/Views/_ViewImports.cshtml")]
    public class Views_Cart_Cart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CartAndAddressesViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("UpdateQuantityInCart"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("SaveNewOrder"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("sendOrder"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 1 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
  
    ViewData["Title"] = "Koszyk";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cfaa0ff0f7a0dd07d7edc6984bb789e6753902495185", async() => {
                WriteLiteral(@"
    <h3>Zawartość koszyka:</h3>

    <table style=""width:1200px"" cellpadding=""2"" cellspacing=""2"" border=""1"">
        <tr>
            <th>Symbol</th>
            <th>Nazwa</th>
            <th>Cena brutto</th>
            <th>Sztuk</th>
            <th>Wartość</th>
            <th>Usuń</th>
        </tr>
");
#nullable restore
#line 22 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
         foreach (OrderDetailViewModel cart in Model.CartToView.OrderDetailList)
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <tr text-align=\"center\">\r\n                <td>");
#nullable restore
#line 25 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
               Write(cart.ProductSymbol);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 26 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
               Write(cart.ProductName);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 27 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
               Write(cart.Price);

#line default
#line hidden
#nullable disable
                WriteLiteral(" zł</td>\r\n                <td>\r\n\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cfaa0ff0f7a0dd07d7edc6984bb789e6753902496879", async() => {
                    WriteLiteral("\r\n                        <input size=\"5\" height=\"25\" type=\"number\" name=\"Quantity\" min=\"1\"");
                    BeginWriteAttribute("max", " max=\"", 946, "\"", 981, 1);
#nullable restore
#line 31 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
WriteAttributeValue("", 952, cart.CurrentStockInWholesale, 952, 29, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    BeginWriteAttribute("value", " value=\"", 982, "\"", 1004, 1);
#nullable restore
#line 31 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
WriteAttributeValue("", 990, cart.Quantity, 990, 14, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" />\r\n                        <input type=\"hidden\" name=\"OrderId\"");
                    BeginWriteAttribute("value", " value=\"", 1069, "\"", 1090, 1);
#nullable restore
#line 32 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
WriteAttributeValue("", 1077, cart.OrderId, 1077, 13, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" />\r\n                        <input type=\"hidden\" name=\"OrderDetailId\"");
                    BeginWriteAttribute("value", " value=\"", 1161, "\"", 1188, 1);
#nullable restore
#line 33 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
WriteAttributeValue("", 1169, cart.OrderDetailId, 1169, 19, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" />\r\n                        <input type=\"hidden\" name=\"ProductId\"");
                    BeginWriteAttribute("value", " value=\"", 1255, "\"", 1278, 1);
#nullable restore
#line 34 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
WriteAttributeValue("", 1263, cart.ProductId, 1263, 15, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" />\r\n                        <input type=\"submit\" value=\"Odśwież\" />\r\n                    ");
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
                WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 39 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
                Write(itemValue = cart.Price * (Convert.ToDouble(cart.Quantity)));

#line default
#line hidden
#nullable disable
                WriteLiteral("zł\r\n                </td>\r\n                <td>\r\n");
#nullable restore
#line 42 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
                     using (Html.BeginForm("RemoveItemFromCart", "Cart", new { orderDetailId = cart.OrderDetailId, orderId = cart.OrderId }))
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("<input type=\"submit\" value=\"Usuń\" />");
#nullable restore
#line 43 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
                                                         }

#line default
#line hidden
#nullable disable
                WriteLiteral("                </td>\r\n\r\n            </tr>\r\n");
#nullable restore
#line 47 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
            method: @CalculateCartBrutto(itemValue);
                @CalculateCartNetto(totalValueBrutto);
                @GenerateMessageAndDiscount(totalValueNetto, totalValueBrutto);
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n        <tr>\r\n            <th colspan=\"2\">Wartość netto zamówienia</th>\r\n            <th colspan=\"4\">");
#nullable restore
#line 54 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
                       Write(totalValueNetto);

#line default
#line hidden
#nullable disable
                WriteLiteral(" zł</th>\r\n        </tr>\r\n        <tr>\r\n            <th colspan=\"2\">Wartość brutto zamówienia</th>\r\n            <th colspan=\"4\">");
#nullable restore
#line 58 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
                       Write(totalValueBrutto);

#line default
#line hidden
#nullable disable
                WriteLiteral(" zł</th>\r\n        </tr>\r\n        <tr>\r\n            <th colspan=\"2\">Koszt wysyłki</th>\r\n            <th colspan=\"4\">");
#nullable restore
#line 62 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
                       Write(shippingCost);

#line default
#line hidden
#nullable disable
                WriteLiteral(" zł</th>\r\n        </tr>\r\n        <tr>\r\n            <th colspan=\"2\">Rabat</th>\r\n            <th colspan=\"4\">");
#nullable restore
#line 66 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
                       Write(discount);

#line default
#line hidden
#nullable disable
                WriteLiteral(" %</th>\r\n        </tr>\r\n        <tr>\r\n            <th colspan=\"2\">Wartość brutto zamówienia po rabacie</th>\r\n            <th colspan=\"4\">");
#nullable restore
#line 70 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
                        Write(valueOrder = totalValueBruttoWithDiscount + shippingCost);

#line default
#line hidden
#nullable disable
                WriteLiteral(" zł</th>\r\n        </tr>\r\n        <tr>\r\n            <th colspan=\"6\">");
#nullable restore
#line 73 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
                       Write(message);

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n        </tr>\r\n        <tr>\r\n            <td colspan=\"6\">\r\n");
#nullable restore
#line 77 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
                 using (Html.BeginForm("Index", "Home", FormMethod.Get))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("<input type=\"submit\" value=\"Dodaj kolejny produkt\" />");
#nullable restore
#line 78 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
                                                                      }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"        </td>
    </tr>
</table>

<p>
    <table style=""width:70%"" cellpadding=""2"" cellspacing=""2"" border=""1"">
        <tr>
            <th colspan=""2"">Dane do wysyłki</th>
        </tr>
        <tr>
            <td style=""width:30%"">Nazwa firmy</td>
            <td>");
#nullable restore
#line 90 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
           Write(Model.ShippingAddress.CompanyName);

#line default
#line hidden
#nullable disable
                WriteLiteral(" ");
#nullable restore
#line 90 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
                                              Write(Model.ShippingAddress.CustomerName);

#line default
#line hidden
#nullable disable
                WriteLiteral(" ");
#nullable restore
#line 90 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
                                                                                  Write(Model.ShippingAddress.CustomerSurname);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Ulica</td>\r\n            <td>");
#nullable restore
#line 94 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
           Write(Model.ShippingAddress.Street);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Numer</td>\r\n            <td>");
#nullable restore
#line 98 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
           Write(Model.ShippingAddress.StreetNumber);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Miasto</td>\r\n            <td>");
#nullable restore
#line 102 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
           Write(Model.ShippingAddress.City);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Kod pocztowy</td>\r\n            <td>");
#nullable restore
#line 106 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
           Write(Model.ShippingAddress.ZipCode);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Telefon</td>\r\n            <td>");
#nullable restore
#line 110 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
           Write(Model.ShippingAddress.Phone);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>E-mail</td>\r\n            <td>");
#nullable restore
#line 114 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
           Write(Model.ShippingAddress.Email);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</td>
        </tr>
    </table>
</p>

<p>
    <table style=""width:70%"" cellpadding=""2"" cellspacing=""2"" border=""1"">
        <tr>
            <th colspan=""2"">Dane do faktury</th>
        </tr>
        <tr>
            <td style=""width:30%"">Nazwa firmy</td>
            <td>");
#nullable restore
#line 126 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
           Write(Model.InvoiceAddress.CompanyName);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </td>\r\n        </tr>\r\n        <tr>\r\n            <td>NIP</td>\r\n            <td>");
#nullable restore
#line 130 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
           Write(Model.InvoiceAddress.NIP);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Ulica</td>\r\n            <td>");
#nullable restore
#line 134 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
           Write(Model.InvoiceAddress.Street);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Numer</td>\r\n            <td>");
#nullable restore
#line 138 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
           Write(Model.InvoiceAddress.StreetNumber);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Miasto</td>\r\n            <td>");
#nullable restore
#line 142 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
           Write(Model.InvoiceAddress.City);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Kod pocztowy</td>\r\n            <td>");
#nullable restore
#line 146 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
           Write(Model.InvoiceAddress.ZipCode);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Telefon</td>\r\n            <td>");
#nullable restore
#line 150 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
           Write(Model.InvoiceAddress.Phone);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</td>
        </tr>
    </table>
</p>

<table style=""width:70%"" cellpadding=""2"" cellspacing=""2"" border=""1"">
    <tr>
        <th colspan=""2"">Uwagi do zamówienia</th>
    </tr>
    <tr>
        <td><textarea name=""OrderMessage"" rows=""14"" cols=""65"" form=""sendOrder"" placeholder=""Uwagi do zamówienia, np. zmiana adresu""></textarea></td>
    </tr>
    <tr>
        <td>
            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cfaa0ff0f7a0dd07d7edc6984bb789e67539024920835", async() => {
                    WriteLiteral("\r\n                <input type=\"hidden\" name=\"orderId\"");
                    BeginWriteAttribute("value", " value=\"", 5529, "\"", 5581, 1);
#nullable restore
#line 165 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
WriteAttributeValue("", 5537, Model.CartToView.OrderDetailList[0].OrderId, 5537, 44, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" />\r\n                <input type=\"hidden\" name=\"valueOrder\"");
                    BeginWriteAttribute("value", " value=\"", 5641, "\"", 5660, 1);
#nullable restore
#line 166 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
WriteAttributeValue("", 5649, valueOrder, 5649, 11, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" />\r\n                <input type=\"submit\" value=\"Wyślij zamówienie\" />\r\n            ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        </td>\r\n    </tr>\r\n</table>\r\n");
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
#line 175 "D:\Kodowanie\projects\HurtowniaReptiGood\Views\Cart\Cart.cshtml"
 
    double valueOrder;
    double totalValueBrutto = 0;
    double totalValueNetto = 0;
    double totalValueBruttoWithDiscount = 0;
    double itemValue = 0;
    string message = "";
    double discount = 0;
    double shippingCost;
    int id;
    int OrderId;


    void CalculateCartBrutto(double itemValue)
    {
        totalValueBrutto = totalValueBrutto + itemValue;
    }

    void GenerateMessageAndDiscount(double totalValueNetto, double totalValueBrutto)
    {
        if (totalValueNetto >= 3000)
        {
            totalValueBruttoWithDiscount = totalValueBrutto * 0.91;
            string temp = String.Format("{0:N2}", totalValueBruttoWithDiscount);
            totalValueBruttoWithDiscount = double.Parse(temp);
            message = "Twoje zamówienie przekracza próg 3000zł netto, otrzymujesz darmową dostawę oraz 9% rabatu na wybrane marki.";
            shippingCost = 0;
            discount = 9;
            return;
        }
        else if (totalValueNetto >= 1500)
        {
            //totalValueNetto(totalValueBrutto);
            message = "Dodaj towary o wartości " + (3000 - totalValueNetto) + " zł aby otrzymać 9% rabatu. Twoje zamówienie przekracza próg 1500zł netto, otrzymujesz darmową dostawę oraz 5% rabatu na wybrane marki.";
            totalValueBruttoWithDiscount = totalValueBrutto * 0.95;
            string temp = String.Format("{0:N2}", totalValueBruttoWithDiscount);
            totalValueBruttoWithDiscount = double.Parse(temp);
            shippingCost = 0;
            discount = 5;
            return;
        }
        else if (totalValueNetto >= 800)
        {
            //totalCartValueNetto(totalValueBrutto);
            message = "Dodaj towary o wartości " + (1500 - totalValueNetto) + " zł aby otrzymać 5% rabatu. Twoje zamówienie przekracza próg 800zł netto i otrzymujesz darmową dostawę.";
            totalValueBruttoWithDiscount = totalValueBrutto;
            shippingCost = 0;
            discount = 0;
            return;
        }
        else if (totalValueNetto < 800)
        {
            //totalCartValueNetto(totalValueBrutto);
            message = "Brakuje " + (800 - totalValueNetto) + " zł do darmowej wysyłki. Dodaj towary o wartości " + (1500 - totalValueNetto) + " zł aby otrzymać darmową wysyłkę i 5% rabatu.";
            totalValueBruttoWithDiscount = totalValueBrutto;
            shippingCost = 18;
            discount = 0;
            return;
        }
    }
    void CalculateCartNetto(double totalValueBrutto)
    {
        totalValueNetto = totalValueBrutto / 1.23;
        string netto = String.Format("{0:N0}", totalValueNetto);
        totalValueNetto = double.Parse(netto);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CartAndAddressesViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
