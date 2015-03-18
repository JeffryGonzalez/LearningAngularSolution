using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using AutoMapper;
using LearningAngular.ControllersApi.Models;
using LearningAngular.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LearningAngular
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			jsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

			Mapper.CreateMap<Mentor, MentorSummaryViewModel>()
				.ForMember(m => m.StudentCount, opt => opt.MapFrom(src => src.Students.Count));
			
			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
					name: "DefaultApi",
					routeTemplate: "api/{controller}/{id}",
					defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
