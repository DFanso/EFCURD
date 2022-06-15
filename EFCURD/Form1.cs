using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace EFCURD
{
    public partial class Form1 : Form
    {
        Customer model1 = new Customer();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clear();
            PopulateDataGridView();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        void clear ()
        {
            txtFirstName.Text = txtLastName.Text = txtCity.Text = txtAddress.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            model1.CustomerID = 0;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            model1.FirstName = txtFirstName.Text.Trim();
            model1.LastName = txtLastName.Text.Trim();
            model1.City = txtCity.Text.Trim();
            model1.Address = txtAddress.Text.Trim();

            using (DBEntities db = new DBEntities())
            {
                //insert
                if(model1.CustomerID == 0)
                    db.Customers.Add(model1);
                else //update
                    db.Entry(model1).State = EntityState.Modified;
                db.SaveChanges();
            }
            clear();
            PopulateDataGridView();
            MessageBox.Show("Submitted Successfully");
        }

        void PopulateDataGridView()
        {
            using(DBEntities db = new DBEntities())
            {
                dataGridView1.DataSource = db.Customers.ToList<Customer>();
                
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure to Delete data?","Warning",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (DBEntities db = new DBEntities())
                {
                    var entry = db.Entry(model1);
                    if (entry.State == EntityState.Detached)
                        db.Customers.Attach(model1);
                    db.Customers.Remove(model1);
                    db.SaveChanges();
                    PopulateDataGridView();
                    clear();
                    MessageBox.Show("Deleted Successfully");
                }    
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DoubleClick_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                model1.CustomerID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["CustomerID"].Value);

                using (DBEntities db = new DBEntities())
                {
                    model1 = db.Customers.Where(x => x.CustomerID == model1.CustomerID).FirstOrDefault();

                    txtFirstName.Text = model1.FirstName;
                    txtLastName.Text = model1.LastName;
                    txtCity.Text = model1.City;
                    txtAddress.Text = model1.Address;
                }
                btnSave.Text = "Update";
                btnDelete.Enabled = true;

            }
        }
    }
}
