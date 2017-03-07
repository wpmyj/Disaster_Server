using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using Swashbuckle.Swagger;

namespace DisasterReport.Api.SwaggerExtensions
{
    public class CachingSwaggerProvider : ISwaggerProvider
    {
        private static ConcurrentDictionary<string, SwaggerDocument> _cache =
            new ConcurrentDictionary<string, SwaggerDocument>();

        private readonly ISwaggerProvider _swaggerProvider;

        public CachingSwaggerProvider(ISwaggerProvider swaggerProvider)
        {
            _swaggerProvider = swaggerProvider;
        }

        public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            var cacheKey = string.Format("{0}_{1}", rootUrl, apiVersion);
            SwaggerDocument srcDoc = null;
            //只读取一次
            if (!_cache.TryGetValue(cacheKey, out srcDoc))
            {
                srcDoc = _swaggerProvider.GetSwagger(rootUrl, apiVersion);

                srcDoc.vendorExtensions = new Dictionary<string, object> { { "ControllerDesc", GetControllerDesc() } };
                _cache.TryAdd(cacheKey, srcDoc);
            }
            return srcDoc;
        }

        /// <summary>
        /// 从API文档中读取控制器描述
        /// </summary>
        /// <returns>所有控制器描述</returns>
        public static ConcurrentDictionary<string, string> GetControllerDesc()
        {
            ConcurrentDictionary<string, string> controllerDescDict = new ConcurrentDictionary<string, string>();
            var controllerXmlPahts = ConfigurationManager.AppSettings["ControllerXmlPath"].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var controllerXmlPaht in controllerXmlPahts)
            {
                var descDic = GetDescription(string.Format("{0}/bin/{1}.xml", System.AppDomain.CurrentDomain.BaseDirectory, controllerXmlPaht));
                foreach (var desc in descDic)
                {
                    controllerDescDict.TryAdd(desc.Key, desc.Value);
                }
            }

            return controllerDescDict;

            string xmlpath = string.Format("{0}/bin/Test.WebApi.XML", System.AppDomain.CurrentDomain.BaseDirectory);

        }

        private static ConcurrentDictionary<string, string> GetDescription(string xmlPath)
        {
            ConcurrentDictionary<string, string> controllerDescDict = new ConcurrentDictionary<string, string>();
            if (System.IO.File.Exists(xmlPath))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlPath);
                string type = string.Empty, path = string.Empty, controllerName = string.Empty;

                string[] arrPath;
                int length = -1, cCount = "Controller".Length;
                XmlNode summaryNode = null;
                foreach (XmlNode node in xmldoc.SelectNodes("//member"))
                {
                    type = node.Attributes["name"].Value;
                    if (type.StartsWith("T:"))
                    {
                        //控制器
                        arrPath = type.Split('.');
                        length = arrPath.Length;
                        controllerName = arrPath[length - 1];
                        if (controllerName.EndsWith("Controller") || (controllerName.StartsWith("I") && controllerName.EndsWith("AppService")))
                        {

                            //获取控制器注释
                            summaryNode = node.SelectSingleNode("summary");
                            string key = controllerName.Remove(controllerName.Length - cCount, cCount);
                            if (controllerName.EndsWith("AppService"))
                            {
                                key = "app_" + (key.Substring(1, 1).ToLower() + key.Substring(2));
                            }

                            if (summaryNode != null && !string.IsNullOrEmpty(summaryNode.InnerText) && !controllerDescDict.ContainsKey(key))
                            {
                                controllerDescDict.TryAdd(key, summaryNode.InnerText.Trim());
                            }
                        }
                    }
                }
            }
            return controllerDescDict;
        }
    }
}
