﻿namespace LifeStruct
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    public class UpdateApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();

        [Route("api/UpdateApi/SetMood")]
        [HttpGet]
        public IHttpActionResult SetMood(string Id)
        {
            var mood = db.Mood.ToList().Where(x => x.UserId == Id);
            MoodModel mm = new MoodModel();

            if (mood.Count() > 0)
            {
                if (mm.Date == DateTime.Now.ToString("dd-MM-yyyy"))
                {
                    mm = mood.First();
                    mm.Date = DateTime.Now.ToString("dd-MM-yyyy");
                    db.Entry(mm).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    mm.Id = Guid.NewGuid().ToString();
                    mm.UserId = Id;
                    mm.Date = DateTime.Now.ToString("dd-MM-yyyy");
                    db.Mood.Add(mm);
                    db.SaveChanges();
                }
            }
            else
            {
                mm.Id = Guid.NewGuid().ToString();
                mm.UserId = Id;
                mm.Date = DateTime.Now.ToString("dd-MM-yyyy");
                db.Mood.Add(mm);
                db.SaveChanges();
            }
            return Ok();
        }
        [Route("api/UpdateApi/SetGoal")]
        [HttpGet]
        public IHttpActionResult SetGoal(string Id)
        {
            var split = Id.Split('_');
            var goal = db.Goal.ToList().Where(x => x.UserId == split[0]);
            GoalModel gm = new GoalModel();
            int Goal = Convert.ToInt32(split[1]);
            if (goal.Count() > 0)
            {
                if (goal.First().Date == DateTime.Now.ToString("dd-MM-yyyy"))
                {
                    gm = goal.First();
                    gm.Date = DateTime.Now.ToString("dd-MM-yyyy");
                    gm.Goal = Goal;
                    db.Entry(gm).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    gm.Id = Guid.NewGuid().ToString();
                    gm.UserId = split[0];
                    gm.Goal = Goal;
                    gm.Date = DateTime.Now.ToString("dd-MM-yyyy");
                    db.Goal.Add(gm);
                    db.SaveChanges();
                }
            }
            else
            {
                gm.Id = Guid.NewGuid().ToString();
                gm.UserId = split[0];
                gm.Goal = Goal;
                gm.Date = DateTime.Now.ToString("dd-MM-yyyy");
                db.Goal.Add(gm);
                db.SaveChanges();
            }
            return Ok();
        }
        [Route("api/UpdateApi/GetGoal")]
        [HttpGet]
        public IHttpActionResult GetGoal(string uId)
        {
            var goal = db.Goal.Where(x => x.UserId == uId);
            var date = new DateTime();
            foreach (var g in goal)
            {
                if (Convert.ToDateTime(g.Date).Date > date)
                {
                    date = Convert.ToDateTime(g.Date);
                }
            }
            var goalByDate = db.Goal.ToList().Where(x => x.UserId == uId && x.Date == date.ToString("dd-MM-yyyy"));
            if (goalByDate.Count() > 0)
            {
                return Ok(goalByDate.First());
            }
            else
            {
                return NotFound();
            }
        }
    }
}
