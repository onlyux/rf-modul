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

using DotNetNuke.Data;
using DotNetNuke.Framework;
using System.Collections.Generic;
using Tarhej.Dnn.Tarhej.Dnn.ContactModule.Models;

namespace Tarhej.Dnn.Tarhej.Dnn.ContactModule.Components
{
    internal interface IMessageManager
    {
        void CreateItem(Message m);
        void DeleteItem(int MessageId);
        void DeleteItem(Message m);
        IEnumerable<Message> GetItems();
        Message GetItem(int MessageId);
        void UpdateItem(Message m);
    }

    internal class MessageManager : ServiceLocator<IMessageManager, MessageManager>, IMessageManager
    {
        public void CreateItem(Message m)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Message>();
                rep.Insert(m);
            }
        }

        public void DeleteItem(int MessageId)
        {
            var t = GetItem(MessageId);
            DeleteItem(t);
        }

        public void DeleteItem(Message m)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Message>();
                rep.Delete(m);
            }
        }

        public IEnumerable<Message> GetItems()
        {
            IEnumerable<Message> m;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Message>();
                m = rep.Get();
            }
            return m;
        }

        public Message GetItem(int MessageId)
        {
            Message t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Message>();
                t = rep.GetById(MessageId);
            }
            return t;
        }

        public void UpdateItem(Message m)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Message>();
                rep.Update(m);
            }
        }

        protected override System.Func<IMessageManager> GetFactory()
        {
            return () => new MessageManager();
        }
    }
}
