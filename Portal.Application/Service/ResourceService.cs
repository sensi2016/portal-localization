
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.DAL.Extensions;
using Portal.Infrastructure;

namespace Portal.Application.Service
{
    public class PortalResourceService : IPortalResourceService
    {
        private readonly IStringLocalizer<ReportResource> _reportLocalizer;
        private readonly IHostingEnvironment _hostEnvironment;

        public PortalResourceService(IStringLocalizer<ReportResource> reportLocalizer, IHostingEnvironment hostEnvironment)
        {
            _reportLocalizer = reportLocalizer;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<BaseResponseDto> GetLabelReport(string reportName, List<Type> models)
        {
            // لیست ترجمه های نهایی
            var labels = new Dictionary<string, string>();

            // لیست همه کلیدهای ترجمه
            var keys = _reportLocalizer.GetAllStrings()
                .Where(x => x.Name.Contains("GlobalReport") || x.Name.Contains(reportName))
                .ToList();

            // جستجو براساس هر مدل ارسالی
            foreach (var model in models)
            {
                // لیست پراپرتی های مدل
                var propertiesName = model.GetProperties().Where(x => x.Name != "ReportTypeModel")
                    .Select(x=>x.Name).ToList();
                if (propertiesName.Count == 0)
                    propertiesName = model.GetFields().Select(x => x.Name).ToList();

                // مدل  
                var reportTypeModel = model.GetIntProperty("ReportTypeModel");
                var modelName = string.Empty;
                switch (reportTypeModel)
                {
                    case 1:
                        modelName = "Header";
                        break;
                    case 2:
                        modelName = "Footer";
                        break;
                    case 3:
                        modelName = "Col";
                        break;
                }

                foreach (var propertyName in propertiesName)
                {
                    string translate = string.Empty;

                    // در صورتیکه نام گزارش ثبت شده باشد
                    if (!string.IsNullOrEmpty(reportName))
                    {
                        // کلید براساس نام ریپورت + مدل + نام پراپرتی
                        translate = keys.FirstOrDefault(x => x.Name.Equals($"{reportName}.{modelName}.{propertyName}"));

                        // کلید براساس نام ریپورت +‌ پراپرتی
                        if (string.IsNullOrEmpty(translate))
                            translate = keys.FirstOrDefault(x => x.Name.Equals($"{reportName}.{propertyName}"));
                    }

                    // کلید براساس گلوبال + مدل + نام پراپرتی 
                    if (string.IsNullOrEmpty(translate))
                        translate = keys.FirstOrDefault(x => x.Name.Equals($"GlobalReport.{modelName}.{propertyName}"));

                    // کلید براساس گلوبال + نام پراپرتی 
                    if (string.IsNullOrEmpty(translate))
                        translate = keys.FirstOrDefault(x => x.Name.Equals($"GlobalReport.{propertyName}"));

                    // اضافه کردن ترجمه و کلید 
                    if (!string.IsNullOrEmpty(translate))
                        labels.Add(propertyName.FirstCharToLower(), translate);
                }
            }

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = labels
            };
        }

        public async Task<BaseResponseDto> New(NewResourceDto newResource)
        {
            // resource file path

            var lang = "en-Us"; if (newResource.Language.TryToLower().Contains("ar")) lang = "ar-IQ";

            var contentRootPath = _hostEnvironment.ContentRootPath;
            var path = Path.Combine(contentRootPath, $@"Resources\SharedResource.{lang}.resx")
                .Replace("Portal.Api", "Portal.Infrastructure");
            if (newResource.ResourceName.TryToLower().Contains("report")) path = path.Replace("SharedResource", "ReportResource");
            //C: \Users\Mojtaba\Documents\Project\HisPortalApi\Portal.Infrastructure\Resources
             var doc = XDocument.Load(path);

            // get data node
            var root = doc.Root;
            var dataElements = root?.Elements("data");
            dataElements = dataElements?.Where(x => newResource.Translates.ContainsKey(x.Attribute("name")?.Value ?? ""));

            var countAddItems = 0;
            var countEditItems = 0;

            // data node exist
            if (dataElements != null)
            {
                
                foreach (var translate in newResource.Translates)
                {
                    var key = translate.Key.Replace(" ", "");
                    var resource = dataElements.FirstOrDefault(x => x.Attribute("name").Value == key);

                    if (resource != null && resource.Element("value")?.Value != translate.Value)
                    {
                        resource.SetElementValue("value", translate.Value);
                        countEditItems++;
                    }

                    else if (resource is null)
                    {
                        var newKey = new XElement("data");
                        newKey.SetAttributeValue("name", key);
                        newKey.SetAttributeValue(XNamespace.Xml + "space", "preserve");
                        newKey.SetElementValue("value", translate.Value);
                        root.Add(newKey);

                        countAddItems++;
                    }
                }

            }

            doc.Save(path);

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = $"{countAddItems} Items Added and {countEditItems} Items Edited."
            };
        }
    }
}
