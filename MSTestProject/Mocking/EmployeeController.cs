using Microsoft.EntityFrameworkCore;

namespace MSTestProject.Mocking
{
    public class EmployeeController
    {
        private IEmployeeStorage _storage;
        public EmployeeController(IEmployeeStorage storage = null)
        {
            _storage = storage ?? new EmployeeStorage();
        }

        public ActionResult DeleteEmployee(int id)
        {
            _storage.DeleteEmployee(id);

            return RedirectToAction("Eployees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }
    public class RedirectResult : ActionResult { }
}
