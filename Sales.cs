using Dz4.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dz4
{
    public partial class Sales : Form
    {
        private readonly string connectionString;
        private readonly string providerName;
        public Sales(string connectionString, string providerName)
        {
            this.providerName = providerName;
            this.connectionString = connectionString;
            InitializeComponent();
        }

        private void SelectTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedItemService service = new SelectedItemService(connectionString, providerName);
            switch (SelectTables.SelectedItem)
            {
                case "Покупатели": TextObject.Clear(); service.GetSellers(TextObject); break;
                case "Продавцы": TextObject.Clear(); service.GetCustomers(TextObject); break;
                case "Продажи": TextObject.Clear(); service.GetSales(TextObject); break;
            }
        }
    }
}
