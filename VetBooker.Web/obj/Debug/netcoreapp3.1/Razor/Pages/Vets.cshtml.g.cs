#pragma checksum "C:\Projects\VetBooker\VetBooker.Web\Pages\Vets.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0523d78c21d3f08e9c559acfc1c28f3cff304854"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(VetBooker.Web.Pages.Pages_Vets), @"mvc.1.0.razor-page", @"/Pages/Vets.cshtml")]
namespace VetBooker.Web.Pages
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
#line 1 "C:\Projects\VetBooker\VetBooker.Web\Pages\_ViewImports.cshtml"
using VetBooker.Web;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0523d78c21d3f08e9c559acfc1c28f3cff304854", @"/Pages/Vets.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"708ad2946fe285214e7b955f0c20c9e977511944", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Vets : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Projects\VetBooker\VetBooker.Web\Pages\Vets.cshtml"
  
  ViewData["Title"] = "Vets";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1 class=""display-4"">Vets</h1>

<p class=""lead"">Here you see a table with all vets available at VetClinic.</p>

<hr />

<table class=""table table-striped table-bordered dt-responsive nowrap"">
  <thead class=""thead-light"">
    <tr>
      <th>Vet Id</th>
      <th>Description</th>
    </tr>
  </thead>
  <tbody>
");
#nullable restore
#line 21 "C:\Projects\VetBooker\VetBooker.Web\Pages\Vets.cshtml"
     foreach (var vet in Model.Vets)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("      <tr>\n        <td>");
#nullable restore
#line 24 "C:\Projects\VetBooker\VetBooker.Web\Pages\Vets.cshtml"
       Write(vet.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n        <td>");
#nullable restore
#line 25 "C:\Projects\VetBooker\VetBooker.Web\Pages\Vets.cshtml"
       Write(vet.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n      </tr>\n");
#nullable restore
#line 27 "C:\Projects\VetBooker\VetBooker.Web\Pages\Vets.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("  </tbody>\n</table>\n\n\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<VetsModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<VetsModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<VetsModel>)PageContext?.ViewData;
        public VetsModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
