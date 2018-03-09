using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RSS_LogicEngine
{
    class Update_Manager
    {
        /***** Begin Singleton Code *****/
        private static Update_Manager instance;
        // Private constructor.  We are the only class
        // allowed to construct a Feed_Update_manager.
        private Update_Manager() => Initialize_Timer();
        // Get_Instance() returns the singleton instance
        public static Update_Manager Get_Instance()
        {
            if (instance == null)
                instance = new Update_Manager();
            return instance;
        }
        /***** End Singleton Code *****/

        /***** Instance code below this point *****/
        Timer t;
        private void Initialize_Timer()
        {
            // -1, -1 so that timer event never triggers.
            AutoResetEvent auto_event = new AutoResetEvent(false);
            t = new Timer(this.On_Update, auto_event, -1, -1);
        }
        // Update_Tasks contains tasks to run on when feeds are updated
        public event EventHandler Update_Tasks;
        private void On_Update(Object state_info)
            => Update_Tasks?.Invoke(this, new EventArgs());
        public void Set_Update_Period(int seconds)
        {
            TimeSpan ts = new TimeSpan(0, 0, seconds);
            t.Change(ts, ts);
        }
    }
}
