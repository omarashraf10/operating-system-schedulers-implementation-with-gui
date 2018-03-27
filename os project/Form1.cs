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
        int quantum;
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Priority (Preemptive)" || comboBox1.Text == "SJF (Preemptive)" || comboBox1.Text == "SJF (Non Preemptive)"
                || comboBox1.Text == "Priority (Non Preemptive)" || comboBox1.Text == "Round Robin" || comboBox1.Text == "FCFS"
           )
                confirm.Enabled = true;

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
                textBox3.Enabled = true;

                label5.Visible = true;
                label5.Enabled = true;

            }
            else
            {
                textBox3.Visible = false;
                label5.Visible = false;
            }

        }
        int count_click = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            count_click++;
            label10.Text = (count_click+1).ToString();
            int temparrival = Int32.Parse(textBox4.Text);
            int tempburst = Int32.Parse(textBox2.Text);
            int temppriority;

            process tempprocess = new process();
            tempprocess.arrival_time = temparrival;
            tempprocess.burst_time = tempburst;

            if (comboBox1.Text == "Priority (Preemptive)" || comboBox1.Text == "Priority (Non Preemptive)")
            {
                temppriority = Int32.Parse(priority_text.Text);
                tempprocess.priority = temppriority;
            }

            if (comboBox1.Text == "SJF (Preemptive)" || comboBox1.Text == "Priority (Preemptive)" || comboBox1.Text == "Round Robin")
            {
                tempprocess.remaining_time = tempprocess.burst_time;
            }

            if (comboBox1.Text == "Round Robin")
                quantum = Int32.Parse(textBox3.Text);

            processes.Add(tempprocess);
            textBox4.Text = "";
            textBox2.Text = "";
            priority_text.Text = "";
            if (count_click == num_process)
            {
                button2.Enabled = true;
                groupBox1.Visible = false;
            }
     

        }
        private void button3_Click(object sender, EventArgs e)
        {
            label5.Enabled = false;
            textBox3.Enabled = false;
            num_process = Int32.Parse(textBox1.Text);
            textBox4.Visible = true;
            textBox2.Visible = true;
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            confirm.Enabled = false;
            label10.Text = "1";


        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(textBox4.Text, out n);
            bool isNumeric2 = int.TryParse(textBox2.Text, out n);
            if (isNumeric && isNumeric2) button1.Enabled = true;
            else
            {
                button1.Enabled = false;

            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(textBox4.Text, out n);
            bool isNumeric2 = int.TryParse(textBox2.Text, out n);
            if (isNumeric && isNumeric2) button1.Enabled = true;
            else
            {
                button1.Enabled = false;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Visible = true;

            
            int end = 0;
            int sum = 0;
            if (comboBox1.Text == "FCFS")
            {

                int flag;
                int t = 1;
                int mintime;
                int fsum = 0;
                int index = 0;
                int n = num_process;
                while (n > 0)
                {
                    flag = 0;
                    mintime = 1000000;
                    for (int i = 0; i < n; i++)
                    {
                        if (processes[i].arrival_time < t && processes[i].arrival_time < mintime)
                        {
                            flag++;
                            mintime = processes[i].arrival_time;
                            index = i;
                        }
                    }

                    if (flag == 0)
                        t++;

                    t += processes[index].burst_time;
                    processes[index].waiting = t - processes[index].burst_time - processes[index].arrival_time - 1;
                    fsum += processes[index].waiting;

                        if (processes[index].remaining_time == 0)
                        {
                            for (int j = index; j < n - 1; j++)
                            {
                                processes[j] = processes[j + 1];
                            }
                            n--;
                        }

                }
                label9.Text = ((float)fsum / num_process).ToString();
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
                  label9.Text = ((float)sjsum/num_process).ToString();
            
         
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
                label9.Text = ((float)psum / num_process).ToString();

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
                label9.Text = ((float)sjsum / num_process).ToString();


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
                label9.Text = ((float)sjsum / num_process).ToString();


            }



            else if (comboBox1.Text == "Round Robin")
            {
                int flag = 0;
                int t = 1;
                int rrsum = 0;
                int n = num_process;
                while (n > 0)
                {
                    for (int i = 0; i < n; i++)
                    {

                        if (processes[i].arrival_time < t)
                        {
                            flag++;
                            if (processes[i].remaining_time > quantum)
                            {
                                processes[i].remaining_time -= quantum;
                                t += quantum;
                            }
                            else
                            {
                                t += processes[i].remaining_time;
                                processes[i].remaining_time = 0;
                                processes[i].waiting = t - processes[i].burst_time - processes[i].arrival_time - 1;
                                rrsum += processes[i].waiting;

                            }

                        }


                    }

                    if (flag == 0)
                        t++;

                    for (int i = 0; i < n; i++)
                    {
                        if (processes[i].remaining_time == 0)
                        {
                            for (int j = i; j < n - 1; j++)
                            {
                                processes[j] = processes[j + 1];
                            }
                            n--;
                            i--;
                        }

                    }


                }

                label9.Text = ((float)rrsum / num_process).ToString();
            }


    }


    










        private void priority_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(textBox1.Text, out n);
            if (isNumeric)
                confirm.Visible = true;
            else confirm.Visible = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            priority_label.Visible = false;
            priority_text.Visible = false;
            textBox3.Visible = false;
            label5.Visible = false;
            confirm.Enabled = false;
            button2.Enabled = false;
            textBox4.Visible = false;
            textBox2.Visible=false;
            processes.Clear();
            textBox1.Text = "0";
            comboBox1.Text = "";
            count_click = 0;
            num_process = Int32.Parse(textBox1.Text);
            groupBox1.Visible = true;
            label10.Text="";
            label9.Text = "";
            textBox1.Enabled = true;
            comboBox1.Enabled = true;

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
