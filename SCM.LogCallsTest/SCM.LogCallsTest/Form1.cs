using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCM.LogCallsTest
{
    public partial class Form1 : Form
    {
        Random r;
        int qid = 0;

        public Form1()
        {
            InitializeComponent();
            r = new Random();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SCMContext ctx = new SCMContext();
            var minId = 1;
            var maxId = 1;
            if (chkExisting.Checked)
            {
                var list = ctx.ServiceRequests.Where(x => x.StatusId < 90).Select(x => x.CustomerId).ToList();
                if (chkRequests.Checked)
                {
                    int index = r.Next(0, list.Count / 2);
                    minId = list[index];
                    index = r.Next(index + 1, list.Count - 1);
                    maxId = list[index];
                }
                else
                {
                    minId = ctx.Customers.Min(x => x.Id);
                    maxId = ctx.Customers.Max(x => x.Id);
                }
                var id = r.Next(minId, maxId);
                var cus = ctx.Customers.Find(id);
                customerName.Text = cus.Name;
                if (!string.IsNullOrEmpty(cus.Phone))
                    customerPhone.Text = cus.Phone;
                else
                    customerPhone.Text = cus.Mobile;
                phone.Text = customerPhone.Text;
            }
            else
            {
                int no = r.Next(1111111, 9999999);
                phone.Text = "012" + no.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            date.Text = DateTime.Now.ToString("MM/dd/yy HH:mm tt");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ext = txtExt.Text;
            var user = txtUserName.Text;
            var theDate = Convert.ToDateTime(date.Text, System.Globalization.CultureInfo.InvariantCulture);
            var dt = theDate.ToString("MM/dd/yy");
            var tt = theDate.ToString("HH:mm tt");

            var ctx = new SCMContext();
            ctx.q_log_call(dt, tt, null, null, "Incoming", phone.Text, null);
            qid = ctx.QINs.Max(x => x.Id);
            listBox1.Items.Add(qid.ToString());
            qid = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem != null)
            {
                int id = int.Parse((string) listBox1.SelectedItem);
                var ctx = new SCMContext();
                var qItem = ctx.QINs.Find(id);
                ctx.q_log_call(DateTime.Now.ToString("MM/dd/yy"), DateTime.Now.ToString("HH:mm tt"), "102", "03", null, qItem.TNo, "0:10':20\"");
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var ctx = new SCMContext();
            ctx.QINs.RemoveRange(ctx.QINs.ToList());
            ctx.SaveChanges();
            listBox1.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var theDate = Convert.ToDateTime(date.Text, System.Globalization.CultureInfo.InvariantCulture);
            var dt = theDate.ToString("M/dd/yy");
            var tt = theDate.ToString("HH:mmtt");

            txtOutput.Text += string.Format("\r\n {0} {1}  {2} {3}  {4}{5} {6}\n\r", dt, tt, "  ", "  ", "< DISA   Incoming >", phone.Text, "  ");

            theDate = theDate.AddMinutes(r.Next(59)).AddSeconds(r.Next(59));
            dt = theDate.ToString("M/dd/yy");
            tt = theDate.ToString("HH:mmtt");
            txtOutput.Text += string.Format("\r\n {0} {1}  {2} {3}  {4}{5} {6}\n\r", dt, tt, "102", "03", "< DISA   Incoming >", phone.Text, "0:00':00\"  ...   25");

        }
    }
}
