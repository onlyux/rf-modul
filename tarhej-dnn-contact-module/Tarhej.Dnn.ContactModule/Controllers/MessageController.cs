/*
' Copyright (c) 2025 Alberti Adam
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using DotNetNuke.Collections;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;
using Tarhej.Dnn.Tarhej.Dnn.ContactModule.Components;
using Tarhej.Dnn.Tarhej.Dnn.ContactModule.Models;
using System.Net.Mail;
using DotNetNuke.Security;
using Microsoft.Web.Helpers;
using Newtonsoft.Json;
using System.Configuration;

namespace Tarhej.Dnn.Tarhej.Dnn.ContactModule.Controllers
{
    [DnnHandleError]
    public class MessageController : DnnController
    {
        private readonly string captchaKey = "6LdRCysrAAAAAF5tRgbb6bNfeUyy2jA74E_WsEVe";
        public ActionResult Delete(int MessageId)
        {
            if (!IsUserAdmin())
            {
                return new HttpStatusCodeResult(403, "Hozzáférés megtagadva: csak adminisztrátorok férhetnek hozzá");
            }
            MessageManager.Instance.DeleteItem(MessageId);
            return RedirectToDefaultRoute();
        }

        public ActionResult Edit(int MessageId = -1)
        {
            if (!IsUserAdmin())
            {
                return new HttpStatusCodeResult(403, "Hozzáférés megtagadva: csak adminisztrátorok férhetnek hozzá");
            }
            var item = (MessageId == -1)
                 ? new Message { }
                 : MessageManager.Instance.GetItem(MessageId);

            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Message message)
        {
            if (!IsUserAdmin())
            {
                return new HttpStatusCodeResult(403, "Hozzáférés megtagadva: csak adminisztrátorok férhetnek hozzá");
            }
            if (message.MessageId == -1)
            {

                MessageManager.Instance.CreateItem(message);
            }
            else
            {
                var existingItem = MessageManager.Instance.GetItem(message.MessageId);
                existingItem.MessageContent = message.MessageContent;
                existingItem.Product = message.Product;
                existingItem.Status = message.Status;
                existingItem.FirstName = message.FirstName;
                existingItem.LastName = message.LastName;
                existingItem.Subject = message.Subject;
                existingItem.Email = message.Email;


                MessageManager.Instance.UpdateItem(existingItem);
            }

            return RedirectToDefaultRoute();
        }


        public ActionResult Index()
        {
            // Retrieve the DefaultView setting
            var defaultView = ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("Dnn.Tarhej.ContactModule_DefaultView", "Index");

            // Redirect to the appropriate action based on the setting
            switch (defaultView)
            {
                case "Additem":
                    return RedirectToAction("Additem");
                default:
                    if (!IsUserAdmin())
                    {
                        return new HttpStatusCodeResult(403, "Hozzáférés megtagadva: csak adminisztrátorok férhetnek hozzá");
                    }
                    var messages = MessageManager.Instance.GetItems();
                    int totalMessages = messages.Count();
                    int unansweredMessages = messages.Count(m => m.Status == "nem megválaszolt");
                    ViewBag.TotalMessages = totalMessages;
                    ViewBag.UnansweredMessages = unansweredMessages;

                    return View(messages);
            }
        }
        public ActionResult AddItem()
        {
            var currentUser = UserController.Instance.GetCurrentUserInfo();
            var message = new Message
            {
                FirstName = currentUser?.FirstName,
                LastName = currentUser?.LastName,
                Email = currentUser?.Email
            };
            return View(message);
        }

        [HttpPost]
        public ActionResult AddItem(Message m)
        {
            var currentUser = UserController.Instance.GetCurrentUserInfo();
            if (currentUser == null || currentUser.UserID == -1)
            {
                ViewBag.Error = "Csak bejelentkezett felhasználók küldhetnek üzenetet";
                return View(m);
            }
            //Captcha
            var response = Request["g-recaptcha-response"];
            var secretKey = captchaKey;
            var client = new System.Net.WebClient();
            var result = client.DownloadString($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={response}");
            var captchaResult = JsonConvert.DeserializeObject<dynamic>(result);
            MessageManager.Instance.CreateItem(m);
            if (captchaResult.success != true)
            {
                ViewBag.Error = "A CAPTCHA ellenőrzése sikertelen. Kérlek próbáld újra.";
                return View(m);
            }
            ViewBag.Message = "Üzenet sikeresen elküldve";
            return View(new Message());
        }

        public ActionResult GetMessage(int MessageId)
        {
            if (!IsUserAdmin())
            {
                return new HttpStatusCodeResult(403, "Hozzáférés megtagadva: csak adminisztrátorok férhetnek hozzá");
            }
            var message = MessageManager.Instance.GetItem(MessageId);
            return View(message);
        }

        public ActionResult SendEmail(string subject, string email)
        {
            if (!IsUserAdmin())
            {
                return new HttpStatusCodeResult(403, "Hozzáférés megtagadva: csak adminisztrátorok férhetnek hozzá");
            }
            var model = new EmailViewModel
            {
                Email = email,
                Subject = subject,
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult SendEmail(EmailViewModel model)
        {
            if (!IsUserAdmin())
            {
                return new HttpStatusCodeResult(403, "Hozzáférés megtagadva: csak adminisztrátorok férhetnek hozzá");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var email = new MailMessage();
                    email.From = new MailAddress("tarhejhu@gmail.com");
                    email.To.Add(model.Email);
                    email.Subject = model.Subject;
                    email.Body = model.Message;
                    email.IsBodyHtml = false;

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        UseDefaultCredentials = false,
                        Port = 587,
                        Credentials = new System.Net.NetworkCredential("tarhejhu@gmail.com", "kloo hrjd kzsm gnor"),
                        EnableSsl = true
                    };
                    smtpClient.Send(email);
                    ViewBag.Message = "Email sikeresen elküldve";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Hiba történt az email küldése során: " + ex.Message;
                }
            }
            return View(model);
        }

        private bool IsUserAdmin()
        {
            var currentUser = UserController.Instance.GetCurrentUserInfo();
            return currentUser != null && (currentUser.IsSuperUser || currentUser.IsAdmin);
        }

        public ActionResult SortMessages(bool unansweredFirst = false)
        {
            if (!IsUserAdmin())
            {
                return new HttpStatusCodeResult(403, "Hozzáférés megtagadva: csak adminisztrátorok férhetnek hozzá");

            }
            var messages = MessageManager.Instance.GetItems();

            if (unansweredFirst)
            {
                messages = messages.OrderByDescending(m => m.Status == "nem megválaszolt").ThenByDescending(m => m.ContactDate);
            }
            else
            {
                messages = messages.OrderByDescending(m => m.ContactDate);
            }
            int totalMessages = messages.Count();
            int unansweredMessages = messages.Count(m => m.Status == "nem megválaszolt");
            ViewBag.TotalMessages = totalMessages;
            ViewBag.UnansweredMessages = unansweredMessages;

            return View("Index", messages);
        }
    }
}
