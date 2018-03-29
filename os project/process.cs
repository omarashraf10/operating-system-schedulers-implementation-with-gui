using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace os_project
{
    class process
    {
        public int arrival_time
        {
            get;
            set; 
        }
        public int burst_time
        {
            get;
            set;
        }
        public int real_end
        {
            get;
            set;
        }

        public int remaining_time
        {
            get;
            set;
        }

        public int waiting
        {
            get;
            set;
        }
        public int priority
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public process(){
        
        }
        public process(process p)
        {
            arrival_time = p.arrival_time; priority = p.priority; name = p.name; burst_time = p.burst_time; waiting = p.waiting; remaining_time = p.remaining_time; real_end = p.real_end;
        }
    }
}
