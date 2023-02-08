using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Question_Answer_Engine.Models;

namespace Question_Answer_Engine.Controllers
{
    public class HomeController : Controller
    {
        private QA_Engine_DbContext db = new QA_Engine_DbContext();

        // GET: Home
        public ActionResult Index()
        {
            List<Question> questionList = null;
            using (db)
            {
                var query = from q in db.Questions
                            orderby q.Views
                            select q;
                questionList = query.ToList();
            }
            ViewBag.Questions = questionList;
            return View();
        }

        // GET: Question/QuestionDetails
        public ActionResult QuestionDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            question.Views++;
            db.SaveChanges();
            ViewBag.Question = question;
            var commentsQuery = from qc in question.QuestionComments
                                orderby qc.Date descending
                                select qc;
            ViewBag.QuestionComments = commentsQuery.ToList();
            var answersQuery = from a in question.Answers
                               orderby a.Upvotes, a.Date
                               select a;
            ViewBag.Answers = answersQuery.ToList();
            return View();
        }

        // GET: Question/VoteQuestion
        [Authorize]
        public ActionResult VoteQuestion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            question.Upvotes++;
            question.Views--;
            db.SaveChanges();
            return RedirectToAction("QuestionDetails/" + id, "Home");
        }

        // GET: Question/CreateQuestion
        [Authorize]
        public ActionResult CreateQuestion()
        {
            return View();
        }

        // POST: Question/CreateQuestion
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateQuestion([Bind(Include = "Title,Content")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.UserName = User.Identity.Name;
                question.Date = DateTime.Now;
                question.Views = 0;
                question.Upvotes = 0;
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index/" + question.QuestionId, "Home");
            }

            return View();
        }

        // GET: Answer/AnswerDetails
        public ActionResult AnswerDetails(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if(answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Answer = answer;
            var commentsQuery = from ac in answer.AnswerComments
                                orderby ac.Date descending
                                select ac;
            ViewBag.AnswerComments = commentsQuery.ToList();
            return View();
        }

        // GET: Answer/VoteAnswer
        [Authorize]
        public ActionResult VoteAnswer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            answer.Upvotes++;
            db.SaveChanges();
            return RedirectToAction("AnswerDetails/" + answer.QuestionId, "Home");
        }

        // GET: Answer/CreateAnswer
        [Authorize]
        public ActionResult CreateAnswer()
        {
            return View();
        }

        // POST: Answer/CreateAnswer
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateAnswer([Bind(Include = "Content")] Answer answer, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                answer.QuestionId = (int)id;
                answer.UserName = User.Identity.Name;
                answer.Date = DateTime.Now;
                answer.Upvotes = 0;
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("QuestionDetails/" + answer.QuestionId, "Home");
            }

            return View();
        }

        // GET: QuestionComment/CreateQuestionComment
        [Authorize]
        public ActionResult CreateQuestionComment()
        {
            return View();
        }

        // POST: QuestionComment/CreateQuestionComment
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateQuestionComment([Bind(Include = "Content")] QuestionComment qComment, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                qComment.QuestionId = (int)id;
                qComment.UserName = User.Identity.Name;
                qComment.Date = DateTime.Now;
                db.QuestionComments.Add(qComment);
                db.SaveChanges();
                return RedirectToAction("QuestionDetails/" + qComment.QuestionId, "Home");
            }

            return View();
        }

        // GET: AnswerComment/CreateAnswerComment
        [Authorize]
        public ActionResult CreateAnswerComment()
        {
            return View();
        }

        // POST: AnswerComment/CreateAnswerComment
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateAnswerComment([Bind(Include = "Content")] AnswerComment aComment, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                aComment.AnswerId = (int)id;
                aComment.UserName = User.Identity.Name;
                aComment.Date = DateTime.Now;
                db.AnswerComments.Add(aComment);
                db.SaveChanges();
                return RedirectToAction("AnswerDetails/" + aComment.AnswerId, "Home");
            }

            return View();
        }
    }
}