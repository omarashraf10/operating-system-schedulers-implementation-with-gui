using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace os_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int num_process;
        List<process> processes = new List<process>();
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Priority (Preemptive)" || comboBox1.Text == "Priority (Non Preemptive)") 
            {
                priority_label.Visible = true;
                priority_text.Visible = true;
            }
            else
            {
                priority_label.Visible = false;
                priority_text.Visible = false;
            }

            if (comboBox1.Text == "Round Robin")
            {
                textBox3.Visible = true;
                label5.Visible = true;
            }
            else
            {
                textBox3.Visible = false;
                label5.Visible = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int temparrival = Int32.Parse(textBox4.Text);
            int tempburst = Int32.Parse(textBox2.Text);
            

            process tempprocess = new process();
            tempprocess.arrival_time = temparrival;
            tempprocess.burst_time = tempburst;

            if (comboBox1.Text == "Priority (Preemptive)" || comboBox1.Text == "Priority (Non Preemptive)")
            {
                int temppriority = Int32.Parse(priority_text.Text);
                tempprocess.priority = temppriority;
            }

            if (comboBox1.Text == "SJF (Preemptive)" || comboBox1.Text == "Priority (Preemptive)")
            {
                tempprocess.remaining_time = tempprocess.burst_time;
            }


            processes.Add(tempprocess);
            textBox4.Text = "";
            textBox2.Text = "";
            priority_text.Text = "";

        }
        private void button3_Click(object sender, EventArgs e)
        {
            num_process = Int32.Parse(textBox1.Text);

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int end = 0;
            int sum = 0;
            if (comboBox1.Text == "FCFS")
            {

                for (int i = 0; i < num_process; i++)
                {
                    if (end > processes[i].arrival_time)
                        processes[i].waiting = end - processes[i].arrival_time;
                    else
                        processes[i].waiting = 0;

                    processes[i].real_end = end + processes[i].burst_time;
                    end = processes[i].real_end;
                    sum += processes[i].waiting;
                }

                textBox5.Text = ((float)sum / num_process).ToString();
            }
            else if (comboBox1.Text == "SJF (Non Preemptive)")
            {
                int cpuend = 0;
                int burst_time = 100000;
                  process minprocess=processes[0];
                  int sjsum = 0;
                  int index=0;
                  int n = num_process; 
                  for (int j = 0; j < num_process;j++ )
                  {
  
                      for (int i = 0; i < n; i++)
                      {
                          if (processes[i].arrival_time <= cpuend)
                          {
                              if (processes[i].burst_time < burst_time)
                              {
                                  minprocess = processes[i];
                                  burst_time = processes[i].burst_time;
                                  index = i;
                              }
                          }

                      }
                      
                      if (cpuend >minprocess.arrival_time)
                          minprocess.waiting = cpuend - minprocess.arrival_time;
                      else
                          minprocess.waiting = 0;


                      cpuend += minprocess.burst_time;
                      sjsum += minprocess.waiting;
                      for (int i = index; i < n-1; i++)
                      {
                        processes[i] = processes[i + 1];
                      }

                       burst_time = 100000;

                          n--;
                  }
                  textBox5.Text = ((float)sjsum/num_process).ToString();
            
         
            }
            else if (comboBox1.Text == "Priority (Non Preemptive)")
            {
                int cpuend = 0;
                int priority = 100000;
                process bestprocess = processes[0];
                int psum = 0;
                int index = 0;
                int n = num_process;
                for (int j = 0; j < num_process; j++)
                {

                    for (int i = 0; i < n; i++)
                    {
                        if (processes[i].arrival_time <= cpuend)
                        {
                            if (processes[i].priority < priority)
                            {
                                bestprocess = processes[i];
                                priority = processes[i].priority;
                                index = i;
                            }
                        }

                    }

                    if (cpuend > bestprocess.arrival_time)
                         bestprocess.waiting = cpuend - bestprocess.arrival_time;
                    else
                        bestprocess.waiting = 0;

                    cpuend += bestprocess.burst_time;
                    psum += bestprocess.waiting;
                    for (int i = index; i < n - 1; i++)
                    {
                        processes[i] = processes[i + 1];
                    }

                    priority = 100000;

                    n--;
                }
                textBox5.Text = ((float)psum / num_process).ToString();

            }

            else if (comboBox1.Text == "SJF (Preemptive)")
            {
                int t =1;
                int remaining_time = 100000;
                process minprocess = processes[0];
                int sjsum = 0;
                int index = 0;
                int n = num_process;
                while(n>0)
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (processes[i].arrival_time <t)
                        {
                            if (processes[i].remaining_time < remaining_time)
                            {
                                minprocess = processes[i];
                                remaining_time = processes[i].remaining_time;
                                index = i;
                            }
                        }

                    }
                    /*
                    if (cpuend > minprocess.arrival_time)
                        minprocess.waiting = cpuend - minprocess.arrival_time;
                    else
                        minprocess.waiting = 0;
                    */

                  //  cpuend += minprocess.burst_time;


                    processes[index].remaining_time--;
                    if (processes[index].remaining_time <= 0)
                   {
                       processes[index].waiting = t - processes[index].arrival_time - processes[index].burst_time;
                       minprocess.waiting = processes[index].waiting;
                       for (int i = index; i < n - 1; i++)
                       {
                           processes[i] = processes[i + 1];
                       }
                       n--;

                       remaining_time = 100000;

                       sjsum += minprocess.waiting;
                   }
                   t++;
                }
                textBox5.Text = ((float)sjsum / num_process).ToString();


            }


            else if (comboBox1.Text == "Priority (Preemptive)")
            {
                int t = 1;
                int priority = 1000000;
                process minprocess = processes[0];
                int sjsum = 0;
                int index = 0;
                int n = num_process;
                while (n > 0)
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (processes[i].arrival_time < t)
                        {
                            if (processes[i].priority < priority)
                            {
                                minprocess = processes[i];
                                priority = processes[i].priority;
                                index = i;
                            }
                        }

                    }
                    /*
                    if (cpuend > minprocess.arrival_time)
                        minprocess.waiting = cpuend - minprocess.arrival_time;
                    else
                        minprocess.waiting = 0;
                    */

                    //  cpuend += minprocess.burst_time;


                    processes[index].remaining_time--;
                    if (processes[index].remaining_time <= 0)
                    {
                        processes[index].waiting = t - processes[index].arrival_time - processes[index].burst_time;
                        minprocess.waiting = processes[index].waiting;
                        for (int i = index; i < n - 1; i++)
                        {
                            processes[i] = processes[i + 1];
                        }
                        n--;

                        priority = 100000;

                        sjsum += minprocess.waiting;
                    }
                    t++;
                }
                textBox5.Text = ((float)sjsum / num_process).ToString();


            }




        }

        private void priority_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
