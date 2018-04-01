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
        Bitmap image;
        private void Form1_Load(object sender, EventArgs e)
        {
            image = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Priority (Preemptive)" || comboBox1.Text == "SJF (Preemptive)" || comboBox1.Text == "SJF (Non Preemptive)"
                || comboBox1.Text == "Priority (Non Preemptive)" || comboBox1.Text == "Round Robin" || comboBox1.Text == "FCFS"
           )
                confirm.Enabled = true;

            if (comboBox1.Text == "Priority (Preemptive)" || comboBox1.Text == "Priority (Non Preemptive)") 
            {
              //  priority_label.Visible = true;
                //priority_text.Visible = true;
            }
            else
            {
              //  priority_label.Visible = false;
             //   priority_text.Visible = false;
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

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button1.Visible = true;

            label5.Enabled = false;
            textBox3.Enabled = false;
            num_process = Int32.Parse(textBox1.Text);
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            confirm.Enabled = false;
            datagridview1.AllowUserToAddRows = false;
            datagridview1.Visible = true;
            datagridview1.DataSource = null;
            datagridview1.Columns.Clear();  //Just make sure things are blank.
            datagridview1.Columns.Add("Column1", "Name");
            datagridview1.Columns.Add("Column2", "Arrival time");
            datagridview1.Columns.Add("Column3", "Burst Time");

            if (comboBox1.Text == "Priority (Preemptive)" || comboBox1.Text == "Priority (Non Preemptive)")
                datagridview1.Columns.Add("Column3", "Priority");


            datagridview1.Rows.Clear();
            for (int i = 1; i <= num_process; i++)
            {
                datagridview1.Rows.Add("P" + (i.ToString()));
            }
            datagridview1.EditMode = DataGridViewEditMode.EditOnKeystroke;


        }

       /* private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(textBox4.Text, out n);
            bool isNumeric2 = int.TryParse(textBox2.Text, out n);
            if (isNumeric && isNumeric2) button1.Enabled = true;
            else
            {
                button1.Enabled = false;

            }
        }*/

       /* private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(textBox4.Text, out n);
            bool isNumeric2 = int.TryParse(textBox2.Text, out n);
            if (isNumeric && isNumeric2) button1.Enabled = true;
            else
            {
                button1.Enabled = false;

            }
        }*/
        Graphics g;


        private void button2_Click(object sender, EventArgs e)
        {
            processes.Clear();
            label11.Visible = true;
            label8.Visible = true;
            for (int i = 0; i < num_process; i++)
            {

                    button2.Enabled = true;

                    int temparrival = Int32.Parse(datagridview1.Rows[i].Cells[1].Value.ToString());
                    int tempburst = Int32.Parse(datagridview1.Rows[i].Cells[2].Value.ToString());

                    int temppriority;

                    process tempprocess = new process();
                    tempprocess.arrival_time = temparrival;
                    tempprocess.burst_time = tempburst;
                    tempprocess.name = datagridview1.Rows[i].Cells[0].Value.ToString();
                    //tempprocess.name = datagridview1[i, 0].Value.ToString();
                    if (comboBox1.Text == "Priority (Preemptive)" || comboBox1.Text == "Priority (Non Preemptive)")
                    {
                        temppriority = Int32.Parse(datagridview1.Rows[i].Cells[3].Value.ToString());
                        tempprocess.priority = temppriority;
                    }

                    if (comboBox1.Text == "SJF (Preemptive)" || comboBox1.Text == "Priority (Preemptive)" || comboBox1.Text == "Round Robin")
                    {
                        tempprocess.remaining_time = tempprocess.burst_time;
                    }

                    if (comboBox1.Text == "Round Robin")
                        quantum = Int32.Parse(textBox3.Text);

                    processes.Add(tempprocess);
                    // textBox4.Text = "";
                    //textBox2.Text = "";
                    // priority_text.Text = "";
                    /*if (count_click == num_process)
                    {
                        groupBox1.Visible = false;
                    }*/
            }
            

            button2.Enabled = false;
            SolidBrush sbwhite = new SolidBrush(Color.Aqua);
            SolidBrush sblack = new SolidBrush(Color.Black);
            SolidBrush sbyellow = new SolidBrush(Color.Yellow);
            FontFamily ff = new FontFamily("Arial");
            System.Drawing.Font font = new System.Drawing.Font(ff, 10);
            System.Drawing.Font bigfont = new System.Drawing.Font(ff, 12);
            button3.Visible = true;


            g = Graphics.FromImage(image);
            g.Clear(Color.WhiteSmoke);
            if (comboBox1.Text == "FCFS")
            {
                int extra = (num_process < 9) ? 100 : (800 / num_process);
                int flag;
                int t = 1;
                int mintime;
                int fsum = 0;
                int index = 0;
                int n = num_process;
                int x = 20;
                int y = 1;
                List<process> Sortedprocesses = processes.OrderBy(o => o.arrival_time).ToList();
                while (n > 0)
                {
                    flag = 0;
                    mintime = 1000000;
                    for (int i = 0; i < n; i++)
                    {
                        if (Sortedprocesses[i].arrival_time < t && Sortedprocesses[i].arrival_time < mintime)
                        {
                            flag++;
                            mintime = Sortedprocesses[i].arrival_time;
                            index = i;
                        }
                    }

                    if (flag == 0)
                    {
                        t++;
                        continue;
                    }
                    if(y%2!=0)
                    g.FillRectangle(sbwhite, x, 20, extra, 50);
                    else
                    g.FillRectangle(sbyellow, x, 20, extra, 50);

                    g.DrawString((t - 1).ToString(), font, sblack, new PointF(x+3 , 80));
                    g.DrawString(Sortedprocesses[index].name, bigfont, sblack, new PointF(x + (extra / 2) - 15, 35));
                    g.DrawString((t - 1 + Sortedprocesses[index].burst_time).ToString(), font, sblack, new PointF(x + (extra - 15), 80));

                    t += Sortedprocesses[index].burst_time;
                    Sortedprocesses[index].waiting = t - Sortedprocesses[index].burst_time - Sortedprocesses[index].arrival_time - 1;
                    fsum += Sortedprocesses[index].waiting;

                    x += extra;
                    y++;
                    if (Sortedprocesses[index].remaining_time == 0)
                        {
                            for (int j = index; j < n - 1; j++)
                            {
                                Sortedprocesses[j] = Sortedprocesses[j + 1];
                            }
                            n--;
                        }
                       
                }

                pictureBox1.Invalidate();
                label9.Text = ((float)fsum / num_process).ToString();
            }


            
            else if (comboBox1.Text == "SJF (Non Preemptive)")
            {
                  int extra = (num_process < 9) ? 100 : (800 / num_process);
                  int t = 1;
                  int flag;
                  int burst_time = 100000;
                  List<process> Sortedprocesses = processes.OrderBy(o => o.arrival_time).ToList();
                  process minprocess = Sortedprocesses[0];
                  int sjsum = 0;
                  int index=0;
                  int n = num_process;
                  int x = 20;
                  int y = 1;
                  while(n>0)
                  {
                      flag = 0;
                      for (int i = 0; i < n; i++)
                      {
                          if (Sortedprocesses[i].arrival_time < t)
                          {
                              flag++;
                              if (Sortedprocesses[i].burst_time < burst_time)
                              {

                                  minprocess = Sortedprocesses[i];
                                  burst_time = Sortedprocesses[i].burst_time;
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
                          g.FillRectangle(sbwhite, x, 20, extra, 50);
                      else
                          g.FillRectangle(sbyellow, x, 20, extra, 50);

                      g.DrawString((t - 1).ToString(), font, sblack, new PointF(x+3 , 80));
                      g.DrawString(Sortedprocesses[index].name, bigfont, sblack, new PointF(x + (extra / 2) - 15, 35));
                      g.DrawString((t - 1 + Sortedprocesses[index].burst_time).ToString(), font, sblack, new PointF(x + extra - 15, 80));



                      t += Sortedprocesses[index].burst_time;
                      Sortedprocesses[index].waiting = t - Sortedprocesses[index].burst_time - Sortedprocesses[index].arrival_time - 1;
                      sjsum += Sortedprocesses[index].waiting;

                      x += extra;
                      y++;
                      for (int i = index; i < n-1; i++)
                      {
                          Sortedprocesses[i] = Sortedprocesses[i + 1];
                      }

                       burst_time = 100000;

                          n--;
                  }
                  pictureBox1.Invalidate();
                  label9.Text = ((float)sjsum/num_process).ToString();
            
         
            }
            else if (comboBox1.Text == "Priority (Non Preemptive)")
            {
                int extra = (num_process < 9) ? 100 : (800 / num_process);
                int t = 1;
                int flag;
                int priority = 10000000;
                List<process> Sortedprocesses = processes.OrderBy(o => o.arrival_time).ToList();
                process bestprocess = Sortedprocesses[0];
                int psum = 0;
                int index = 0;
                int n = num_process;
                int x = 20;
                int y = 1;

                while(n>0)
                {
                    flag = 0;
                    for (int i = 0; i < n; i++)
                    {
                        if (Sortedprocesses[i].arrival_time < t)
                       {
                           flag++;
                           if (Sortedprocesses[i].priority < priority)
                            {
                                bestprocess = Sortedprocesses[i];
                                priority = Sortedprocesses[i].priority;
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
                        g.FillRectangle(sbwhite, x, 20, extra, 50);
                    else
                        g.FillRectangle(sbyellow, x, 20, extra, 50);


                    g.DrawString((t - 1).ToString(), font, sblack, new PointF(x + 3, 80));
                    g.DrawString(Sortedprocesses[index].name, bigfont, sblack, new PointF(x + (extra / 2) - 15, 35));
                    g.DrawString((t - 1 + Sortedprocesses[index].burst_time).ToString(), font, sblack, new PointF(x + extra - 15, 80));
                    t += Sortedprocesses[index].burst_time;
                    Sortedprocesses[index].waiting = t - Sortedprocesses[index].burst_time - Sortedprocesses[index].arrival_time - 1;
                    psum += Sortedprocesses[index].waiting;

                    x += extra;
                    y++;
                    
                    for (int i = index; i < n - 1; i++)
                    {
                        Sortedprocesses[i] = Sortedprocesses[i + 1];
                    }

                    priority = 100000;

                    n--;
                }
                pictureBox1.Invalidate();
                label9.Text = ((float)psum / num_process).ToString();

            }


            else if (comboBox1.Text == "SJF (Preemptive)")
            {
                int t =1;
                int remaining_time = 10000000;
                List<process> Sortedprocesses = processes.OrderBy(o => o.arrival_time).ToList();
                process minprocess = Sortedprocesses[0];
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
                // هعمل الكود مرتين مرة عشان اعرف عدد المستطيلات اللى هرسمها ومرة عشان ارسم الشارت كلها واحسب التايم
                List<process> countlist = new List <process>();
                foreach (process pc in Sortedprocesses)
                {
                    countlist.Add(new process(pc));
                }
                int count=0;
                while(n>0)
                {
                    flag = 0;
                    // بجيب هنا البروسيس اللى جاهزة تخش
                    for (int i = 0; i < n; i++)
                    {
                        if (countlist[i].arrival_time <t)
                        {
                            flag++;
                            if (countlist[i].remaining_time < remaining_time)
                            {
                                    minprocess = countlist[i];
                                    remaining_time = countlist[i].remaining_time;
                                    newindex = i;
                            }
                            
                        }
                    }
                    //لو لقيت الفلاج بصفر يبقى مفيش ولا بروسيس جاهزة وهزود التايم واروح اللفة التانية
                    if (flag == 0)
                    {
                        t++;
                        continue;
                    }
                    //دا عشان ارسم اول بروسيس ومش هخش هنا تانى
                    if (flag2 == 0)
                    {
                        flag2++;
                            count++;
                    }
                    
                    //لو رحت لبروسيس جديدة هكمل رسم القديمة وابدأ ارسم الجديدة
                    if (newindex != index)
                    {
                        if (inturrupt > 0)
                        {
                               count++;
                        }
                        inturrupt++;
                    }

                    countlist[newindex].remaining_time--;
                    if (countlist[newindex].remaining_time <= 0)
                   {
                       countlist[newindex].waiting = t - countlist[newindex].arrival_time - countlist[newindex].burst_time;
                       minprocess.waiting = countlist[newindex].waiting;
                       for (int i = newindex; i < n - 1; i++)
                       {
                           countlist[i] = countlist[i + 1];
                       }
                       n--;
                      newindex=0;
                      flagcount = 0;
                        if (n == 0)
                       {
                       };
                       

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


                int extra = (count < 9) ? 100 : (800 / count);
                t = 1;
                remaining_time = 10000000;
                minprocess = Sortedprocesses[0];
                sjsum = 0;
                index = -1;
                newindex = 0;
                n = num_process;
                inturrupt = 0;
                x = 20;
                y = 1;
                int tend=0;
                flagcount = 9887;//عشان لما بروسيس تخلف اخليه بصفر واخلى الاندكس ميساويش النيو اندكس
                flag2 = 0;//عشان ارسم اول بروسيس
                flag = 0;// يعنى مفيش بروسيس موجودة
                while(n>0)
                {
                    flag = 0;
                    // بجيب هنا البروسيس اللى جاهزة تخش
                    for (int i = 0; i < n; i++)
                    {
                        if (Sortedprocesses[i].arrival_time < t)
                        {
                            flag++;
                            if (Sortedprocesses[i].remaining_time < remaining_time)
                            {

                                minprocess = Sortedprocesses[i];
                                remaining_time = Sortedprocesses[i].remaining_time;
                                    newindex = i;
                            }
                            
                        }
                    }
                    //لو لقيت الفلاج بصفر يبقى مفيش ولا بروسيس جاهزة وهزود التايم واروح اللفة التانية
                    if (flag == 0)
                    {
                        t++;
                        continue;
                    }
                    //دا عشان ارسم اول بروسيس ومش هخش هنا تانى
                    if (flag2 == 0)
                    {
                        flag2++;
                            if (y % 2 != 0)
                                g.FillRectangle(sbwhite, x, 20, extra, 50);
                            else
                                g.FillRectangle(sbyellow, x, 20, extra, 50);

                            g.DrawString((t - 1).ToString(), font, sblack, new PointF(x + 3, 80));
                            g.DrawString(Sortedprocesses[newindex].name, bigfont, sblack, new PointF(x + (extra / 2) - 15, 35));
                    }
              
                    //لو رحت لبروسيس جديدة هكمل رسم القديمة وابدأ ارسم الجديدة
                    if (newindex != index)
                    {
                        if (inturrupt > 0)
                        {
                                g.DrawString((tend).ToString(), font, sblack, new PointF(x + extra - 15, 80));
                                x += extra;
                                y++;
                                if (y % 2 != 0)
                                    g.FillRectangle(sbwhite, x, 20, extra, 50);
                                else
                                    g.FillRectangle(sbyellow, x, 20, extra, 50);
                                g.DrawString((t - 1).ToString(), font, sblack, new PointF(x + 3, 80));
                                g.DrawString(Sortedprocesses[newindex].name, bigfont, sblack, new PointF(x + (extra / 2) - 15, 35));
                        }
                        inturrupt++;
                    }

                    Sortedprocesses[newindex].remaining_time--;
                    if (Sortedprocesses[newindex].remaining_time <= 0)
                    {
                        Sortedprocesses[newindex].waiting = t - Sortedprocesses[newindex].arrival_time - Sortedprocesses[newindex].burst_time;
                        minprocess.waiting = Sortedprocesses[newindex].waiting;
                        tend = t;
                        Sortedprocesses.RemoveAt(newindex);
                        n--;
                        newindex = 0;
                        flagcount = 0;
                        if (n == 0)
                        {
                            g.DrawString(t.ToString(), font, sblack, new PointF(x + extra - 15, 80));
                        }


                        sjsum += minprocess.waiting;
                    }
                    else
                    {
                        tend = t;
                    }
                    t++;
                   if (flagcount != 0)
                       index = newindex;
                   else
                       index = 1000000;

                   flagcount++;
                   remaining_time = 100000;

                }
                pictureBox1.Invalidate();
                label9.Text = ((float)sjsum / num_process).ToString();
            }










            else if (comboBox1.Text == "Priority (Preemptive)")
            {
                int t = 1;
                int priority = 1000000;
                List<process> Sortedprocesses = processes.OrderBy(o => o.arrival_time).ToList();
                process minprocess = Sortedprocesses[0];
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
                List<process> countlist = new List<process>();
                foreach (process pc in Sortedprocesses)
                {
                    countlist.Add(new process(pc));
                }
                int count = 0;
                while (n > 0)
                {
                    flag = 0;
                    // بجيب هنا البروسيس اللى جاهزة تخش
                    for (int i = 0; i < n; i++)
                    {
                        if (countlist[i].arrival_time < t)
                        {
                            flag++;
                            if (countlist[i].priority < priority)
                            {
                                minprocess = countlist[i];
                                priority = countlist[i].priority;
                                newindex = i;
                            }
                        }

                    }
                    //لو لقيت الفلاج بصفر يبقى مفيش ولا بروسيس جاهزة وهزود التايم واروح اللفة التانية
                    if (flag == 0)
                    {
                        t++;
                        continue;
                    }
                    //دا عشان ارسم اول بروسيس ومش هخش هنا تانى
                    if (flag2 == 0)
                    {
                        flag2++;
                        count++;
                    }

                    //لو رحت لبروسيس جديدة هكمل رسم القديمة وابدأ ارسم الجديدة
                    if (newindex != index)
                    {
                        if (inturrupt > 0)
                        {
                            count++;
                        }
                        inturrupt++;
                    }

                    countlist[newindex].remaining_time--;
                    if (countlist[newindex].remaining_time <= 0)
                    {
                        countlist[newindex].waiting = t - countlist[newindex].arrival_time - countlist[newindex].burst_time;
                        minprocess.waiting = countlist[newindex].waiting;
                        for (int i = newindex; i < n - 1; i++)
                        {
                            countlist[i] = countlist[i + 1];
                        }
                        n--;
                        newindex = 0;
                        flagcount = 0;
                        if (n == 0)
                        {

                        };


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

                 int extra = (count < 9) ? 100 : (800 / count);
                 t = 1;
                 priority = 1000000;
                 minprocess = Sortedprocesses[0];
                 pjsum = 0;
                 index = -1;
                 newindex = 0;
                 n = num_process;
                 inturrupt = 0;
                 x = 20;
                 y = 1;
                 int tend = 0;
                 flagcount = 9887;//عشان لما بروسيس تخلف اخليه بصفر واخلى الاندكس ميساويش النيو اندكس
                 flag2 = 0;//عشان ارسم اول بروسيس
                 flag = 0;// يعنى مفيش بروسيس موجودة
                
                while (n > 0)
                {
                    flag = 0;                    // بجيب هنا البروسيس اللى جاهزة تخش
                    for (int i = 0; i < n; i++)
                    {
                        if (Sortedprocesses[i].arrival_time < t)
                        {
                            flag++;
                            if (Sortedprocesses[i].priority < priority)
                            {
                                minprocess = Sortedprocesses[i];
                                priority = Sortedprocesses[i].priority;
                                newindex = i;
                            }
                        }

                    }
                    //لو لقيت الفلاج بصفر يبقى مفيش ولا بروسيس جاهزة وهزود التايم واروح اللفة التانية
                    if (flag == 0)
                    {
                        t++;
                        continue;
                    }
                    //دا عشان ارسم اول بروسيس ومش هخش هنا تانى
                    if (flag2 == 0)
                    {
                        flag2++;
                        if (y % 2 != 0)
                            g.FillRectangle(sbwhite, x, 20, extra, 50);
                        else
                            g.FillRectangle(sbyellow, x, 20, extra, 50);

                        g.DrawString((t - 1).ToString(), font, sblack, new PointF(x + 3, 80));
                        g.DrawString(Sortedprocesses[newindex].name, bigfont, sblack, new PointF(x + (extra / 2) - 15, 35));
                    }
                  
                    //لو رحت لبروسيس جديدة هكمل رسم القديمة وابدأ ارسم الجديدة
                    if (newindex != index)
                    {
                        if (inturrupt > 0)
                        {
                            g.DrawString((tend).ToString(), font, sblack, new PointF(x + extra - 15, 80));
                            x += extra;
                                y++;
                                if (y % 2 != 0)
                                    g.FillRectangle(sbwhite, x, 20, extra, 50);
                                else
                                    g.FillRectangle(sbyellow, x, 20, extra, 50);

                                g.DrawString((t - 1).ToString(), font, sblack, new PointF(x + 3, 80));
                                g.DrawString(Sortedprocesses[newindex].name, bigfont, sblack, new PointF(x + (extra / 2) - 15, 35));
                        }
                        inturrupt++;
                    }

                    Sortedprocesses[newindex].remaining_time--;
                    if (Sortedprocesses[newindex].remaining_time <= 0)
                    {
                        Sortedprocesses[newindex].waiting = t - Sortedprocesses[newindex].arrival_time - Sortedprocesses[newindex].burst_time;
                        minprocess.waiting = Sortedprocesses[newindex].waiting;
                        tend = t;
                        Sortedprocesses.RemoveAt(newindex);
                        n--;
                        newindex = 0;
                        flagcount = 0;
                        if (n == 0)
                        {
                            g.DrawString(t.ToString(), font, sblack, new PointF(x + extra - 15, 80));
                        }


                        pjsum += minprocess.waiting;
                    }
                    else {
                        tend = t;
                    }
                   t++;
                   if (flagcount != 0)
                       index = newindex;
                   else
                       index = 1000000;

                   flagcount++;
                   priority = 100000;

                }

                pictureBox1.Invalidate();
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
                int count = 0;
                List<process> Sortedprocesses = processes.OrderBy(o => o.arrival_time).ToList();
                List<process> countlist = new List<process>();
                foreach (process pc in Sortedprocesses)
                {
                    countlist.Add(new process(pc));
                }
                while (n > 0)
                {
                    flag = 0;
                    numR = 0;
                    for (int i = 0; i < n; i++)
                        if (countlist[i].arrival_time <= t)
                        {
                            flag++;
                            numR++;
                        }
                    if (flag == 0)
                    {
                        t++;
                        continue;

                    }


                    if (countlist[0].remaining_time > quantum)
                    {
                        countlist[0].remaining_time -= quantum;

                        count++;
                        t += quantum;
                        numR = 0;
                        for (int i = 0; i < n; i++)
                            if (countlist[i].arrival_time <= t)
                            {
                                flag++;
                                numR++;
                            }

                        process tp = new process();
                        tp = countlist[0];
                        for (int i = 0; i < numR - 1; i++)
                        {
                            countlist[i] = countlist[i + 1];

                        }
                        countlist[numR - 1] = tp;



                       
                    }
                    else
                    {



                        count++;
                        t += countlist[0].remaining_time;
                        countlist[0].waiting = t - countlist[0].burst_time - countlist[0].arrival_time;
                        rrsum += countlist[0].waiting;
                        countlist[0].remaining_time = 0;
                        countlist.RemoveAt(0);
                        n--;
                        numR--;
                    }

                }

         
             int extra = (count < 9) ? 100 : (800 / count);

             flag = 0;
             t = 0;
             rrsum = 0;
             n = num_process;
             x = 20;
             y = 1;
             numR=0;
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
                    {
                        t++;
                        continue;
                    }
                 
                  
                    if (Sortedprocesses[0].remaining_time > quantum)
                        {
                            Sortedprocesses[0].remaining_time -= quantum;
        
                            if (y % 2 != 0)
                                g.FillRectangle(sbwhite, x, 20, extra, 50);
                            else
                                g.FillRectangle(sbyellow, x, 20, extra, 50);

                            g.DrawString((t).ToString(), font, sblack, new PointF(x + 3, 80));
                            g.DrawString(Sortedprocesses[0].name, bigfont, sblack, new PointF(x + (extra / 2) - 15, 35));
                            g.DrawString((t + quantum).ToString(), font, sblack, new PointF(x + extra - 15, 80));
                            t += quantum;
                            numR = 0;
                            for (int i = 0; i < n; i++)
                                if (Sortedprocesses[i].arrival_time <= t)
                                {
                                    flag++;
                                    numR++;
                                }
                            
                                process tp = new process();
                                tp = Sortedprocesses[0];
                                for (int i = 0; i <numR-1; i++)
                                {
                                    Sortedprocesses[i] = Sortedprocesses[i+1];

                                }
                                Sortedprocesses[numR-1] = tp;
                            
                                
                                
                            x += extra;
                            y++;
                        }
                        else
                        {
                                
                                
                               
                            if (y % 2 != 0)
                                g.FillRectangle(sbwhite, x, 20, extra, 50);
                            else
                                g.FillRectangle(sbyellow, x, 20, extra, 50);

                            g.DrawString((t).ToString(), font, sblack, new PointF(x + 3, 80));
                            g.DrawString(Sortedprocesses[0].name, bigfont, sblack, new PointF(x + (extra / 2) - 15, 35));
                            g.DrawString((t+ Sortedprocesses[0].remaining_time).ToString(), font, sblack, new PointF(x + extra - 15, 80));
                            

                            t += Sortedprocesses[0].remaining_time;
                            Sortedprocesses[0].waiting = t - Sortedprocesses[0].burst_time - Sortedprocesses[0].arrival_time;
                            rrsum += Sortedprocesses[0].waiting;
                            Sortedprocesses[0].remaining_time = 0;
                            Sortedprocesses.RemoveAt(0);
                            n--;
                            numR--;    
                        
                        
                            x += extra;
                            y++;
                        }

            }
            pictureBox1.Invalidate();
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
            label11.Visible = false;
            label8.Visible = false;
            g = Graphics.FromImage(image);
            g.Clear(Color.WhiteSmoke);
            pictureBox1.Invalidate();
            datagridview1.Visible = false;
            textBox3.Visible = false;
            label5.Visible = false;
            confirm.Enabled = false;
            button2.Enabled = false;
            processes.Clear();
            textBox1.Text = "0";
            comboBox1.Text = "";
            num_process = Int32.Parse(textBox1.Text);
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

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, 0, 0, image.Width, image.Height);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            datagridview1.DataSource = null;
            datagridview1.Columns.Clear();  //Just make sure things are blank.
            datagridview1.Columns.Add("Column1","Name");
            datagridview1.Columns.Add("Column2","Arrival time");
            datagridview1.Columns.Add("Column3","Burst Time");
            datagridview1.Columns.Add("Column3", "Priority");


            datagridview1.Rows.Clear();
            for(int i = 0;i<10;i++)
            {
                datagridview1.Rows.Add("P"+(i.ToString()));
            }
            datagridview1.EditMode = DataGridViewEditMode.EditOnKeystroke;
        }

        private void datagridview1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datagridview1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {int i,flag=0;
        int n;
            
            for ( i = 0; i < num_process; i++)
            {

                if ((datagridview1.Rows[i].Cells[1].Value != null) && (datagridview1.Rows[i].Cells[2].Value != null)) 
                {
                    bool isNumeric = int.TryParse(datagridview1.Rows[i].Cells[1].Value.ToString(), out n);
                    bool isNumeric2 = int.TryParse(datagridview1.Rows[i].Cells[2].Value.ToString(), out n);
                    bool isNumeric3=true; //assume priority entered correct
                    if (comboBox1.Text == "Priority (Preemptive)" || comboBox1.Text == "Priority (Non Preemptive)")
                    {
                        if (datagridview1.Rows[i].Cells[3].Value != null)
                            isNumeric3 = int.TryParse(datagridview1.Rows[i].Cells[3].Value.ToString(), out n);
                        else flag = 1; //priority empty
                    
                    }

                    if (!isNumeric || !isNumeric2 || !isNumeric3)
                    {
                        flag = 1;
                    }
                }
                else flag = 1;
            }

            if(flag==1)
             MessageBox.Show("Please fill all fields with valid integers");

            else{
            button1.Visible = false; 
            button2.Enabled = true;}

        }

        private void datagridview1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datagridview1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            button1.Visible = true;
            button1.Enabled = true;
        }
    }
}
