using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper.QueryableExtensions;
using LearningAngular.ControllersApi.Models;
using LearningAngular.Domain;

namespace LearningAngular.ControllersApi
{
    [RoutePrefix("api/mentors")]
    public class MentorsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllMentors()
        {
            using (var context = new MentoringContext())
            {
	            var results = await context.Mentors.Project().To<MentorSummaryViewModel>().ToListAsync();
                return Ok(results);
            }
        }

        [HttpGet] 
        [Route("{id:int}", Name = "find")]
        public async Task<IHttpActionResult> GetAMentor(int id)
        {
            using (var context = new MentoringContext())
            {
                var result = await context.Mentors.FindAsync(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Add(NewMentor newMentor)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            using (var context = new MentoringContext())
            {
                var entity = new Mentor()
                {
                    FirstName = newMentor.FirstName,
                    LastName = newMentor.LastName,
                    TakingNewStudents = newMentor.Available,
                    StudentCount = 0
                };

                context.Mentors.Add(entity);
                await context.SaveChangesAsync();
                return CreatedAtRoute("find", new { id = entity.Id }, entity);
            }


        }
    }



    public class NewMentor
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool Available { get; set; }
    }

}
