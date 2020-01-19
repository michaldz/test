using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interview.Models
{
    public interface IIdable
    {
        string Id { get; set; }
    }
}