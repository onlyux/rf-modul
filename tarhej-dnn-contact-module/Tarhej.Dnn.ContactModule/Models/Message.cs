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

using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
using System;
using System.Web.Caching;

namespace Tarhej.Dnn.Tarhej.Dnn.ContactModule.Models
{
    [TableName("ContactModule_Items")]
    //setup the primary key for table
    [PrimaryKey("MessageId", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("Messages", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("ModuleId")]
    public class Message
    {
        public int MessageId { get; set; } = -1;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Subject { get; set; }

        public string Email { get; set; }

        public string MessageContent { get; set; }

        public string Product { get; set; }

        public DateTime ContactDate { get;set; } = DateTime.UtcNow;

        public string Status { get; set; } = "nem megválaszolt";

        public int ModuleId { get; set; }
    }
}
