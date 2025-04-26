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

namespace Tarhej.Dnn.Tarhej.Dnn.ContactModule.Controllers
{
    [DnnHandleError]
    public class MessageController : DnnController
    {
        public ActionResult Delete(int MessageId)
        {
            MessageManager.Instance.DeleteItem(MessageId);
            return RedirectToDefaultRoute();
        }

        public ActionResult Edit(int MessageId = -1)
        {

            var item = (MessageId == -1)
                 ? new Message { }
                 : MessageManager.Instance.GetItem(MessageId);

            return View(item);
        }

        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Edit(Item item)
        {
            if (item.MessageId == -1)
            {

                ItemManager.Instance.CreateItem(item);
            }
            else
            {
                var existingItem = ItemManager.Instance.GetItem(item.MessageId);
                existingItem.MessageContent = item.MessageContent;
                existingItem.Product = item.Product;

                ItemManager.Instance.UpdateItem(existingItem);
            }

            return RedirectToDefaultRoute();
        }
        public ActionResult Index()
        {
            // Retrieve the DefaultView setting
            var defaultView = ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("Dnn.Tarhej.MessageModul_DefaultView", "Index");

            // Redirect to the appropriate action based on the setting
            switch (defaultView)
            {
                case "Additem":
                    return RedirectToAction("Additem");
                case "GetMessage":
                    return RedirectToAction("GetMessage");
                default:
                    var items = ItemManager.Instance.GetItems();
                    return View(items);
            }
        }
        public ActionResult Additem()
        {
            return View(new Item());
        }

        [HttpPost]
        public ActionResult AddItem(Item t)
        {
            ItemManager.Instance.CreateItem(t);
            return RedirectToDefaultRoute();
        }
        public ActionResult GetMessage(int MessageId)
        {
            var item = ItemManager.Instance.GetItem(MessageId);
            return View(item);
        }
    }
}
