using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IFocusDAO
    {
        List<Focus> GetFocuses();
        Focus GetFocus(int id);
    }
}