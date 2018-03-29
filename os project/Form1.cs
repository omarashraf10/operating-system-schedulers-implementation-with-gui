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
        int num=0;
        private void button1_Click(object sender, EventArgs e)
        {
           
            count_click++;
            
            int temparrival = Int32.Parse(textBox4.Text);
            int tempburst = Int32.Parse(textBox2.Text);
            int temppriority;

            process tempprocess = new process();
            tempprocess.arrival_time = temparrival;
            tempprocess.burst_time = tempburst;
            tempprocess.name = "P"+ (num).ToString();
            label10.Text = "P" + (num + 1).ToString();
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

            num++;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            num = 1;
            label5.Enabled = false;
            textBox3.Enabled = false;
            num_process = Int32.Parse(textBox1.Text);
            textBox4.Visible = true;
            textBox2.Visible = true;
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            confirm.Enabled = false;
            label10.Text = "P1";

            

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
            panel1.AutoScroll = false;
            panel1.VerticalScroll.Enabled = false;
            panel1.VerticalScroll.Visible = false;
            panel1.VerticalScroll.Maximum = 0;
            panel1.AutoScroll = true;
            //int newWidth = 2000;
            //panel1.MaximumSize = new Size(newWidth, panel1.Height);
            //panel1.Size = new Size(newWidth, panel1.Height);
            button2.Enabled = false;
            SolidBrush sbwhite = new SolidBrush(Color.Green);
            SolidBrush sblack = new SolidBrush(Color.Black);
            SolidBrush sbyellow = new SolidBrush(Color.Yellow);
            Graphics g = panel1.CreateGraphics();
            FontFamily ff = new FontFamily("Arial");
            System.Drawing.Font font = new System.Drawing.Font(ff, 10);
            /*
            for (int i = 0; i < num_process; i++)
            {
                if (i % num_process == 0)
                {
                    g.FillRectangle(sbred, 20 * i, 20, 100, 50);
                    g.DrawString("P1", font, sbwhite, new PointF(20 * i + 40, 30));
                }
                else if (i % num_process == 1)
                {
                    g.FillRectangle(sbgreen, 20 * (i - 1) + 100, 20, 100, 50);
                    g.DrawString("P2", font, sbwhite, new PointF(20 * (i-1)+100 + 40, 30));

                }
                else if (i % num_process == 2)
                {
                    g.FillRectangle(sbblue, 20 * (i - 2) + 100 + 100, 20, 100, 50);
                    g.DrawString("P3", font, sbwhite, new PointF(20 * (i-2) +100+100+ 40, 30));

                }

                else if (i % num_process == 3)
                {
                    g.FillRectangle(sbpink, 20 * (i - 3) + 100 + 100+100, 20, 100, 50);
                    g.DrawString("P4", font, sbwhite, new PointF(20 * (i - 3) +100+ 100 + 100 + 40, 30));

                }

           }
            */


                button3.Visible = true;
            if (comboBox1.Text == "FCFS")
            {

                int flag;
                int t = 1;
                int mintime;
                int fsum = 0;
                int index = 0;
                int n = num_process;
                int x = 20;
                int y = 1;
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
                    {
                        t++;
                        continue;
                    }
                    if(y%2!=0)
                    g.FillRectangle(sbwhite, x, 20, 100, 50);
                    else
                    g.FillRectangle(sbyellow, x, 20, 100, 50);

                    g.DrawString((t - 1).ToString(), font, sblack, new PointF(x + 5, 30));
                    g.DrawString(processes[index].name, font, sblack, new PointF(x + 45, 30));
                    g.DrawString((t-1+processes[index].burst_time).ToString(), font, sblack, new PointF(x + 80, 30));

                    t += processes[index].burst_time;
                    processes[index].waiting = t - processes[index].burst_time - processes[index].arrival_time - 1;
                    fsum += processes[index].waiting;

                    x += 100;
                    y++;
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
                  int t = 1;
                  int flag;
                  int burst_time = 100000;
                  process minprocess=processes[0];
                  int sjsum = 0;
                  int index=0;
                  int n = num_process;
                  int x = 20;
                  int y = 1;
                  for (int j = 0; j < num_process;j++ )
                  {
                      flag = 0;
                      for (int i = 0; i < n; i++)
                      {
                          if (processes[i].arrival_time < t)
                          {
                              if (processes[i].burst_time < burst_time)
                              {
                                  flag++;
                                  minprocess = processes[i];
                                  burst_time = processes[i].burst_time;
                                  index = i;
                              }
                          }

                      }
                      if (flag == 0)
                      {
                          t++;
                          continue;
                      }
                      if (y % 2 != 0)
                          g.FillRectangle(sbwhite, x, 20, 100, 50);
                      else
                          g.FillRectangle(sbyellow, x, 20, 100, 50);

                      g.DrawString((t - 1).ToString(), font, sblack, new PointF(x + 5, 30));
                      g.DrawString(processes[index].name, font, sblack, new PointF(x + 45, 30));
                      g.DrawString((t - 1 + processes[index].burst_time).ToString(), font, sblack, new PointF(x + 80, 30));

                      t += processes[index].burst_time;
                      processes[index].waiting = t - processes[index].burst_time - processes[index].arrival_time - 1;
                      sjsum += processes[index].waiting;

                      x += 100;
                      y++;
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
                int t = 1;
                int flag;
                int priority = 10000000;
                process bestprocess = processes[0];
                int psum = 0;
                int index = 0;
                int n = num_process;
                int x = 20;
                int y = 1;

                for (int j = 0; j < num_process; j++)
                {
                    flag = 0;
                    for (int i = 0; i < n; i++)
                    {
                        if (processes[i].arrival_time <t)
                       {
                            if (processes[i].priority < priority)
                            {
                                flag++;
                                bestprocess = processes[i];
                                priority = processes[i].priority;
                                index = i;
                            }
                        }

                    }
                    if (flag == 0)
                    {
                        t++;
                        continue;
                    }

                    if (y % 2 != 0)
                        g.FillRectangle(sbwhite, x, 20, 100, 50);
                    else
                        g.FillRectangle(sbyellow, x, 20, 100, 50);

                    g.DrawString((t - 1).ToString(), font, sblack, new PointF(x + 5, 30));
                    g.DrawString(processes[index].name, font, sblack, new PointF(x + 45, 30));
                    g.DrawString((t - 1 + processes[index].burst_time).ToString(), font, sblack, new PointF(x + 80, 30));

                    t += processes[index].burst_time;
                    processes[index].waiting = t - processes[index].burst_time - processes[index].arrival_time - 1;
                    psum += processes[index].waiting;

                    x += 100;
                    y++;
                    
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
                int remaining_time = 10000000;
                process minprocess = processes[0];
                int sjsum = 0;
                int index = -1;
                int newindex = 0;
                int n = num_process;
                int inturrupt = 0 ;
                int x = 20;
                int y = 1;
                int flagcount=9887;//عشان لما بروسيس تخلف اخليه بصفر واخلى الاندكس ميساويش النيو اندكس
                int flag2 = 0;//عشان ارسم اول بروسيس
                int flag = 0;// يعنى مفيش بروسيس موجودة
                while(n>0)
                {
                    flag = 0;
                    /* بجيب هنا البروسيس اللى جاهزة تخش*/
                    for (int i = 0; i < n; i++)
                    {
                        if (processes[i].arrival_time <t)
                        {
                            flag++;
                            if (processes[i].remaining_time < remaining_time)
                            {
                                   
                                    minprocess = processes[i];
                                    remaining_time = processes[i].remaining_time;
                                    newindex = i;
                            }
                            
                        }
                    }
                    /*لو لقيت الفلاج بصفر يبقى مفيش ولا بروسيس جاهزة وهزود التايم واروح اللفة التانية*/
                    if (flag == 0)
                    {
                        t++;
                        continue;
                    }
                    /*دا عشان ارسم اول بروسيس ومش هخش هنا تانى*/
                    if (flag2 == 0)
                    {
                        flag2++;
                            if (y % 2 != 0)
                                g.FillRectangle(sbwhite, x, 20, 100, 50);
                            else
                                g.FillRectangle(sbyellow, x, 20, 100, 50);

                            g.DrawString((t - 1).ToString(), font, sblack, new PointF(x + 5, 30));
                            g.DrawString(processes[newindex].name, font, sblack, new PointF(x + 45, 30));
                    }
                    
                    /*لو رحت لبروسيس جديدة هكمل رسم القديمة وابدأ ارسم الجديدة*/
                    if (newindex != index)
                    {
                        if (inturrupt > 0)
                        {
                                g.DrawString((t - 1).ToString(), font, sblack, new PointF(x + 80, 30));
                                x += 100;
                                y++;
                                if (y % 2 != 0)
                                    g.FillRectangle(sbwhite, x, 20, 100, 50);
                                else
                                    g.FillRectangle(sbyellow, x, 20, 100, 50);

                                g.DrawString((t - 1).ToString(), font, sblack, new PointF(x + 5, 30));
                                g.DrawString(processes[newindex].name, font, sblack, new PointF(x + 45, 30));
                        }
                        inturrupt++;
                    }
                   
                    processes[newindex].remaining_time--;
                    if (processes[newindex].remaining_time <= 0)
                   {
                       processes[newindex].waiting = t - processes[newindex].arrival_time - processes[newindex].burst_time;
                       minprocess.waiting = processes[newindex].waiting;
                       for (int i = newindex; i < n - 1; i++)
                       {
                           processes[i] = processes[i + 1];
                       }
                       n--;
                      newindex=0;
                      flagcount = 0;
                        if (n == 0)
                       {
                           g.DrawString(t.ToString(), font, sblack, new PointF(x + 80, 30));
                       }
                       

                       sjsum += minprocess.waiting;
                   }
                   t++;
                   if (flagcount != 0)
                       index = newindex;
                   else
                       index = 1000000;

                   flagcount++;
                   remaining_time = 100000;

                }
                label9.Text = ((float)sjsum / num_process).ToString();


            }


            else if (comboBox1.Text == "Priority (Preemptive)")
            {
                int t = 1;
                int priority = 1000000;
                process minprocess = processes[0];
                int pjsum = 0;
                int index = -1;
                int newindex = 0;
                int n = num_process;
                int inturrupt = 0;
                int x = 20;
                int y = 1;
                int flagcount = 9887;//عشان لما بروسيس تخلف اخليه بصفر واخلى الاندكس ميساويش النيو اندكس
                int flag2 = 0;//عشان ارسم اول بروسيس
                int flag = 0;// يعنى مفيش بروسيس موجودة
                
                while (n > 0)
                {
                    flag = 0;
                    /* بجيب هنا البروسيس اللى جاهزة تخش*/
                    for (int i = 0; i < n; i++)
                    {
                        if (processes[i].arrival_time < t)
                        {
                            flag++;   
                            if (processes[i].priority < priority)
                            {
                                minprocess = processes[i];
                                priority = processes[i].priority;
                                newindex = i;
                            }
                        }

                    }
                    /*لو لقيت الفلاج بصفر يبقى مفيش ولا بروسيس جاهزة وهزود التايم واروح اللفة التانية*/
                    if (flag == 0)
                    {
                        t++;
                        continue;
                    }
                    /*دا عشان ارسم اول بروسيس ومش هخش هنا تانى*/
                    if (flag2 == 0)
                    {
                        flag2++;
                        if (y % 2 != 0)
                            g.FillRectangle(sbwhite, x, 20, 100, 50);
                        else
                            g.FillRectangle(sbyellow, x, 20, 100, 50);

                        g.DrawString((t - 1).ToString(), font, sblack, new PointF(x + 5, 30));
                        g.DrawString(processes[newindex].name, font, sblack, new PointF(x + 45, 30));
                    }

                    /*لو رحت لبروسيس جديدة هكمل رسم القديمة وابدأ ارسم الجديدة*/
                    if (newindex != index)
                    {
                        if (inturrupt > 0)
                        {
                                g.DrawString((t - 1).ToString(), font, sblack, new PointF(x + 80, 30));
                                x += 100;
                                y++;
                                if (y % 2 != 0)
                                    g.FillRectangle(sbwhite, x, 20, 100, 50);
                                else
                                    g.FillRectangle(sbyellow, x, 20, 100, 50);

                                g.DrawString((t - 1).ToString(), font, sblack, new PointF(x + 5, 30));
                                g.DrawString(processes[newindex].name, font, sblack, new PointF(x + 45, 30));
                        }
                        inturrupt++;
                    }
                   
                    processes[newindex].remaining_time--;
                    if (processes[newindex].remaining_time <= 0)
                   {
                       processes[newindex].waiting = t - processes[newindex].arrival_time - processes[newindex].burst_time;
                       minprocess.waiting = processes[newindex].waiting;
                       for (int i = newindex; i < n - 1; i++)
                       {
                           processes[i] = processes[i + 1];
                       }
                       n--;
                      newindex=0;
                      flagcount = 0;
                        if (n == 0)
                       {
                           g.DrawString(t.ToString(), font, sblack, new PointF(x + 80, 30));
                       }
                       

                       pjsum += minprocess.waiting;
                   }
                   t++;
                   if (flagcount != 0)
                       index = newindex;
                   else
                       index = 1000000;

                   flagcount++;
                   priority = 100000;

                }
                
                label9.Text = ((float)pjsum / num_process).ToString();


            }





            else if (comboBox1.Text == "Round Robin")
            {
                int flag = 0;
                int t = 0;
                int rrsum = 0;
                int n = num_process;
                int x = 20;
                int y = 1;
                int numR;
                int prevnumR=0;
                List<process> Sortedprocesses = processes.OrderBy(o => o.arrival_time).ToList();
               while (n > 0)
               {
                flag = 0;
                numR = 0;
                    for (int i = 0; i < n; i++)
                        if (Sortedprocesses[i].arrival_time <= t)
                        {
                            flag++;
                            numR++;
                        }
                   if (flag == 0)
                             t++;

                   if (numR > prevnumR)
                   {
                       for (int i = numR - 1; i >= prevnumR; i--)
                       {
                           process tp = new process();
                           tp = Sortedprocesses[i];
                           for (int j = numR-1; j > 0; j--)
                           {
                               Sortedprocesses[j] = Sortedprocesses[j - 1];
                           
                           }
                           Sortedprocesses[0] = tp;
                       }
                   }


                for (int i = 0; i < numR; i++)
                {
                    if (Sortedprocesses[i].remaining_time > quantum)
                        {
                            Sortedprocesses[i].remaining_time -= quantum;
        
                            if (y % 2 != 0)
                                g.FillRectangle(sbwhite, x, 20, 100, 50);
                            else
                                g.FillRectangle(sbyellow, x, 20, 100, 50);
                                
                            g.DrawString((t).ToString(), font, sblack, new PointF(x + 5, 30));
                            g.DrawString(Sortedprocesses[i].name, font, sblack, new PointF(x + 45, 30));
                            g.DrawString((t + quantum).ToString(), font, sblack, new PointF(x + 80, 30));
                            t += quantum;
                 
                                
                                
                                
                            x += 100;
                            y++;
                        }
                        else
                        {
                                
                                
                               
                            if (y % 2 != 0)
                                g.FillRectangle(sbwhite, x, 20, 100, 50);
                            else
                                g.FillRectangle(sbyellow, x, 20, 100, 50);
                            g.DrawString((t).ToString(), font, sblack, new PointF(x + 5, 30));
                            g.DrawString(Sortedprocesses[i].name, font, sblack, new PointF(x + 45, 30));
                            g.DrawString((t+ Sortedprocesses[i].remaining_time).ToString(), font, sblack, new PointF(x + 80, 30));

                            t += Sortedprocesses[i].remaining_time;
                            Sortedprocesses[i].waiting = t - Sortedprocesses[i].burst_time - Sortedprocesses[i].arrival_time;
                            rrsum += Sortedprocesses[i].waiting;
                            Sortedprocesses[i].remaining_time = 0;
                            x += 100;
                            y++;
                        }

                        


                }

                for (int i = 0; i < numR; i++)
                {
                    if (Sortedprocesses[i].remaining_time <= 0)
                    {
                        Sortedprocesses.RemoveAt(i);
                        n--;
                        i--;
                        numR--;
                    }
                }

                prevnumR = numR;
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
            panel1.Invalidate();
            num = 0;
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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
