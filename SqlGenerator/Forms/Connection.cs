using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using SqlGenerator.Extenders;
using SqlConnection.GetSchema;


namespace SqlGenerator.Forms
{
	public partial class Connection : Form
	{
		public Connection()
		{
			InitializeComponent();
		}


		#region  Properties

		public string ConnectionString
		{
			get { return txtConnectionString.Text; }
			set { txtConnectionString.Text = value; }
		}

		public string DisplayName
		{
			get
			{
				var builder = new SqlConnectionStringBuilder(ConnectionString);
				return string.Format("{0} - {1}", builder.DataSource, builder.InitialCatalog);
			}
		}

		#endregion


		#region Event Handlers

		private void Connection_Load(object sender, EventArgs e)
		{
			if (txtConnectionString.Text.IsNullOrEmpty())
			{
				txtConnectionString.Text = "Server=localhost; Database=; Trusted_Connection=True; User ID=; Password=;";
			}
		}

		private void Connection_Activated(object sender, EventArgs e)
		{
			btnCancel.Focus();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				using (var con = new SqlConnection(txtConnectionString.Text))
				{
					con.Open();
				}
				DialogResult = DialogResult.OK;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to validate connection string", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion
		
		
	//Added - SQL DB browsing capability  -	You can use SqlConnection.GetSchema:
       private void btnBrowseSchema_Click(object sender, EventArgs e) {
       	
       using(var con = new SqlConnection("Data Source=Yourserver; Integrated Security=True;"))
       {
        con.Open();
        DataTable databases = con.GetSchema("Databases");
        foreach (DataRow database in databases.Rows)
                {
        String databaseName = database.Field<String>("database_name");
        short dbID = database.Field<short>("dbid");
        DateTime creationDate = database.Field<DateTime>("create_date");
                }
        } 
	}
	
	}
}
